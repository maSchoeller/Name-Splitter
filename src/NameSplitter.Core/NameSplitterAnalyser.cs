using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using NameSplitter.Core.Models;

namespace NameSplitter.Core
{
    public class NameSplitterAnalyser
    {
        private readonly IServiceProvider _provider;

        public NameSplitterAnalyser(IServiceProvider provider)
        {
            _provider = provider;

        }

        public async Task<NameSplittingResult> AnalyseAsync(string target)
        {
            var error = new List<string>();
            var result = new NameSplittingResult
            {
                IsValid = true
            };
            var context = _provider.GetRequiredService<NameSplitterDBContext>();

            var salutation = await context.Salutations
                .OrderBy(s => s.Content.Length)
                .FirstOrDefaultAsync(s => target.ToLower().Contains(s.Content.ToLower()));
            if (salutation is not null && target.StartsWith(salutation.Content + " "))
            {
                target = target.Remove(salutation.Content);
                result.Salutation = salutation;
            }
            else
            {
                error.Add("Es konnte keine Anrede bestimmt werden");
                result.IsValid = false;
            }

            var titles = await context.Titles
                .Where(t => target.ToLower().Contains(t.Content.ToLower()))
                .OrderBy(t => t.Content.Length)
                .ToListAsync(); ;
            var resultTitles = new List<Title>();
            foreach (var title in titles)
            {
                if (target.Contains(title.Content, StringComparison.InvariantCultureIgnoreCase))
                {
                    target = target.Remove(title.Content);
                    resultTitles.Add(title);
                }
            }
            result.Titles = ImmutableArray.Create(resultTitles.ToArray());

            if (target.Contains(",", StringComparison.InvariantCultureIgnoreCase))
            {
                result.Lastname = target.Substring(0, target.IndexOf(',', StringComparison.InvariantCultureIgnoreCase) - 1);
                result.Firstname = target.Substring(target.IndexOf(',', StringComparison.InvariantCultureIgnoreCase));
            }
            else
            {
                var nameExpansion = await context.NameFillingWords
                    .FirstOrDefaultAsync(n => target.ToLower().Contains(n.FillingWord.ToLower()));
                if (nameExpansion is not null)
                {
                    result.Firstname = target.Substring(0, target.IndexOf(nameExpansion.FillingWord, StringComparison.InvariantCultureIgnoreCase));
                    result.Lastname = target.Substring(target.IndexOf(nameExpansion.FillingWord, StringComparison.InvariantCultureIgnoreCase));
                }
                else
                {
                    var wordSplitt = target.Split(" ");
                    if (wordSplitt.Length == 1)
                    {
                        result.Lastname = wordSplitt[0];
                    }
                    else
                    {
                        result.Firstname = wordSplitt.Take(wordSplitt.Length - 1).Aggregate((l, n) => l + " " + n);
                        result.Lastname = wordSplitt[wordSplitt.Length - 1];
                    }
                }
            }
            result.ErrorMessage = ImmutableArray.Create(error.ToArray());
            return result;
        }
    }

    public record Word(string word);

    public class NameSplittingResult
    {
        public NameSplittingResult()
        {
            Firstname = string.Empty;
            Lastname = string.Empty;
        }

        public Salutation? Salutation { get; internal set; }
        public ImmutableArray<Title> Titles { get; internal set; }
        public string Firstname { get; internal set; }
        public string Lastname { get; internal set; }

        public bool IsValid { get; internal set; }
        public ImmutableArray<string> ErrorMessage { get; internal set; }
    }
}

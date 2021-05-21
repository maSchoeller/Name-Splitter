using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using NameSplitter.Core.Models;

namespace NameSplitter.Core
{
    public class NameSplitterAnalyser
    {
        private readonly NameSplitterDBContext _context;

        public NameSplitterAnalyser(NameSplitterDBContext context)
        {
            _context = context;

        }

        public async Task<NameSplittingResult> AnalyseAsync(string target)
        {
            //Note: all comparisons above are case insensitive.
            var error = new List<string>();
            var result = new NameSplittingResult
            {
                IsValid = true
            };

            //Note: get corresponding salutation form database
            var salutation = await _context.Salutations
                .OrderBy(s => s.Content.Length)
                .FirstOrDefaultAsync(s => target.ToLower().Contains(s.Content.ToLower()));
            if (salutation is not null && target.StartsWith(salutation.Content + " "))
            {
                target = target.Remove(salutation.Content);
                result.Salutation = salutation;
            }

            //Note: get corresponding titles form database
            var titles = await _context.Titles
                .Where(t => target.ToLower().Contains(t.Content.ToLower()))
                .OrderByDescending(t => t.Content.Length)
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


            //Note: get first- and lastname form target string
            if (target.Contains(",", StringComparison.InvariantCultureIgnoreCase))
            {
                result.Lastname = target.Substring(0, target.IndexOf(',', StringComparison.InvariantCultureIgnoreCase)).Trim();
                result.Firstname = target.Substring(target.IndexOf(',', StringComparison.InvariantCultureIgnoreCase) + 1).Trim();
            }
            else
            {
                var nameExpansion = await _context.NameFillingWords
                    .FirstOrDefaultAsync(n => target.ToLower().Contains(n.FillingWord.ToLower()));
                if (nameExpansion is not null)
                {
                    result.Firstname = target.Substring(0, target.IndexOf(nameExpansion.FillingWord, StringComparison.InvariantCultureIgnoreCase)).Trim();
                    result.Lastname = target.Substring(target.IndexOf(nameExpansion.FillingWord, StringComparison.InvariantCultureIgnoreCase)).Trim();
                }
                else
                {
                    var wordSplitt = target.Split(" ");
                    if (wordSplitt.Length == 1)
                    {
                        result.Lastname = wordSplitt[0].Trim();
                    }
                    else
                    {
                        result.Firstname = wordSplitt.Take(wordSplitt.Length - 1).Aggregate((l, n) => l + " " + n).Trim();
                        result.Lastname = wordSplitt[wordSplitt.Length - 1].Trim();
                    }
                }
            }
            result.ErrorMessage = ImmutableArray.Create(error.ToArray());
            return result;
        }
    }

    public record Word(string word);
}

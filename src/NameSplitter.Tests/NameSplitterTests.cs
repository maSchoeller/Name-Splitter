using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using NameSplitter.Core;
using NameSplitter.Core.Models;

using Xunit;

namespace NameSplitter.Tests
{

    public class NameSplitterTests
    {
        private readonly NameSplitterDBContext _dbContext;
        public NameSplitterTests()
        {
            _dbContext = new NameSplitterDBContext(
                new DbContextOptionsBuilder()
                    .UseSqlite("Data Source=namesplitter.db")
                    .Options);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public async Task NameSplittingValidTest(string input, NameSplittingResult expected)
        {
            var analyser = new NameSplitterAnalyser(_dbContext);
            var result = await analyser.AnalyseAsync(input);
            Assert.Equal(expected.Firstname, result.Firstname);
            Assert.Equal(expected.Lastname, result.Lastname);
            Assert.Equal(expected.Titles.Length, result.Titles.Length);
            Assert.Equal(expected.IsValid, result.IsValid);
            if (expected.Salutation is not null)
            {
                Assert.Equal(expected.Salutation.Content, result.Salutation.Content);
                Assert.Equal(expected.Salutation.Form, result.Salutation.Form);
                Assert.Equal(expected.Salutation.Gender, result.Salutation.Gender);
                Assert.Equal(expected.Salutation.Language, result.Salutation.Language);
            }
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
            new object[] {"Herr Prinz Lorenzo von Matterhorn",
                new NameSplittingResult {

                    Firstname = "Lorenzo",
                    Lastname = "von Matterhorn",
                    IsValid = true,
                    Salutation = new Salutation {Content="Herr",Form=SalutationForm.Formal,Gender=Gender.Male,Language=Language.German },
                    Titles = ImmutableArray.Create(new[] { new Title() })}
                },
            new object[] {"Frau Sandra Berger",
                new NameSplittingResult {
                    Firstname = "Sandra",
                    Lastname = "Berger",
                    IsValid = true,
                    Salutation = new Salutation {Content="Frau",Form=SalutationForm.Formal,Gender=Gender.Female,Language=Language.German },
                    Titles = ImmutableArray.Create(Array.Empty<Title>())}
                },
            new object[] {"Herr Dr. Sandro Gutmensch",
                new NameSplittingResult {
                    Firstname = "Sandro",
                    Lastname = "Gutmensch",
                    IsValid = true,
                    Salutation = new Salutation {Content="Herr",Form=SalutationForm.Formal,Gender=Gender.Male,Language=Language.German },
                    Titles = ImmutableArray.Create(new[] { new Title() })}
                },
            new object[] {"Professor Heinrich Freiherr vom Wald",
                new NameSplittingResult {
                    Firstname = "Heinrich",
                    Lastname = "Freiherr vom Wald",
                    IsValid = true,
                    Salutation = null,
                    Titles = ImmutableArray.Create(new[] { new Title() })}
                },
            new object[] {"Mrs. Doreen Faber",
                new NameSplittingResult {
                    Firstname = "Doreen",
                    Lastname = "Faber",
                    IsValid = true,
                    Salutation = new Salutation {Content="Mrs.",Form=SalutationForm.Formal,Gender=Gender.Female,Language=Language.English },
                    Titles = ImmutableArray.Create( Array.Empty<Title>() )}
                },
            new object[] {"Mme. Charlotte Noir",
                new NameSplittingResult {
                    Firstname = "Charlotte",
                    Lastname = "Noir",
                    IsValid = true,
                    Salutation = new Salutation {Content="Mme.",Form=SalutationForm.Formal,Gender=Gender.Female,Language=Language.French },
                    Titles = ImmutableArray.Create(Array.Empty<Title>())}
                },
            new object[] {"Frau Prof. Dr. rer. nat. Maria von Leuthäuser-Schnarrenberger",
                new NameSplittingResult {

                    Firstname = "Maria",
                    Lastname = "von Leuthäuser-Schnarrenberger",
                    IsValid = true,
                    Salutation = new Salutation {Content="Frau",Form=SalutationForm.Formal,Gender=Gender.Female,Language=Language.German },
                    Titles = ImmutableArray.Create(new[] { new Title(),new Title() })}
                },
            new object[] {"Herr Dipl. Ing. Max von Müller",
                new NameSplittingResult {
                    Firstname = "Max",
                    Lastname = "von Müller",
                    IsValid = true,
                    Salutation = new Salutation {Content="Herr",Form=SalutationForm.Formal,Gender=Gender.Male,Language=Language.German },
                    Titles = ImmutableArray.Create(new[] { new Title() })}
                },
            new object[] {"Dr. Russwurm, Winfried",
                new NameSplittingResult {
                    Firstname = "Winfried",
                    Lastname = "Russwurm",
                    IsValid = true,
                    Salutation = null,
                    Titles = ImmutableArray.Create(new[] { new Title() })}
                },
            new object[] {"Herr Dr.-Ing. Dr. rer. nat. Dr. h.c.  mult. Paul Steffens",
                new NameSplittingResult {
                    Firstname = "Paul",
                    Lastname = "Steffens",
                    IsValid = true,
                    Salutation = new Salutation {Content="Herr",Form=SalutationForm.Formal,Gender=Gender.Male,Language=Language.German },
                    Titles = ImmutableArray.Create(new[] { new Title(),new Title(),new Title() ,new Title()})}
                },
            new object[] {"M. Alexandre Renault",
                new NameSplittingResult {

                    Firstname = "Alexandre",
                    Lastname = "Renault",
                    IsValid = true,
                    Salutation = new Salutation {Content="M.",Form=SalutationForm.Formal,Gender=Gender.Male,Language=Language.French },
                    Titles = ImmutableArray.Create(Array.Empty<Title>())}
                },
            };
    }
}

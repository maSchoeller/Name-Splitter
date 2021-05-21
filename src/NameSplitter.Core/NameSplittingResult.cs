using System.Collections.Immutable;

using NameSplitter.Core.Models;

namespace NameSplitter.Core
{
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

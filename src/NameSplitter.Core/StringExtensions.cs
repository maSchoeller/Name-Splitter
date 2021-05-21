using System;

namespace NameSplitter.Core
{
    public static class StringExtensions
    {
        public static string Remove(this string target, string argument)
        {
            var start = target.IndexOf(argument, StringComparison.InvariantCultureIgnoreCase);
            if (start == -1)
            {
                return target;
            }
            return (target.Substring(0, start) + target.Substring(start + argument.Length)).Trim();
        }
    }
}

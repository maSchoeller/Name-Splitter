namespace NameSplitter.Core
{
    public static class StringExtensions
    {
        public static string Remove(this string target, string argument)
        {
            var start = target.IndexOf(argument);
            if (start == 0)
            {
                return target;
            }
            return target.Substring(0, start + 1) + target.Substring(start + argument.Length);
        }
    }
}

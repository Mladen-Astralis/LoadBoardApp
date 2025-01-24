using System.Text.RegularExpressions;

namespace LoadBoardApp.Common.Extensions
{
    public static class StringExtensions
    {
        public static string Truncate(this string value, int maxLength)
            => value?.Length > maxLength ? value.Substring(0, maxLength) + "..." : value;

        public static string ReplaceSpecialCharactersWithWhitespace(this string inputString)
        {
            return Regex.Replace(inputString, "[!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]", string.Empty);
        }
    }
}

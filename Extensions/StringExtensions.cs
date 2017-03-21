using System.Text.RegularExpressions;

namespace Jaywing.Ditto.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveTrailingDelimiter(this string s, string delimiter)
        {
            try
            {
                if (s.EndsWith(delimiter))
                    s = s.TrimEnd(delimiter.ToCharArray());
                return s;
            }
            catch { return s; }
        }
        
        public static string RemoveSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", "");
        }
    }
}
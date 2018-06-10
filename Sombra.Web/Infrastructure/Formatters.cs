using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Sombra.Web.Infrastructure
{
    public static class Formatters
    {
        public static IFormatProvider DefaultFormatProvider = CultureInfo.GetCultureInfo("nl-NL");
        private static readonly Regex UrlRegex = new Regex("[\\~#%&*{}/:<>?|\"\']");
        
        public static string ToFormattedString(this DateTime dateTime)
        {
            return dateTime.ToString(DefaultFormatProvider);
        }

        public static string ToFormattedString(this decimal value)
        {
            return value.ToString(DefaultFormatProvider);
        }

        public static string CreateUrlComponent(string value)
        {
            return Regex.Replace(UrlRegex.Replace(value, " "), @"\s+", "-");
        }
    }
}

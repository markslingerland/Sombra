using System;
using System.Globalization;

namespace Sombra.Web.Infrastructure
{
    public static class Formatters
    {
        public static IFormatProvider DefaultFormatProvider = CultureInfo.GetCultureInfo("nl-NL");
        
        public static string ToFormattedString(this DateTime dateTime)
        {
            return dateTime.ToString(DefaultFormatProvider);
        }

        public static string ToFormattedString(this decimal value)
        {
            return value.ToString(DefaultFormatProvider);
        }
    }
}

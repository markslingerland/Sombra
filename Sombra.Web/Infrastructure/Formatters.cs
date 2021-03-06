﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Sombra.Web.Infrastructure
{
    public static class Formatters
    {
        public static IFormatProvider DefaultFormatProvider = CultureInfo.GetCultureInfo("nl-NL");
        private static readonly Regex UrlRegex = new Regex(@"[^A-z|0-9|\-|\s]");
        
        public static string ToFormattedString(this DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy");
        }

        public static string ToFormattedString(this decimal value)
        {
            return value.ToString("N0", DefaultFormatProvider);
        }

        public static string CreateUrlComponent(string value)
        {
            return Regex.Replace(UrlRegex.Replace(value, ""), @"\s+", "-");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Sombra.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsAll(this string source, IEnumerable<string> items)
        {
            return source.ContainsAll(items, StringComparison.CurrentCulture);
        }

        public static bool ContainsAll(this string source, IEnumerable<string> items, StringComparison comparisonType)
        {
            return items.All(item => source.Contains(item, comparisonType));
        }
    }
}
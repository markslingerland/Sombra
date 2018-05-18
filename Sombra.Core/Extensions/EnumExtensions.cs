using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Sombra.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            if (enumValue == null) return string.Empty;
            var enumType = enumValue.GetType();

            return enumType.GetCustomAttributes<FlagsAttribute>().Any()
                ? string.Join(',', enumValue.GetIndividualFlags().Select(f => GetName(enumType, f)))
                : GetName(enumType, enumValue);
        }

        private static string GetName(Type enumType, Enum enumValue)
        {
            var fieldInfo = enumType.GetMember(enumValue.ToString()).First();
            if (fieldInfo.GetCustomAttribute<DisplayAttribute>() is DisplayAttribute attribute)
                return attribute.GetName();

            return Enum.GetName(enumType, enumValue);
        }

        public static bool OnlyHasFlag(this Enum value, Enum flag)
        {
            return value.Equals(flag);
        }

        public static IEnumerable<Enum> GetFlags(this Enum value)
        {
            return GetFlags(value, Enum.GetValues(value.GetType()).Cast<Enum>().ToArray());
        }

        public static IEnumerable<Enum> GetIndividualFlags(this Enum value)
        {
            return GetFlags(value, GetFlagValues(value.GetType()).ToArray());
        }

        public static bool HasAnyFlag(this Enum value, Enum flag)
        {
            return flag.GetIndividualFlags().Any(value.HasFlag);
        }

        private static IEnumerable<Enum> GetFlags(Enum value, Enum[] values)
        {
            var bits = Convert.ToUInt64(value);
            var results = new List<Enum>();
            for (var i = values.Length - 1; i >= 0; i--)
            {
                var mask = Convert.ToUInt64(values[i]);
                if (i == 0 && mask == 0L)
                    break;
                if ((bits & mask) == mask)
                {
                    results.Add(values[i]);
                    bits -= mask;
                }
            }
            if (bits != 0L)
                return Enumerable.Empty<Enum>();
            if (Convert.ToUInt64(value) != 0L)
                return results.Reverse<Enum>();
            if (bits == Convert.ToUInt64(value) && values.Length > 0 && Convert.ToUInt64(values[0]) == 0L)
                return values.Take(1);
            return Enumerable.Empty<Enum>();
        }

        private static IEnumerable<Enum> GetFlagValues(Type enumType)
        {
            ulong flag = 0x1;
            foreach (var value in Enum.GetValues(enumType).Cast<Enum>())
            {
                var bits = Convert.ToUInt64(value);
                if (bits == 0L)
                    continue;
                while (flag < bits) flag <<= 1;
                if (flag == bits)
                    yield return value;
            }
        }
    }
}

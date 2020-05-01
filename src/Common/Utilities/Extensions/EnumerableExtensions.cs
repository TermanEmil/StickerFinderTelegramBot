using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
                throw new ArgumentNullException(nameof(enumerable));

            return !enumerable.Any();
        }
    }
}

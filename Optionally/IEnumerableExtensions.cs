using System;
using System.Collections.Generic;
using System.Linq;

namespace Optionally
{
    public static class IEnumerableExtensions
    {
        public static Option<T> TryFirst<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null) return Option<T>.None();
            foreach (var v in enumerable)
            {
                return Option<T>.Some(v);
            }
            return Option<T>.None();
        }

        public static Option<T> TryFirst<T>(this IEnumerable<T> enumerable, Func<T, bool> filter)
        {
            if (enumerable == null) return Option<T>.None();
            if (filter == null) return Option<T>.None();
            return enumerable.Where(filter).TryFirst();
        }
    }
}

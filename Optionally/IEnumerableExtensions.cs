using System;
using System.Collections.Generic;
using System.Linq;

namespace Optionally
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Attemps to retrieve the first element in an IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns>None if the list is empty or null, Some(T) otherwise</returns>
        public static Option<T> TryFirst<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null) return Option<T>.None;
            foreach (var v in enumerable)
            {
                return Option<T>.Some(v);
            }
            return Option<T>.None;
        }


        /// <summary>
        /// Attemps to retrieve the first element in an IEnumerable that satisifies the filter function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="filter"></param>
        /// <returns>None if the enumerable or filter is null. None if filter produces an empty IEnumerable, otherwise Some(T)</returns>
        public static Option<T> TryFirst<T>(this IEnumerable<T> enumerable, Func<T, bool> filter)
        {
            if (enumerable == null) return Option<T>.None;
            if (filter == null) return Option<T>.None;
            return enumerable.Where(filter).TryFirst();
        }
    }
}

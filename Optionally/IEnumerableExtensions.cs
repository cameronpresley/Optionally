using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optionally
{
    public static class IEnumerableExtensions
    {
        public static Option<T> TryHead<T>(this IEnumerable<T> enumerable)
        {
            foreach (var v in enumerable)
            {
                return Option<T>.Some(v);
            }
            return Option<T>.None();
        }

        public static Option<T> TryHead<T>(this IEnumerable<T> enumerable, Func<T, bool> filter)
        {
            return enumerable.Where(filter).TryHead();
        }
    }
}

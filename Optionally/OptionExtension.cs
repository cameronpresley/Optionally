using System;

namespace Optionally
{
    public static class Option
    {
        public static Option<TResult> Apply<T1, T2, TResult>(Func<T1, T2, TResult> func, Option<T1> first, Option<T2> second)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (first.HasValue && second.HasValue) return Option<TResult>.Some(func(first.Value, second.Value));
            return Option<TResult>.None;
        }

        public static Option<TResult> Apply<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, Option<T1> first,
            Option<T2> second, Option<T3> third)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (first.HasValue && second.HasValue && third.HasValue)
                return Option<TResult>.Some(func(first.Value, second.Value, third.Value));
            return Option<TResult>.None;
        }
    }
}

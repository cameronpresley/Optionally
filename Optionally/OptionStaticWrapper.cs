using System;

namespace Optionally
{
    public static class Option
    {
        /// <summary>
        /// Call a function with Option inputs 
        /// </summary>
        /// <typeparam name="T1">Type of the first argument for the function</typeparam>
        /// <typeparam name="T2">Type of the second argument for the function</typeparam>
        /// <typeparam name="TResult">Type of the result of the function</typeparam>
        /// <param name="func"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>Some(TResult) if first and second are Some. Otherwise, None</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Option<TResult> Apply<T1, T2, TResult>(Func<T1, T2, TResult> func, Option<T1> first, Option<T2> second)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (first.HasValue && second.HasValue) return Option<TResult>.Some(func(first.Value, second.Value));
            return Option<TResult>.None;
        }

        /// <summary>
        /// Call a function with Option inputs
        /// </summary>
        /// <typeparam name="T1">Type of the first argument for function</typeparam>
        /// <typeparam name="T2">Type of the second argument for function</typeparam>
        /// <typeparam name="T3">Type of the third argument for function</typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="third"></param>
        /// <returns>Some(TResult) if first, second, and third are Some. Otherwise, None</returns>
        public static Option<TResult> Apply<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, Option<T1> first,
            Option<T2> second, Option<T3> third)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (first.HasValue && second.HasValue && third.HasValue)
                return Option<TResult>.Some(func(first.Value, second.Value, third.Value));
            return Option<TResult>.None;
        }

        /// <summary>
        /// Creates an Option with no value inside
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Option<T> No<T>()
        {
            return Option<T>.None;
        }

        /// <summary>
        /// Creates an Option with a value inside
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If value is null</exception>
        public static Option<T> Some<T>(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return Option<T>.Some(value);
        }
    }
}
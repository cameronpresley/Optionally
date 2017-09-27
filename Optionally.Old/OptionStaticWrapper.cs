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
        public static IOption<TResult> Apply<T1, T2, TResult>(Func<T1, T2, TResult> func, IOption<T1> first, IOption<T2> second)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));

            var firstIsASome = first is Some<T1>;
            var secondIsASome = second is Some<T2>;

            if (!firstIsASome || !secondIsASome) return No<TResult>();

            var firstValue = ((Some<T1>)first).Value;
            var secondValue = ((Some<T2>)second).Value;
            return Some(func(firstValue, secondValue));
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
        /// <exception cref="ArgumentNullException"></exception>
        public static IOption<TResult> Apply<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, IOption<T1> first,
            IOption<T2> second, IOption<T3> third)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));

            var firstIsASome = first is Some<T1>;
            var secondIsASome = second is Some<T2>;
            var thirdIsASome = third is Some<T3>;

            if (!firstIsASome || !secondIsASome || !thirdIsASome) return No<TResult>();

            var firstValue = ((Some<T1>)first).Value;
            var secondValue = ((Some<T2>)second).Value;
            var thirdValue = ((Some<T3>)third).Value;
            return Some(func(firstValue, secondValue, thirdValue));
        }

        /// <summary>
        /// Wrap an exception throwing function into an Option.
        /// </summary>
        /// <param name="func"></param>
        /// <returns>Some(T) if the function succeeds, None otherwise</returns>
        /// <remarks>Useful for when working with functions that might throw and you want to convert exceptions into None</remarks>
        public static IOption<T> Wrap<T>(Func<T> func)
        {
            try
            {
                return Some(func());
            }
            catch
            {
                return No<T>();
            }
        }

        /// <summary>
        /// Creates an Option with no value inside
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IOption<T> No<T>()
        {
            return new None<T>();
        }

        /// <summary>
        /// Creates an Option with a value inside
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IOption<T> Some<T>(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return new Some<T>(value);
        }
    }
}
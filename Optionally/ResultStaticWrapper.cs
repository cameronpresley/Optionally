using System;
using System.Collections.Generic;

namespace Optionally
{
    public static class Result
    {

        /// <summary>
        /// Apply a function to a series of Result arguments
        /// </summary>
        /// <typeparam name="TResult">Type of Success</typeparam>
        /// <typeparam name="TFailure">Type of Failure</typeparam>
        /// <typeparam name="T1">Type of the first argument</typeparam>
        /// <typeparam name="T2">Type of the second argument</typeparam>
        /// <param name="func">Function to call if all arguments are a Success</param>
        /// <param name="first">First argument for the function</param>
        /// <param name="second">Second argument for the function</param>
        /// <returns>If all arguments are Success, then Success is returned. Otherwise, a Failure with all the argument Failures is returned</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>Provides an applicative style of data validation</remarks>
        public static IResult<IEnumerable<TFailure>, TResult> Apply<TResult, TFailure, T1, T2>(Func<T1, T2, TResult> func, IResult<TFailure, T1> first, IResult<TFailure, T2> second)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));

            IResult<IEnumerable<TFailure>, TResult> doSuccess()
            {
                var firstValue = ((Success<TFailure, T1>)first).Value;
                var secondValue = ((Success<TFailure, T2>)second).Value;
                return Success<IEnumerable<TFailure>, TResult>(func(firstValue, secondValue));
            }

            IResult<IEnumerable<TFailure>, TResult> doFailure()
            {
                var errors = new List<TFailure>();
                void addError(TFailure error) => errors.Add(error);
                first.Do(_ => { }, addError);
                second.Do(_ => { }, addError);
                return Failure<IEnumerable<TFailure>, TResult>(errors);
            }

            var firstIsSuccess = first is Success<TFailure, T1>;
            var secondIsSuccess = second is Success<TFailure, T2>;
            return firstIsSuccess && secondIsSuccess ? doSuccess() : doFailure();
        }

        /// <summary>
        /// Applies a function to a series of Result arguments
        /// </summary>
        /// <typeparam name="TFailure">Type of Failure</typeparam>
        /// <typeparam name="TResult">Type of Success</typeparam>
        /// <typeparam name="T1">Type of the first argument</typeparam>
        /// <typeparam name="T2">Type of the second argument</typeparam>
        /// <typeparam name="T3">Type of the third argument</typeparam>
        /// <param name="func">Function to call</param>
        /// <param name="first">First argument for the function</param>
        /// <param name="second">Second argument for the function</param>
        /// <param name="third">Third argument for the function</param>
        /// <returns>If all arguments are Success, then a Success is returned. Otherwise, all the Failures are concatentated into a Failure Result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IResult<IEnumerable<TFailure>, TResult> Apply<TResult, TFailure, T1, T2, T3>(Func<T1, T2, T3, TResult> func, IResult<TFailure, T1> first, IResult<TFailure, T2> second, IResult<TFailure, T3> third)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));


            IResult<IEnumerable<TFailure>, TResult> doSuccess()
            {
                var firstValue = ((Success<TFailure, T1>)first).Value;
                var secondValue = ((Success<TFailure, T2>)second).Value;
                var thirdValue = ((Success<TFailure, T3>) third).Value;
                return Success<IEnumerable<TFailure>, TResult>(func(firstValue, secondValue, thirdValue));
            }

            IResult<IEnumerable<TFailure>, TResult> doFailure()
            {
                var errors = new List<TFailure>();
                void addError(TFailure error) => errors.Add(error);
                first.Do(_ => { }, addError);
                second.Do(_ => { }, addError);
                third.Do(_ => { }, addError);
                return Failure<IEnumerable<TFailure>, TResult>(errors);
            }

            var firstIsSuccess = first is Success<TFailure, T1>;
            var secondIsSuccess = second is Success<TFailure, T2>;
            var thirdIsSuccess = third is Success<TFailure, T3>;

            return firstIsSuccess && secondIsSuccess && thirdIsSuccess ? doSuccess() : doFailure();
        }

        public static IResult<TFailure, TSuccess> Failure<TFailure, TSuccess>(TFailure value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return new Failure<TFailure, TSuccess>(value);
        }

        public static IResult<TFailure, TSuccess> Success<TFailure, TSuccess>(TSuccess value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return new Success<TFailure, TSuccess>(value);
        }
    }
}

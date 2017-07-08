using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optionally
{
    public static class Result
    {

        /// <summary>
        /// Apply a function to a series of Result arguments
        /// </summary>
        /// <typeparam name="T1">Type of the first argument</typeparam>
        /// <typeparam name="T2">Type of the second argument</typeparam>
        /// <param name="func">Function to call if all arguments are a Success</param>
        /// <param name="first">First argument for the function</param>
        /// <param name="second">Second argument for the function</param>
        /// <returns>If all arguments are Success, then Success is returned. Otherwise, a Failure with all the argument Failures is returned</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <remarks>Provides an applicative style of data validation</remarks>
        public static Result<TSuccess, List<TFailure>> Apply<TSuccess, TFailure, T1, T2>(Func<T1, T2, TSuccess> func, Result<T1, TFailure> first, Result<T2, TFailure> second)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            if (first.DidSucceed && second.DidSucceed)
                return Result<TSuccess, List<TFailure>>.Success(func(first._success, second._success));

            var errors = new List<TFailure>();
            if (!first.DidSucceed) errors.Add(first._failure);
            if (!second.DidSucceed) errors.Add(second._failure);
            return Result<TSuccess, List<TFailure>>.Failure(errors);
        }

        /// <summary>
        /// Applies a function to a series of Result arguments
        /// </summary>
        /// <typeparam name="T1">Type of the first argument</typeparam>
        /// <typeparam name="T2">Type of the second argument</typeparam>
        /// <typeparam name="T3">Type of the third argument</typeparam>
        /// <param name="func">Function to call</param>
        /// <param name="first">First argument for the function</param>
        /// <param name="second">Second argument for the function</param>
        /// <param name="third">Third argument for the function</param>
        /// <returns>If all arguments are Success, then a Success is returned. Otherwise, all the Failures are concatentated into a Failure Result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Result<TSuccess, List<TFailure>> Apply<TSuccess, TFailure,T1, T2, T3>(Func<T1, T2, T3, TSuccess> func, Result<T1, TFailure> first, Result<T2, TFailure> second, Result<T3, TFailure> third)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            if (first.DidSucceed && second.DidSucceed && third.DidSucceed)
                return Result<TSuccess, List<TFailure>>.Success(func(first._success, second._success, third._success));

            var errors = new List<TFailure>();
            if (!first.DidSucceed) errors.Add(first._failure);
            if (!second.DidSucceed) errors.Add(second._failure);
            if (!third.DidSucceed) errors.Add(third._failure);

            return Result<TSuccess, List<TFailure>>.Failure(errors);
        }
    }
}

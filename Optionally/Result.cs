using System;
using System.Collections.Generic;

namespace Optionally
{
    /// <summary>
    /// Models a Success or a Failure
    /// </summary>
    /// <typeparam name="TSuccess">Type of success</typeparam>
    /// <typeparam name="TFailure">Type of failure</typeparam>
    public struct Result<TSuccess, TFailure>
    {
        private readonly TSuccess _success;
        private readonly TFailure _failure;
        private readonly bool _didSucceed;

        private Result(TSuccess success, TFailure failure, bool didSucceed)
        {
            _success = success;
            _failure = failure;
            _didSucceed = didSucceed;
        }

        /// <summary>
        /// Creates a Success Result with the value
        /// </summary>
        /// <param name="value">Value of the success</param>
        /// <returns>Success Result</returns>
        /// <remarks>Does not check if value is null</remarks>
        public static Result<TSuccess, TFailure> Success(TSuccess value)
        {
            return new Result<TSuccess, TFailure>(value, default(TFailure), true);
        }

        /// <summary>
        /// Creates a Failure result with the value
        /// </summary>
        /// <param name="value">Value of the failure</param>
        /// <returns>Failure Result</returns>
        /// <remarks>Does not check if value is null</remarks>
        public static Result<TSuccess, TFailure> Failure(TFailure value)
        {
            return new Result<TSuccess, TFailure>(default(TSuccess), value, false);
        }

        /// <summary>
        /// Converts a Success Result to another Success Result
        /// </summary>
        /// <typeparam name="V">Type of the new Success</typeparam>
        /// <param name="mapper">How to convert the Success value</param>
        /// <returns>If Result is a Success, then Success is returned. Otherwise a Failure is returned</returns>
        public Result<V, TFailure> Map<V>(Func<TSuccess, V> mapper)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            return _didSucceed
                ? Result<V, TFailure>.Success(mapper(_success))
                : Result<V, TFailure>.Failure(_failure);
        }

        /// <summary>
        /// Chains a Success Result with a function call
        /// </summary>
        /// <typeparam name="V">Type of the Result from binder</typeparam>
        /// <param name="binder">Function to call if current Result is a Success</param>
        /// <returns>If Result is a Success, then binder is called with the success value. Otherwise, None is returned</returns>
        /// <remarks>Provides a monadic approach to data validation</remarks>
        public Result<V, TFailure> AndThen<V>(Func<TSuccess, Result<V, TFailure>> binder)
        {
            if (binder == null) throw new ArgumentNullException(nameof(binder));
            return _didSucceed ? binder(_success) : Result<V, TFailure>.Failure(_failure);
        }

        /// <summary>
        /// Perform an action on the current Result
        /// </summary>
        /// <param name="onSuccess">Action to call if Result is a Success</param>
        /// <param name="onFailure">Action to call if Result is a Failure</param>
        public void Do(Action<TSuccess> onSuccess, Action<TFailure> onFailure)
        {
            if (_didSucceed)
                onSuccess?.Invoke(_success);
            else
                onFailure?.Invoke(_failure);
        }

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
        public static Result<TSuccess, List<TFailure>> Apply<T1, T2>(Func<T1, T2, TSuccess> func, Result<T1, TFailure> first, Result<T2, TFailure> second)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            if (first._didSucceed && second._didSucceed)
                return Result<TSuccess, List<TFailure>>.Success(func(first._success, second._success));

            var errors = new List<TFailure>();
            if (!first._didSucceed) errors.Add(first._failure);
            if (!second._didSucceed) errors.Add(second._failure);
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
        public static Result<TSuccess, List<TFailure>> Apply<T1, T2, T3>(Func<T1, T2, T3, TSuccess> func, Result<T1, TFailure> first, Result<T2, TFailure> second, Result<T3, TFailure> third)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            if (first._didSucceed && second._didSucceed && third._didSucceed)
                return Result<TSuccess, List<TFailure>>.Success(func(first._success, second._success, third._success));

            var errors = new List<TFailure>();
            if (!first._didSucceed) errors.Add(first._failure);
            if (!second._didSucceed) errors.Add(second._failure);
            if (!third._didSucceed) errors.Add(third._failure);

            return Result<TSuccess, List<TFailure>>.Failure(errors);
        }
    }
}
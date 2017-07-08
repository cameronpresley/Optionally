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
        internal readonly TSuccess _success;
        internal readonly TFailure _failure;
        internal readonly bool DidSucceed;

        private Result(TSuccess success, TFailure failure, bool didSucceed)
        {
            _success = success;
            _failure = failure;
            DidSucceed = didSucceed;
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
            return DidSucceed
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
            return DidSucceed ? binder(_success) : Result<V, TFailure>.Failure(_failure);
        }

        /// <summary>
        /// Perform an action on the current Result
        /// </summary>
        /// <param name="onSuccess">Action to call if Result is a Success</param>
        /// <param name="onFailure">Action to call if Result is a Failure</param>
        public void Do(Action<TSuccess> onSuccess, Action<TFailure> onFailure)
        {
            if (DidSucceed)
                onSuccess?.Invoke(_success);
            else
                onFailure?.Invoke(_failure);
        }
    }
}
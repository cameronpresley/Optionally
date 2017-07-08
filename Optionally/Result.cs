﻿using System;

namespace Optionally
{
    /// <summary>
    /// Models a Success or a Failure
    /// </summary>
    /// <typeparam name="TSuccess">Type of success</typeparam>
    /// <typeparam name="TFailure">Type of failure</typeparam>
    public struct Result<TSuccess, TFailure>
    {
        internal readonly TSuccess SuccessValue;
        internal readonly TFailure FailureValue;
        internal readonly bool DidSucceed;

        private Result(TSuccess success, TFailure failure, bool didSucceed)
        {
            SuccessValue = success;
            FailureValue = failure;
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
        /// <typeparam name="U">Type of the new Success</typeparam>
        /// <param name="mapper">How to convert the Success value</param>
        /// <returns>If Result is a Success, then Success is returned. Otherwise a Failure is returned</returns>
        public Result<U, TFailure> Map<U>(Func<TSuccess, U> mapper)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            return DidSucceed
                ? Result<U, TFailure>.Success(mapper(SuccessValue))
                : Result<U, TFailure>.Failure(FailureValue);
        }

        /// <summary>
        /// Chains a Success Result with a function call
        /// </summary>
        /// <typeparam name="U">Type of the Result from binder</typeparam>
        /// <param name="binder">Function to call if current Result is a Success</param>
        /// <returns>If Result is a Success, then binder is called with the success value. Otherwise, None is returned</returns>
        /// <remarks>Provides a monadic approach to data validation</remarks>
        public Result<U, TFailure> AndThen<U>(Func<TSuccess, Result<U, TFailure>> binder)
        {
            if (binder == null) throw new ArgumentNullException(nameof(binder));
            return DidSucceed ? binder(SuccessValue) : Result<U, TFailure>.Failure(FailureValue);
        }

        /// <summary>
        /// Perform an action on the current Result
        /// </summary>
        /// <param name="onSuccess">Action to call if Result is a Success</param>
        /// <param name="onFailure">Action to call if Result is a Failure</param>
        public void Do(Action<TSuccess> onSuccess, Action<TFailure> onFailure)
        {
            if (DidSucceed)
                onSuccess?.Invoke(SuccessValue);
            else
                onFailure?.Invoke(FailureValue);
        }
    }
}
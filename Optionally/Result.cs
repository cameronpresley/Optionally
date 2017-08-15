using System;

namespace Optionally
{
    /// <summary>
    /// Models a Success or a Failure
    /// </summary>
    /// <typeparam name="TFailure">Type of failure</typeparam>
    /// <typeparam name="TSuccess">Type of success</typeparam>
    public struct Result<TFailure, TSuccess>
    {
        internal readonly TSuccess SuccessValue;
        internal readonly TFailure FailureValue;
        internal readonly bool DidSucceed;

        private Result(TFailure failure, TSuccess success, bool didSucceed)
        {
            FailureValue = failure;
            SuccessValue = success;
            DidSucceed = didSucceed;
        }

        /// <summary>
        /// Creates a Success Result with the value
        /// </summary>
        /// <param name="value">Value of the success</param>
        /// <returns>Success Result</returns>
        /// <remarks>Does not check if value is null</remarks>
        public static Result<TFailure, TSuccess> Success(TSuccess value)
        {
            return new Result<TFailure, TSuccess>(default(TFailure), value, true);
        }

        /// <summary>
        /// Creates a Failure result with the value
        /// </summary>
        /// <param name="value">Value of the failure</param>
        /// <returns>Failure Result</returns>
        /// <remarks>Does not check if value is null</remarks>
        public static Result<TFailure, TSuccess> Failure(TFailure value)
        {
            return new Result<TFailure, TSuccess>(value, default(TSuccess), false);
        }

        /// <summary>
        /// Converts a Success Result to another Success Result
        /// </summary>
        /// <typeparam name="U">Type of the new Success</typeparam>
        /// <param name="mapper">How to convert the Success value</param>
        /// <returns>If Result is a Success, then Success is returned. Otherwise a Failure is returned</returns>
        public Result<TFailure, U> Map<U>(Func<TSuccess, U> mapper)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            return DidSucceed
                ? Result<TFailure, U>.Success(mapper(SuccessValue))
                : Result<TFailure, U>.Failure(FailureValue);
        }

        /// <summary>
        /// Chains a Success Result with a function call
        /// </summary>
        /// <typeparam name="U">Type of the Result from binder</typeparam>
        /// <param name="binder">Function to call if current Result is a Success</param>
        /// <returns>If Result is a Success, then binder is called with the success value. Otherwise, None is returned</returns>
        /// <remarks>Provides a monadic approach to data validation</remarks>
        public Result<TFailure, U> AndThen<U>(Func<TSuccess, Result<TFailure, U>> binder)
        {
            if (binder == null) throw new ArgumentNullException(nameof(binder));
            return DidSucceed ? binder(SuccessValue) : Result<TFailure, U>.Failure(FailureValue);
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

        public override string ToString()
        {
            return DidSucceed ? $"Success of '{SuccessValue}'" : $"Failure of '{FailureValue}'";
        }
    }
}
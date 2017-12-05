using System;

namespace Optionally
{
    public interface IResult<TFailure, TSuccess>
    {
        /// <summary>
        /// Converts a Success Result to another Success Result
        /// </summary>
        /// <typeparam name="U">Type of the new Success</typeparam>
        /// <param name="mapper">How to convert the Success value</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>If Result is a Success, then Success is returned. Otherwise a Failure is returned</returns>
        IResult<TFailure, U> Map<U>(Func<TSuccess, U> mapper);

        /// <summary>
        /// Converts both the Failure and Success type of the Result
        /// </summary>
        /// <typeparam name="UFailure"></typeparam>
        /// <typeparam name="USuccess"></typeparam>
        /// <param name="mapFailure">Function to map the failure</param>
        /// <param name="mapSuccess">Function to map the success</param>
        /// <returns></returns>
        IResult<UFailure, USuccess> BiMap<UFailure, USuccess>(Func<TFailure, UFailure> mapFailure, Func<TSuccess, USuccess> mapSuccess);
       
        /// <summary>
        /// Chains a Success Result with a function call
        /// </summary>
        /// <typeparam name="U">Type of the Result from binder</typeparam>
        /// <param name="binder">Function to call if current Result is a Success</param>
        /// <returns>If Result is a Success, then binder is called with the success value. Otherwise, None is returned</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>Provides a monadic approach to data validation</remarks>
        IResult<TFailure, U> AndThen<U>(Func<TSuccess, IResult<TFailure, U>> binder);

        /// <summary>
        /// Perform an action on the current Result
        /// </summary>
        /// <param name="onFailure">Action to call if Result is a Failure</param>
        /// <param name="onSuccess">Action to call if Result is a Success</param>
        /// <exception cref="ArgumentNullException"></exception>
        IResult<TFailure, TSuccess> Do(Action<TFailure> onFailure, Action<TSuccess> onSuccess);

        /// <summary>
        /// Converts Result to T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="onFailure"></param>
        /// <param name="onSuccess"></param>
        /// <returns></returns>
        T Match<T>(Func<TFailure, T> onFailure, Func<TSuccess, T> onSuccess);
    }

    internal struct Success <TFailure, TSuccess> : IResult<TFailure, TSuccess>
    {
        internal readonly TSuccess Value;

        public Success(TSuccess value)
        {
            Value = value;
        }

        public IResult<TFailure, U> Map<U>(Func<TSuccess, U> mapper)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            return Result.Success<TFailure, U>(mapper(Value));
        }

        public IResult<UFailure, USuccess> BiMap<UFailure, USuccess>(Func<TFailure, UFailure> mapFailure, Func<TSuccess, USuccess> mapSuccess)
        {
            return Result.Success<UFailure, USuccess>(mapSuccess(Value));
        }

        public IResult<TFailure, U> AndThen<U>(Func<TSuccess, IResult<TFailure, U>> binder)
        {
            if (binder == null) throw new ArgumentNullException(nameof(binder));
            return binder(Value);
        }

        public IResult<TFailure, TSuccess> Do(Action<TFailure> onFailure, Action<TSuccess> onSuccess)
        {
            if (onFailure == null) throw new ArgumentNullException(nameof(onFailure));
            if (onSuccess == null) throw new ArgumentNullException(nameof(onSuccess));
            onSuccess(Value);
            return this;
        }

        public T Match<T>(Func<TFailure, T> onFailure, Func<TSuccess, T> onSuccess)
        {
            if (onFailure == null) throw new ArgumentNullException(nameof(onFailure));
            if (onSuccess == null) throw new ArgumentNullException(nameof(onSuccess));

            return onSuccess(Value);
        }

        public override string ToString()
        {
            return $"Success of '{Value}'";
        }
    }

    internal struct Failure <TFailure, TSuccess> : IResult<TFailure, TSuccess>
    {
        internal readonly TFailure Value;

        public Failure(TFailure value)
        {
            Value = value;
        }

        public IResult<TFailure, U> Map<U>(Func<TSuccess, U> mapper)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            return Result.Failure<TFailure, U>(Value);
        }

        public IResult<UFailure, USuccess> BiMap<UFailure, USuccess>(Func<TFailure, UFailure> mapFailure, Func<TSuccess, USuccess> mapSuccess)
        {
            return Result.Failure<UFailure, USuccess>(mapFailure(Value));
        }

        public IResult<TFailure, U> AndThen<U>(Func<TSuccess, IResult<TFailure, U>> binder)
        {
            if (binder == null) throw new ArgumentNullException(nameof(binder));
            return Result.Failure<TFailure, U>(Value);
        }

        public IResult<TFailure, TSuccess> Do(Action<TFailure> onFailure, Action<TSuccess> onSuccess)
        {
            if (onFailure == null) throw new ArgumentNullException(nameof(onFailure));
            if (onSuccess == null) throw new ArgumentNullException(nameof(onSuccess));
            onFailure(Value);
            return this;
        }

        public T Match<T>(Func<TFailure, T> onFailure, Func<TSuccess, T> onSuccess)
        {
            if (onFailure == null) throw new ArgumentNullException(nameof(onFailure));
            if (onSuccess == null) throw new ArgumentNullException(nameof(onSuccess));

            return onFailure(Value);
        }

        public override string ToString()
        {
            return $"Failure of '{Value}'";
        }
    }
}
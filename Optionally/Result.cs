﻿using System;
using System.Collections.Generic;

namespace Optionally
{
    /// <summary>
    /// Models a Success or a Failure
    /// </summary>
    /// <typeparam name="T">Type of success</typeparam>
    /// <typeparam name="U">Type of failure</typeparam>
    public class Result<T, U>
    {
        private readonly T _success;
        private readonly U _failure;
        private readonly bool _didSucceed;

        private Result(T success, U failure, bool didSucceed)
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
        public static Result<T, U> Success(T value)
        {
            return new Result<T, U>(value, default(U), true);
        }

        /// <summary>
        /// Creates a Failure result with the value
        /// </summary>
        /// <param name="value">Value of the failure</param>
        /// <returns>Failure Result</returns>
        /// <remarks>Does not check if value is null</remarks>
        public static Result<T, U> Failure(U value)
        {
            return new Result<T, U>(default(T), value, false);
        }

        /// <summary>
        /// Converts a Success Result to another Success Result
        /// </summary>
        /// <typeparam name="V">Type of the new Success</typeparam>
        /// <param name="mapper">How to convert the Success value</param>
        /// <returns>If Result is a Success, then Success is returned. Otherwise a Failure is returned</returns>
        public Result<V, U> Map<V>(Func<T, V> mapper)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            return _didSucceed
                ? Result<V, U>.Success(mapper(_success))
                : Result<V, U>.Failure(_failure);
        }

        /// <summary>
        /// Chains a Success Result with a function call
        /// </summary>
        /// <typeparam name="V">Type of the Result from binder</typeparam>
        /// <param name="binder">Function to call if current Result is a Success</param>
        /// <returns>If Result is a Success, then binder is called with the success value. Otherwise, None is returned</returns>
        /// <remarks>Provides a monadic approach to data validation</remarks>
        public Result<V, U> AndThen<V>(Func<T, Result<V, U>> binder)
        {
            if (binder == null) throw new ArgumentNullException(nameof(binder));
            return _didSucceed ? binder(_success) : Result<V, U>.Failure(_failure);
        }

        /// <summary>
        /// Perform an action on the current Result
        /// </summary>
        /// <param name="onSuccess">Action to call if Result is a Success</param>
        /// <param name="onFailure">Action to call if Result is a Failure</param>
        public void Do(Action<T> onSuccess, Action<U> onFailure)
        {
            if (_didSucceed && onSuccess != null)
                onSuccess(_success);
            else if (!_didSucceed && onFailure != null)
                onFailure(_failure);
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
        public static Result<T, List<U>> Apply<T1, T2>(Func<T1, T2, T> func, Result<T1, U> first, Result<T2, U> second)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));

            if (first._didSucceed && second._didSucceed)
                return Result<T, List<U>>.Success(func(first._success, second._success));

            var errors = new List<U>();
            if (!first._didSucceed) errors.Add(first._failure);
            if (!second._didSucceed) errors.Add(second._failure);
            return Result<T, List<U>>.Failure(errors);
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
        public static Result<T, List<U>> Apply<T1, T2, T3>(Func<T1, T2, T3, T> func, Result<T1, U> first, Result<T2, U> second, Result<T3, U> third)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));

            if (first._didSucceed && second._didSucceed && third._didSucceed)
                return Result<T, List<U>>.Success(func(first._success, second._success, third._success));

            var errors = new List<U>();
            if (!first._didSucceed) errors.Add(first._failure);
            if (!second._didSucceed) errors.Add(second._failure);
            if (!third._didSucceed) errors.Add(third._failure);

            return Result<T, List<U>>.Failure(errors);
        }

        #region Equal/HashCode overrides
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj as Result<T, U> == null) return false;
            return Equals(obj as Result<T, U>);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        private bool Equals(Result<T, U> other)
        {
            return other._didSucceed == _didSucceed && Equals(_success, other._success) && Equals(_failure, other._failure);
        }
        #endregion
    }
}

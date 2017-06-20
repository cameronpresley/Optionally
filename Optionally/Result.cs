﻿using System;

namespace Optionally
{
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

        public static Result<T, U> Success(T value)
        {
            return new Result<T, U>(value, default(U), true);
        }

        public static Result<T, U> Failure(U value)
        {
            return new Result<T, U>(default(T), value, false);
        }
        public Result<V, U> Map<V>(Func<T, V> mapper)
        {
            return _didSucceed
                ? new Result<V,U>(mapper(_success), default(U), true)
                : new Result<V,U>(default(V), _failure, false);
        }

        public Result<V, U> AndThen<V>(Func<T, Result<V, U>> chain)
        {
            return _didSucceed ? chain(_success) : new Result<V, U>(default(V), _failure, false);
        }

        public void Do(Action<T> onSuccess, Action<U> onFailure)
        {
            if (_didSucceed)
                onSuccess(_success);
            else
                onFailure(_failure);
        }
    }
}

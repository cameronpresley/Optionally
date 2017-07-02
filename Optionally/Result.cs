using System;

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
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            return _didSucceed
                ? Result<V, U>.Success(mapper(_success))
                : Result<V, U>.Failure(_failure);
        }

        public Result<V, U> AndThen<V>(Func<T, Result<V, U>> binder)
        {
            if (binder == null) throw new ArgumentNullException(nameof(binder));
            return _didSucceed ? binder(_success) : Result<V, U>.Failure(_failure);
        }

        public void Do(Action<T> onSuccess, Action<U> onFailure)
        {
            if (_didSucceed && onSuccess != null)
                onSuccess(_success);
            else if (!_didSucceed && onFailure != null)
                onFailure(_failure);
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

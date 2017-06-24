using System;

namespace Optionally
{
    public class Option<T>
    {
        private readonly T _value;
        private readonly bool _hasValue;

        private Option(T value, bool hasValue)
        {
            _value = value;
            _hasValue = hasValue;
        }

        public static Option<T> Some(T value)
        {
            return new Option<T>(value, true);
        }

        public static Option<T> None()
        {
            return new Option<T>(default(T), false);
        }

        public Option<U> Map<U>(Func<T, U> mapper)
        {
            return _hasValue ? new Option<U>(mapper(_value), true) : new Option<U>(default(U), false);
        }

        public Option<U> AndThen<U>(Func<T, Option<U>> chain)
        {
            return _hasValue ? chain(_value) : new Option<U>(default(U), false);
        }

        public void Do(Action<T> ifSome, Action ifNone)
        {
            if (_hasValue)
            {
                ifSome(_value);
            }
            else
            {
                ifNone();
            }
        }
    }
}
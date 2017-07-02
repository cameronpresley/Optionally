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
            if (mapper == null) return Option<U>.None();
            return _hasValue
                ? Option<U>.Some(mapper(_value))
                : Option<U>.None();
        }

        public Option<U> AndThen<U>(Func<T, Option<U>> binder)
        {
            if (binder == null) return Option<U>.None();
            return _hasValue
                ? binder(_value)
                : Option<U>.None();
        }

        public void Do(Action<T> ifSome, Action ifNone)
        {
            if (_hasValue && ifSome != null)
            {
                ifSome(_value);
            }
            else if (!_hasValue && ifNone != null)
            {
                ifNone();
            }
        }

        public Option<T> Where(Func<T, bool> filter)
        {
            if (filter == null) return None();
            if (_hasValue && filter(_value)) return Some(_value);
            return None();
        }

        public static Option<T> Apply<T1, T2>(Func<T1, T2, T> func, Option<T1> first, Option<T2> second)
        {
            if (func == null || first == null || second == null) return None();

            if (first._hasValue && second._hasValue)
                return Some(func(first._value, second._value));
            return None();
        }

        public static Option<T> Apply<T1, T2, T3>(Func<T1, T2, T3, T> func, Option<T1> first, Option<T2> second, Option<T3> third)
        {
            if (func == null || first == null || second == null || third == null) return None();
            if (first._hasValue && second._hasValue && third._hasValue)
                return Some(func(first._value, second._value, third._value));
            return None();
        }

        #region Equals/Hashcode Overrides
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj as Option<T> == null) return false;
            return Equals((Option<T>)obj);
        }

        private bool Equals(Option<T> obj)
        {
            return obj._hasValue == _hasValue && Equals(obj._value, _value);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
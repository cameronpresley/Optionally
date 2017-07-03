using System;

namespace Optionally
{
    /// <summary>
    /// Represents whether a value exists or not
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Option<T>
    {
        private readonly T _value;
        private readonly bool _hasValue;

        private Option(T value, bool hasValue)
        {
            _value = value;
            _hasValue = hasValue;
        }

        /// <summary>
        /// Factory method that creates an Option with a value inside
        /// </summary>
        /// <param name="value">The value to embed in the Option</param>
        /// <remarks>Does not check if value is null</remarks>
        /// <returns></returns>
        public static Option<T> Some(T value)
        {
            return new Option<T>(value, true);
        }

        /// <summary>
        /// Factory method that creates an Option without a value
        /// </summary>
        /// <returns></returns>
        public static Option<T> None()
        {
            return new Option<T>(default(T), false);
        }

        /// <summary>
        /// Convert the current Option to a different Option
        /// </summary>
        /// <typeparam name="U">Type to convert to</typeparam>
        /// <param name="mapper">Function to convert T to U</param>
        /// <returns>Some if Option is a Some, None otherwise</returns>
        /// <remarks>Similiar to Select from LINQ</remarks>
        public Option<U> Map<U>(Func<T, U> mapper)
        {
            if (mapper == null) return Option<U>.None();
            return _hasValue
                ? Option<U>.Some(mapper(_value))
                : Option<U>.None();
        }

        /// <summary>
        /// Chain a function call using the current Option
        /// </summary>
        /// <typeparam name="U">Type of Option to return</typeparam>
        /// <param name="binder">Function to call if Option is Some</param>
        /// <returns>If Option is Some, returns the result of binder(value), otherwise, None </returns>
        /// <remarks>Provides a monadic approach to data validation</remarks>
        public Option<U> AndThen<U>(Func<T, Option<U>> binder)
        {
            if (binder == null) return Option<U>.None();
            return _hasValue
                ? binder(_value)
                : Option<U>.None();
        }

        /// <summary>
        /// Performs an Action on the current Option
        /// </summary>
        /// <param name="ifSome">Function to call if Option is Some</param>
        /// <param name="ifNone">Function to call if Option is None</param>
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

        /// <summary>
        /// Check if Option fulfills the filter
        /// </summary>
        /// <param name="filter">Predicate used to test Option</param>
        /// <returns>If Option is Some and the value fulfills the filter, then Some. Otherwise None</returns>
        public Option<T> Where(Func<T, bool> filter)
        {
            if (filter == null) return None();
            if (_hasValue && filter(_value)) return Some(_value);
            return None();
        }

        /// <summary>
        /// Create an option based on a function call and Option arguments
        /// </summary>
        /// <typeparam name="T1">Type of the first arugment for the func</typeparam>
        /// <typeparam name="T2">Type of the second argument for the func</typeparam>
        /// <param name="func">Function to call if all the arguments are Some</param>
        /// <param name="first">First argument for func</param>
        /// <param name="second">Second argument for the func</param>
        /// <returns>If all arguments are Some, then Some is returned. None otherwise</returns>
        /// <remarks>Provides an applicative style for data validation</remarks>
        public static Option<T> Apply<T1, T2>(Func<T1, T2, T> func, Option<T1> first, Option<T2> second)
        {
            if (func == null || first == null || second == null) return None();

            if (first._hasValue && second._hasValue)
                return Some(func(first._value, second._value));
            return None();
        }

        /// <summary>
        /// Create an option based on a function call and Option arguments
        /// </summary>
        /// <typeparam name="T1">Type of the first argument for the func</typeparam>
        /// <typeparam name="T2">Type of the second argument for the func</typeparam>
        /// <typeparam name="T3">Type of the third argument for the func</typeparam>
        /// <param name="func">Function to call if all the arguments are Some</param>
        /// <param name="first">First argument for func</param>
        /// <param name="second">Second argument for func</param>
        /// <param name="third">Third argument for func</param>
        /// <returns>If all arguments are Some, then Some is returned. Otherwise, None is returned</returns>
        /// <remarks>Provides an applicative style for data validation</remarks>
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
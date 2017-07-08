using System;

namespace Optionally
{
    /// <summary>
    /// Represents whether a value exists or not
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Option<T>
    {
        internal readonly T Value;
        internal readonly bool HasValue;
        
        private Option(T value, bool hasValue)
        {
            Value = value;
            HasValue = hasValue;
        }

        internal static readonly Option<T> None = new Option<T>(default(T), false); 
        internal static Option<T> Some(T value)
        {
            return new Option<T>(value, true);
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
            if (mapper == null) return Option<U>.None;
            return HasValue
                ? Option<U>.Some(mapper(Value))
                : Option<U>.None;
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
            if (binder == null) return Option<U>.None;
            return HasValue
                ? binder(Value)
                : Option<U>.None;
        }

        /// <summary>
        /// Performs an Action on the current Option
        /// </summary>
        /// <param name="ifSome">Function to call if Option is Some</param>
        /// <param name="ifNone">Function to call if Option is None</param>
        public void Do(Action<T> ifSome, Action ifNone)
        {
            if (HasValue)
                ifSome?.Invoke(Value);
            else
                ifNone?.Invoke();
        }

        /// <summary>
        /// Check if Option fulfills the filter
        /// </summary>
        /// <param name="filter">Predicate used to test Option</param>
        /// <returns>If Option is Some and the value fulfills the filter, then Some. Otherwise None</returns>
        public Option<T> Where(Func<T, bool> filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (HasValue && filter(Value)) return Some(Value);
            return None;
        }
    }
}
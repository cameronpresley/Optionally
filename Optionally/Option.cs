using System;

namespace Optionally
{
    using System;

    namespace Optionally
    {
        public interface IOption<T>
        {
            /// <summary>
            /// Convert the current Option to a different Option
            /// </summary>
            /// <typeparam name="U">Type to convert to</typeparam>
            /// <param name="mapper">Function to convert T to U</param>
            /// <returns>Some if Option is a Some, None otherwise</returns>
            IOption<U> Map<U>(Func<T, U> mapper);

            /// <summary>
            /// Chain a function call using the current Option
            /// </summary>
            /// <typeparam name="U">Type of Option to return</typeparam>
            /// <param name="binder">Function to call if Option is Some</param>
            /// <returns>If Option is Some, returns the result of binder(value), otherwise, None </returns>
            /// <remarks>Provides a monadic approach to data validation</remarks>
            IOption<U> AndThen<U>(Func<T, IOption<U>> binder);

            /// <summary>
            /// Performs an Action on the current Option
            /// </summary>
            /// <param name="ifNone">Function to call if Option is None</param>
            /// <param name="ifSome">Function to call if Option is Some</param>
            void Do(Action ifNone, Action<T> ifSome);

            /// <summary>
            /// Check if Option fulfills the filter
            /// </summary>
            /// <param name="filter">Predicate used to test Option</param>
            /// <returns>If Option is Some and the value fulfills the filter, then Some. Otherwise None</returns>
            IOption<T> Where(Func<T, bool> filter);

            /// <summary>
            /// Performs a Function on the current Option
            /// </summary>
            /// <typeparam name="U">Type to convert to</typeparam>
            /// <param name="ifNone">Function to call if Option is None</param>
            /// <param name="ifSome">Function to call if Option is Some</param>
            /// <returns></returns>
            U Match<U>(Func<U> ifNone, Func<T, U> ifSome);
        }

        internal struct Some<T> : IOption<T>
        {
            internal readonly T Value;

            public Some(T value)
            {
                Value = value;
            }
            public IOption<U> Map<U>(Func<T, U> mapper)
            {
                if (mapper == null) throw new ArgumentNullException(nameof(mapper));
                return new Some<U>(mapper(Value));
            }

            public IOption<U> AndThen<U>(Func<T, IOption<U>> binder)
            {
                if (binder == null) throw new ArgumentNullException(nameof(binder));
                return binder(Value);
            }

            public void Do(Action ifNone, Action<T> ifSome)
            {
                if (ifSome == null) throw new ArgumentNullException(nameof(ifSome));
                if (ifNone == null) throw new ArgumentNullException(nameof(ifNone));

                ifSome(Value);
            }

            public IOption<T> Where(Func<T, bool> filter)
            {
                if (filter == null) throw new ArgumentNullException(nameof(filter));
                return filter(Value) ? (IOption<T>)this : new None<T>();
            }

            public U Match<U>(Func<U> ifNone, Func<T, U> ifSome)
            {
                if (ifSome == null) throw new ArgumentNullException(nameof(ifSome));
                if (ifNone == null) throw new ArgumentNullException(nameof(ifNone));

                return ifSome(Value);
            }

            public override string ToString()
            {
                return $"Some of '{Value}'";
            }
        }

        internal struct None<T> : IOption<T>
        {
            public IOption<U> Map<U>(Func<T, U> mapper)
            {
                if (mapper == null) throw new ArgumentNullException(nameof(mapper));
                return new None<U>();
            }

            public IOption<U> AndThen<U>(Func<T, IOption<U>> binder)
            {
                if (binder == null) throw new ArgumentNullException(nameof(binder));
                return new None<U>();
            }

            public void Do(Action ifNone, Action<T> ifSome)
            {
                if (ifSome == null) throw new ArgumentNullException(nameof(ifSome));
                if (ifNone == null) throw new ArgumentNullException(nameof(ifNone));
                ifNone();
            }

            public IOption<T> Where(Func<T, bool> filter)
            {
                if (filter == null) throw new ArgumentNullException(nameof(filter));
                return this;
            }

            public U Match<U>(Func<U> ifNone, Func<T, U> ifSome)
            {
                if (ifSome == null) throw new ArgumentNullException(nameof(ifSome));
                if (ifNone == null) throw new ArgumentNullException(nameof(ifNone));

                return ifNone();
            }

            public override string ToString()
            {
                return "None";
            }
        }
    }
}

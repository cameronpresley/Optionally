using System;

namespace Optionally.Extensions
{
    public static class FunctionExtensions
    {
        public static Option<T> WrapInOption<T>(this Func<T> func)
        {
            try
            {
                return Option.Some(func());
            }
            catch
            {
                return Option.No<T>();
            }
        }

        public static Result<T, Exception> WrapInResult<T>(this Func<T> func)
        {
            try
            {
                return Result<T, Exception>.Success(func());
            }
            catch (Exception ex)
            {
                return Result<T, Exception>.Failure(ex);
            }
        }
    }
}

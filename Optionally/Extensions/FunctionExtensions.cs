using System;

namespace Optionally.Extensions
{
    public static class FunctionExtensions
    {
        public static IOption<T> WrapInOption<T>(this Func<T> func)
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

        public static IResult<Exception, T> WrapInResult<T>(this Func<T> func)
        {
            try
            {
                return Result.Success<Exception, T>(func());
            }
            catch (Exception ex)
            {
                return Result.Failure<Exception, T>(ex);
            }
        }
    }
}

﻿namespace Optionally.Extensions
{
    public static class NullableExtensions
    {
        /// <summary>
        /// Converts a Nullable to an Option
        /// </summary>
        /// <typeparam name="T">Type of the nullable</typeparam>
        /// <param name="nullable"></param>
        /// <returns>If nullable is null or has no value, then None. Otherwise Some</returns>
        public static IOption<T> ToOption<T>(this T? nullable) where T : struct
        {
            return nullable == null ? Option.No<T>() : Option.Some(nullable.Value);
        }
    }
}

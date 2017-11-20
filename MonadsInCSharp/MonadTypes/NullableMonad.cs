using System;
using System.Collections.Generic;
using System.Text;

namespace MonadsInCSharp.MonadTypes
{
    public static class NullableMonad
    {
        /// <summary>
        /// Wraps a T value
        /// </summary>
        public static Nullable<T> Unit<T>(T value)
            where T : struct
        {
            return new Nullable<T>(value);
        }

        /// <summary>
        /// Applies 'action' to the value inside 'target' and un-nests the wrapped T object
        /// </summary>
        public static Nullable<TResult> Bind<T, TResult>(
            this Nullable<T> target,
            Func<T, Nullable<TResult>> action)
            where T : struct
            where TResult : struct
        {
            return target.HasValue
                ? action(target.Value)
                : null;
        }












        public static Nullable<TResult> SelectMany<TSource, TResult>(
                this Nullable<TSource> source,
                Func<TSource, Nullable<TResult>> projector)
            where TSource : struct
            where TResult : struct
        {
            return Bind(source, projector);
        }

        public static Nullable<TResult> SelectMany<TSource, TNullable, TResult>(
                        this Nullable<TSource> source,
                        Func<TSource, Nullable<TNullable>> projector,
                        Func<TSource, TNullable, TResult> selector)
            where TSource : struct
            where TResult : struct
            where TNullable : struct
        {
            return source
                .Bind(s => projector(s).Bind(value => Unit(selector(s, value))));
        }
    }
}

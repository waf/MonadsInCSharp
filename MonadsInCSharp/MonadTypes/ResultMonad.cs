using System;
using System.Collections.Generic;
using System.Text;

namespace MonadsInCSharp
{
    public static class ResultMonad
    {
        /// <summary>
        /// Wraps a T object
        /// </summary>
        public static Result<TResult> Unit<TResult>(TResult obj) =>
            Result<TResult>.Ok(obj);

        /// <summary>
        /// Applies action and un-nests the wrapped T object
        /// </summary>
        public static Result<TResult> Bind<T, TResult>(
            this Result<T> target,
            Func<T, Result<TResult>> action)
        {
            return target.isOk
                ? action(target.ok)
                : Result<TResult>.Error(target.error);
        }








        public static Result<TResult> SelectMany<TSource, TResult>(
                this Result<TSource> m,
                Func<TSource, Result<TResult>> projector) =>
            Bind(m, projector);

        public static Result<TResult> SelectMany<TSource, TIntermediate, TResult>(
                        this Result<TSource> m,
                        Func<TSource, Result<TIntermediate>> projector,
                        Func<TSource, TIntermediate, TResult> selector) =>
            m.Bind(source => projector(source)
                .Bind(task => Unit(selector(source, task))));
    }
}

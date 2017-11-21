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
            Result<TResult>.Success(obj);

        /// <summary>
        /// Applies 'action' to the value inside 'target' and returns an un-nested object.
        /// </summary>
        public static Result<TResult> Bind<T, TResult>(
            this Result<T> target,
            Func<T, Result<TResult>> action)
        {
            return target.IsSuccess
                ? action(target.success)
                : Result<TResult>.Error(target.error);
        }








        public static Result<TResult> SelectMany<TSource, TResult>(
                this Result<TSource> source,
                Func<TSource, Result<TResult>> projector) =>
            Bind(source, projector);

        public static Result<TResult> SelectMany<TSource, TIntermediate, TResult>(
                        this Result<TSource> source,
                        Func<TSource, Result<TIntermediate>> projector,
                        Func<TSource, TIntermediate, TResult> selector) =>
            source
                .Bind(src => projector(src)
                    .Bind(value => Unit(selector(src, value))));
    }
}

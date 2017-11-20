using System;
using System.Collections.Generic;
using System.Text;

namespace MonadsInCSharp
{
    public static class OptionMonad
    {
        /// <summary>
        /// wraps a T object
        /// </summary>
        public static Option<T> Unit<T>(T obj) =>
            Option<T>.Some(obj);

        /// <summary>
        /// applies action and un-nests the wrapped T object
        /// </summary>
        public static Option<Result> Bind<T, Result>(
            this Option<T> target,
            Func<T, Option<Result>> action)
        {
            return target.HasValue
                ? action(target.Value)
                : Option<Result>.None;
        }







        public static Option<TResult> SelectMany<TSource, TResult>(
                this Option<TSource> m,
                Func<TSource, Option<TResult>> projector) =>
            Bind(m, projector);


        public static Option<TResult> SelectMany<TSource, TOption, TResult>(
                        this Option<TSource> m,
                        Func<TSource, Option<TOption>> projector,
                        Func<TSource, TOption, TResult> selector) =>
            m.Bind(source => projector(source)
                .Bind(task => Unit(selector(source, task))));
    }
}

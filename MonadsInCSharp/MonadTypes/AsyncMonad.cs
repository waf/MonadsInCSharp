using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonadsInCSharp
{
    public static class AsyncMonad
    {
        /// <summary>
        /// Wraps a T object
        /// </summary>
        public static Task<T> Unit<T>(T obj) =>
            Task.FromResult(obj);

        /// <summary>
        /// Applies action and un-nests the wrapped T object
        /// </summary>
        public static Task<Result> Bind<T, Result>(
            this Task<T> target,
            Func<T, Task<Result>> action) =>
            target
                .ContinueWith(obj => action(obj.Result))
                .Unwrap();









        public static Task<TResult> SelectMany<TSource, TResult>(
                this Task<TSource> m,
                Func<TSource, Task<TResult>> projector) =>
            Bind(m, projector);

        public static Task<TResult> SelectMany<TSource, TTask, TResult>(
                        this Task<TSource> m,
                        Func<TSource, Task<TTask>> projector,
                        Func<TSource, TTask, TResult> selector) =>
            m.Bind(source => projector(source)
                .Bind(task => Unit(selector(source, task))));
    }
}

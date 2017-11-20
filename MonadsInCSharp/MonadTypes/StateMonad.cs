using System;
using System.Collections.Generic;
using System.Text;

namespace MonadsInCSharp
{
    public class State<TState, TA>
    {
        public Func<TState, (TState, TA)> Computation { get; set; }
    }

    public static class State
    {
        public static State<TState, TState> Get<TState>() =>
            new State<TState, TState>
            {
                Computation = sx => (sx, sx)
            };

        public static State<TState, Unit> Set<TState>(TState state) =>
            new State<TState, Unit>
            {
                Computation = sx => (state, null)
            };
    }

    public class Unit { }

    public static class StateMonad
    {
        public static State<TState, TA> Unit<TState, TA>(TA obj) =>
            new State<TState, TA>
            {
                Computation = state => (state, obj)
            };

        public static State<TState, TB> Bind<TState, TA, TB>(
            this State<TState, TA> target,
            Func<TA, State<TState, TB>> action) =>
            new State<TState, TB>
            {
                Computation = x =>
                {
                    var stateContent = target.Computation(x);
                    return action(stateContent.Item2).Computation(stateContent.Item1);
                }
            };

        public static State<TState, TB> SelectMany<TState, TA, TB>(
            this State<TState, TA> m,
            Func<TA, State<TState, TB>> projector) =>
            Bind(m, projector);

        public static State<TState, TB> SelectMany<TState, TA, TIntermediate, TB>(
                        this State<TState, TA> m,
                        Func<TA, State<TState, TIntermediate>> projector,
                        Func<TA, TIntermediate, TB> selector) =>
            m.Bind(source => projector(source)
                .Bind(task => Unit<TState, TB>(selector(source, task))));
    }
}

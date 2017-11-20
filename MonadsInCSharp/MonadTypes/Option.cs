using System;
using System.Collections.Generic;
using System.Text;

namespace MonadsInCSharp
{
    public class Option<T>
    {
        public T Value { get; }
        public bool HasValue { get; }

        private Option(T value, bool hasValue)
        {
            HasValue = hasValue;
            if(HasValue)
            {
                Value = value;
            }
        }

        public static Option<T> Some(T value) =>
            new Option<T>(value, true);

        public static Option<T> None { get; } =
            new Option<T>(default, false);
    }
}

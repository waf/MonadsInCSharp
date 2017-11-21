using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MonadsInCSharp.Linq
{
    /// <summary>
    /// Demonstrates the "list" monad with IEnumerable.
    /// </summary>
    public class EnumerableDemo
    {
        [Fact]
        public void CombiningEnumerables_IntoPairs()
        {
            // create two instances of our container type, IEnumerable
            IEnumerable<int> enumerableA =
                Enumerable.Range(1, 3);
            IEnumerable<int> enumerableB =
                Enumerable.Range(11, 3);

            // combine these two instances
            IEnumerable<(int, int)> pairs =
                from a in enumerableA
                from b in enumerableB
                select (a, b);
        }

        [Fact]
        public void CombiningEnumerables_WithAddition()
        {
            // create two instances of our container type, IEnumerable
            IEnumerable<int> enumerableA =
                Enumerable.Range(1, 3);
            IEnumerable<int> enumerableB =
                Enumerable.Range(11, 3);

            // combine these two instances
            IEnumerable<int> sums =
                from a in enumerableA
                from b in enumerableB
                select a + b;
        }

        /// Next step, see <see cref="NullableDemo"/>
    }
}

using System;
using System.Linq;
using Xunit;
using MonadsInCSharp.MonadTypes;

namespace MonadsInCSharp.Linq
{
    public static class NullableDemo
    {
        [Fact]
        public static Nullable<int> CombiningNullables_WithAddition()
        {
            // create two instances of our container type, Nullable
            Nullable<int> nullableA = 5;
            Nullable<int> nullableB = null;

            // combine these two instances
            Nullable<int> sum =
                from a in nullableA
                from b in nullableB
                select a + b;

            return sum;
        }

        [Fact]
        public static void CombiningNullables_IntoPairs()
        {
            // create two instances of our container type, Nullable
            Nullable<int> nullableA = 5;
            Nullable<int> nullableB = null;

            // combine these two instances
            Nullable<(int a, int b)> pair =
                from a in nullableA
                from b in nullableB
                select (a, b);

            return;
        }



        // back to the slides!



        /// <summary>
        /// Returns the index of the character chr in the string sentence,
        /// or null if the character is not in the sentence.
        /// </summary>
        public static Nullable<int> GetIndexOfCharacter(string sentence, char chr)
        {
            int index = sentence.IndexOf(chr);
            return index == -1
                ? null as Nullable<int>
                : index;
        }

        /// <summary>
        /// Returns the character at the index in the string sentence,
        /// or null if the index is out of the bounds of the sentence.
        /// </summary>
        public static Nullable<char> GetCharacterAtIndex(string sentence, int index)
        {
            return index < 0 || sentence.Length <= index
                ? null as Nullable<char>
                : sentence[index];
        }

        [Fact]
        public static Nullable<char> Nullable_Composition()
        {
            string sentence = "Hello world!";

            /*// doesn't work!
            Nullable<char> result = GetCharacterAtIndex(
                sentence,
                GetIndexOfCharacter(sentence, 'H').Value + 1
            );

            Nullable<char> result = ('H'
                .GetIndexOfCharacter(sentence) + 1)
                ?.GetCharacterAtIndexExt(sentence);

            //*/

            // use our monad implementation to compose
            Nullable<char> result =
                GetIndexOfCharacter(sentence, 'H')
                .Bind(index => GetCharacterAtIndex(sentence, index + 1));

            return result;
        }

        [Fact]
        public static Nullable<char> Nullable_Composition_WithLinq()
        {
            string sentence = "Hello world!";

            Nullable<char> result =
                from index in GetIndexOfCharacter(sentence, 'H')
                from nextChar in GetCharacterAtIndex(sentence, index + 1)
                select nextChar;

            return result;
        }

        [Fact]
        public static Nullable<char> Nullable_MoreComposition()
        {
            string sentence = "Hello world!";
            Nullable<char> charToFind = NullableMonad.Unit('H');

            Nullable<char> nextChar = charToFind
                .Bind(chr => GetIndexOfCharacter(sentence, chr))
                .Bind(index => GetCharacterAtIndex(sentence, index + 1));

            return nextChar;
        }

        /// Next step, see <see cref="ResultDemo"/>
    }



    public static class Extensions
    {

        /// <summary>
        /// Returns the index of the character chr in the string sentence,
        /// or null if the character is not in the sentence.
        /// </summary>
        public static Nullable<int> GetIndexOfCharacter(this char chr, string sentence)
        {
            int index = sentence.IndexOf(chr);
            return index == -1
                ? null as Nullable<int>
                : index;
        }

        /// <summary>
        /// Returns the character at the index in the string sentence,
        /// or null if the index is out of the bounds of the sentence.
        /// </summary>
        public static Nullable<char> GetCharacterAtIndexExt(this int index, string sentence)
        {
            return index < 0 || sentence.Length <= index
                ? null as Nullable<char>
                : sentence[index];
        }
    }
}

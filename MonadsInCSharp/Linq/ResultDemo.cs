using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MonadsInCSharp
{

    public class ResultDemo
    {
        // some sample functions that throw exceptions
        static int GetIndexOfCharacterException(string sentence, char chr)
        {
            int index = sentence.IndexOf(chr);
            return index == -1
                ? throw new ArgumentException($"character {chr} is not in string {sentence}")
                : index;
        }
        static char GetCharacterAtIndexException(string sentence, int index)
        {
            return index < 0 || sentence.Length <= index
                ? throw new ArgumentOutOfRangeException($"Index {index} is out of bounds of the string {sentence}")
                : sentence[index];
        }

        // the same as above, but with the Result monad
        static Result<int> GetIndexOfCharacter(string sentence, char chr)
        {
            int index = sentence.IndexOf(chr);
            return index == -1
                ? Result<int>.Error($"character {chr} is not in string {sentence}")
                : Result<int>.Ok(index);
        }
        static Result<char> GetCharacterAtIndex(string sentence, int index)
        {
            return index < 0 || sentence.Length <= index
                ? Result<char>.Error($"Index {index} is out of bounds of the string {sentence}")
                : Result<char>.Ok(sentence[index]);
        }

        [Fact]
        public static int Demo_Exception()
        {
            string sentence = "Hello World";
            char charToFind = 'H';
            try
            {
                int index = GetIndexOfCharacterException(sentence, charToFind);
                int nextChar = GetCharacterAtIndexException(sentence, index + 1);
                return nextChar;
            }
            catch (Exception e)
            {
                // e.g. do some logging
                throw;
            }
        }

        [Fact]
        public static Result<char> Demo_WithBind()
        {
            string sentence = "Hello World";
            Result<char> charToFind = ResultMonad.Unit('H');

            Result<char> nextChar = charToFind
                .Bind(chr => GetIndexOfCharacter(sentence, chr))
                .Bind(index => GetCharacterAtIndex(sentence, index + 1));

            return nextChar;
        }


        [Fact]
        public static Result<char> Demo_WithLinq()
        {
            string sentence = "Hello World";

            Result<char> result =
                from index in GetIndexOfCharacter(sentence, 'H')
                from nextChar in GetCharacterAtIndex(sentence, index + 1)
                select nextChar;

            return result;
        }

        [Fact]
        public static Result<char> Demo_WithUnitAndBind_Linq()
        {
            string sentence = "Hello World";
            Result<char> toFind = ResultMonad.Unit('H');

            Result<char> result =
                from chr in toFind
                from index in GetIndexOfCharacter(sentence, chr)
                from nextChar in GetCharacterAtIndex(sentence, index + 1)
                select nextChar;

            return result;
        }

        /// Next step, see <see cref="AsyncDemo"/>
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace MonadsInCSharp
{
    /// <summary>
    /// This is like Nullable, but works with reference types as well as value types.
    /// </summary>
    public static class OptionDemo
    {
        /// <summary>
        /// Returns the index of the character chr in the string sentence,
        /// or null if the character is not in the sentence.
        /// </summary>
        public static Option<int> GetIndexOfCharacter(string sentence, char chr)
        {
            int index = sentence.IndexOf(chr);
            return index == -1
                ? Option<int>.None
                : Option<int>.Some(index);
        }

        /// <summary>
        /// Returns the character at the index in the string sentence,
        /// or null if the index is out of the bounds of the sentence.
        /// </summary>
        public static Option<char> GetCharacterAtIndex(string sentence, int index)
        {
            return index < 0 || sentence.Length <= index
                ? Option<char>.None
                : Option<char>.Some(sentence[index]);
        }


        // some sample nullable functions
        static int? ParseIntNullable(this string number) =>
            int.TryParse(number, out int result)
                ? result
                : null as int?;

        static int? DivideByNullable(this int number, int divisor) =>
            divisor != 0
                ? number / divisor
                : null as int?;

        // the same functions as above, but with our
        // Optional rather than the built-in Nullable
        static Option<int> ParseIntOptional(string number) =>
            int.TryParse(number, out int result)
                ? Option<int>.Some(result)
                : Option<int>.None;

        static Option<int> DivideByOptional(int number, int divisor) =>
            divisor != 0
                ? Option<int>.Some(number / divisor)
                : Option<int>.None;

        [Fact]
        public static int? Demo_Nullable()
        {
            string input = "24";

            int? divided = input
                ?.ParseIntNullable()
                ?.DivideByNullable(2);

            return divided;
        }

        [Fact]
        public static Option<int> Demo_WithUnitAndBind()
        {
            Option<string> input = OptionMonad.Unit("asdfasdf");

            Option<int> divided = input
                .Bind(number => ParseIntOptional(number))
                .Bind(parsed => DivideByOptional(parsed, 2));

            return divided;
        }











        [Fact]
        public static Option<int> Demo_WithUnitAndBind_Linq()
        {
            Option<string> input = OptionMonad.Unit("24");

            Option<int> result =
                from number in input
                from parsed in ParseIntOptional(number)
                from divided in DivideByOptional(parsed, 2)
                select divided;

            return result;
        }

        [Fact]
        public static Option<char> Demo_WithNextChar_Linq()
        {
            Option<char> input = OptionMonad.Unit('H');
            const string sentence = "Hello World!";

            Option<char> result =
                from chr in input
                from index in GetIndexOfCharacter(sentence, chr)
                from nextChar in GetCharacterAtIndex(sentence, index + 1)
                select nextChar;

            return result;
        }
    }
}
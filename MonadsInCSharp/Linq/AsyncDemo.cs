using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MonadsInCSharp
{

    public class AsyncDemo
    {
        // some sample async functions -- in the real world code,
        // these would be more complex async functions.

        private Task<int> GetIndexOfCharacter(string sentence, char chr) =>
            // for demo purposes, skipping error handling here, we'll
            // see error handling later.
            Task.FromResult(sentence.IndexOf(chr));

        private Task<char> GetCharacterAtIndex(string sentence, int index) =>
            Task.FromResult(sentence[index]);


        [Fact]
        public async Task<char> Demo_AsyncAwait()
        {
            string sentence = "Hello world!";
            Task<char> charToFind = Task.FromResult('H');

            char chr = await charToFind;
            int index = await GetIndexOfCharacter(sentence, chr);
            char nextChar = await GetCharacterAtIndex(sentence, index);

            return nextChar;
        }

        [Fact]
        public Task<char> Demo_WithUnitAndBind()
        {
            string sentence = "Hello world!";
            Task<char> charToFind = AsyncMonad.Unit('H');

            Task<char> divided = charToFind
                .Bind(chr => GetIndexOfCharacter(sentence, chr))
                .Bind(index => GetCharacterAtIndex(sentence, index));

            return divided;
        }


        [Fact]
        public Task<char> Demo_Linq()
        {
            string sentence = "Hello world!";
            Task<char> charToFind = AsyncMonad.Unit('H');

            Task<char> result =
                from chr in charToFind
                from index in GetIndexOfCharacter(sentence, chr)
                from nextChar in GetCharacterAtIndex(sentence, index)
                select nextChar;

            /* similar to Haskell...
               do chr <- charToFind
                  index <- GetIndexOfCharacter sentence chr
                  nextChar <- GetCharacterAtIndex sentence index
                  return nextChar
             */

            return result;
        }

        // back to the slides!
    }
}

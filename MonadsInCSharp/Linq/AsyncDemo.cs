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

        private Task<int> GetIndexOfCharacterAsync(string sentence, char chr) =>
            // for demo purposes, skipping error handling here.
            Task.FromResult(sentence.IndexOf(chr));

        private Task<char> GetCharacterAtIndexAsync(string sentence, int index) =>
            Task.FromResult(sentence[index]);


        [Fact]
        public async Task<char> Demo_AsyncAwait()
        {
            string sentence = "Hello world!";
            Task<char> charToFind = Task.FromResult('H');

            char chr = await charToFind;
            int index = await GetIndexOfCharacterAsync(sentence, chr);
            char nextChar = await GetCharacterAtIndexAsync(sentence, index);

            return nextChar;
        }

        [Fact]
        public Task<char> Demo_WithUnitAndBind()
        {
            string sentence = "Hello world!";
            Task<char> charToFind = AsyncMonad.Unit('H');

            Task<char> divided = charToFind
                .Bind(chr => GetIndexOfCharacterAsync(sentence, chr))
                .Bind(index => GetCharacterAtIndexAsync(sentence, index));

            return divided;
        }


        [Fact]
        public Task<char> Demo_Linq()
        {
            string sentence = "Hello world!";
            Task<char> charToFind = AsyncMonad.Unit('H');

            Task<char> result =
                from chr in charToFind
                from index in GetIndexOfCharacterAsync(sentence, chr)
                from nextChar in GetCharacterAtIndexAsync(sentence, index)
                select nextChar;

            /* similar to 'do notation' Haskell...
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

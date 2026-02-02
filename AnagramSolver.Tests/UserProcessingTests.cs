using System.Text;
using AnagramSolver.BusinessLogic;
using Xunit;
using System.Collections.Generic;
using FluentAssertions;


namespace AnagramSolver.BusinessLogic.Tests
{
    public class UserProcessingTests
    {
        [Fact]
        public void GetWords_ShouldSplitWordsBySpace()
        {
            string words = "labas rytas";
            var up = new UserProcessing(words);

            var result = up.GetWords();

            result.Should().BeEquivalentTo(new[] {"labas", "rytas"});
        }

        [Fact]
        public void GetWords_ShouldConvertWordsToLowerCase()
        {
            string words = "LAbAS rYtAs";
            var up = new UserProcessing(words);

            var result = up.GetWords();

            result.Should().BeEquivalentTo(new[] { "labas", "rytas" });
        }

        [Fact]
        public void GetWords_WhenInputEmpty_ShouldReturnEmptyList()
        {
            string words = "";
            var up = new UserProcessing(words);

            var result = up.GetWords();

            result.Should().BeEmpty();
        }

        [Fact]
        public void CombineLetters_ShouldReturnOneString()
        {
            string words = "labas rytas";
            var up = new UserProcessing(words);
            var split = up.GetWords();

            var result = up.CombineLetters(split);
            
            result.Should().Be("labasrytas");
        }

        [Fact]
        public void CombineLetters_WhenInputEmpty_ShouldReturnEmptyString()
        {
            string words = "";
            var up = new UserProcessing(words);
            var split = up.GetWords();

            var result = up.CombineLetters(split);

            result.Should().BeEmpty();
        }

        [Fact]
        public void GetSortedLetters_ShouldReturnJoinedSortedLetters()
        {
            string words = "labas rytas";
            var up = new UserProcessing(words);
            var split = up.GetWords();

            var result = up.GetSortedLetters(split);

            result.Should().Be("aaablrssty");
        }

        [Fact]
        public void GetSortedLetters_WhenInputEmpty_ShouldReturnEmptyList()
        {
            string words = "";
            var up = new UserProcessing(words);
            var split = up.GetWords();

            var result = up.GetSortedLetters(split);

            result.Should().BeEmpty();
        }
    }
}

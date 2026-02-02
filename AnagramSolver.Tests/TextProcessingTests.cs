using System.Text;
using AnagramSolver.BusinessLogic;
using Xunit;
using System.Collections.Generic;
using FluentAssertions;

namespace AnagramSolver.BusinessLogic.Tests
{
    public class TextProcessingTests
    {
        [Fact]
        public void BuildAnagramMap_ShouldGroupWordsBySortedLetters()
        {
            int maxTxtLen = 1;
            var tp = new TextProcessing("unused", maxTxtLen);

            var words = new List<string> { "tarka", "karta", "dcba" };

            tp.BuildAnagramMap(words);

            var map = tp.GetAnagramMap();

            map.Should().ContainKey("aakrt");
            map["aakrt"].Should().Contain(new[] { "tarka", "karta" });
            map.Should().ContainKey("abcd");
            map["abcd"].Should().Contain(new[] { "dcba" });
        }

        [Fact]
        public void BuildAnagramMap_WhenEmptyInput_ShouldReturnEmptyList()
        {
            int maxTxtLen = 1;
            var tp = new TextProcessing("unused", maxTxtLen);

            var words = new List<string>();

            tp.BuildAnagramMap(words);

            var map = tp.GetAnagramMap();

            map.Should().BeEmpty();
        }

        [Fact]
        public void BuildAnagramMap_ClearsPreviousData()
        {
            int maxTxtLen = 1;
            var tp = new TextProcessing("unused", maxTxtLen);

            var words1 = new List<string> { "tarka", "karta" };
            tp.BuildAnagramMap(words1);

            var words2 = new List<string> { "bcda" };
            tp.BuildAnagramMap(words2);

            var map = tp.GetAnagramMap();

            map.Should().NotContainKey("aakrt");
            map.Should().ContainKey("abcd");
        }

        [Fact]
        public void BuildAnagramMap_ShouldNotGroupDifferentAnagrams()
        {
            int maxTxtLen = 1;
            var tp = new TextProcessing("unused", maxTxtLen);

            var words = new List<string> { "tarka", "dabc" };

            tp.BuildAnagramMap(words);

            var map = tp.GetAnagramMap();

            map.Should().ContainKey("aakrt");
            map.Should().ContainKey("abcd");
            map["aakrt"].Should().NotContain("dabc");
            map["abcd"].Should().NotContain("tarka");
        }
    }
}

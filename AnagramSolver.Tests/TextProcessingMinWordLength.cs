using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic;
using FluentAssertions;

namespace AnagramSolver.BusinessLogic.Tests
{
    public class TextProcessingMinWordLength
    {
        [Fact]
        public void BuildAnagramMap_ShouldIgnoreWordsShorterThanMinLength()
        {
            int minLen = 4;
            var tp = new TextProcessing("unused", minLen);

            var words = new List<string>
            {
                "aa", "labas", "rytas", "aaa"

            };

            tp.BuildAnagramMap(words);

            var map = tp.GetAnagramMap();
            var allWordsInMap = map.SelectMany(keyValue => keyValue.Value).ToList();

            allWordsInMap.Should().NotContain("aa");
            allWordsInMap.Should().Contain("labas");
            allWordsInMap.Should().Contain("rytas");
            allWordsInMap.Should().NotContain("aaa");
        }

        [Fact]

        public void BuildAnagram_WhenInputShorterThanMinLength_ShouldReturnEmptyList()
        {
            int minLen = 4;
            var tp = new TextProcessing("unused", minLen);

            var words = new List<string>
            {
                "aa", "bbb"
            };

            tp.BuildAnagramMap(words);

            var map = tp.GetAnagramMap();

            map.Should().BeEmpty();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using FluentAssertions;
using Moq;

namespace AnagramSolver.BusinessLogic.Tests
{
    public class AnagramSolverMockTests
    {
        [Fact]
        public void GetAnagrams_ShouldWork_WithMockedRepository_NoFile()
        {
            var repoMock = new Mock<IWordRepository>();
            repoMock.Setup(r => r.GetAllWords()).Returns(new List<string>
            {
                "visma",
                "praktika"
            });

            int maxTxtLen = 1;

            var tp = new TextProcessing("unused", maxTxtLen);
            tp.BuildAnagramMap(repoMock.Object.GetAllWords());
            var map = tp.GetAnagramMap();

            int maxResults = 10;
            int anagramOutput = 2;

            IAnagramSolver solver = new AnagramProcessing(map, maxResults, anagramOutput);

            var input = "aaaiikkmprstv";

            var results = solver.GetAnagrams(input);

            results.Should().Contain(r => r == "visma praktika" || r == "praktika visma");
        }

        [Fact]
        public void GetAnagrams_ShouldExpectMaxResults_WithMockedRepository()
        {
            var repoMock = new Mock<IWordRepository>();
            repoMock.Setup(r => r.GetAllWords()).Returns(new List<string>
            {
                "abc", "bca", "cab", "def", "fed", "edf"
            });

            int maxTxtLen = 1;

            var tp = new TextProcessing("unused", maxTxtLen);
            tp.BuildAnagramMap(repoMock.Object.GetAllWords());
            var map = tp.GetAnagramMap();

            int maxResults = 3;
            int anagramOutput = 2;

            IAnagramSolver solver = new AnagramProcessing(map, maxResults, anagramOutput);

            var input = "abcdef";

            var results = solver.GetAnagrams(input);

            results.Count.Should().BeLessThanOrEqualTo(maxResults);
        }
    }
}

using System.Text;
using AnagramSolver.BusinessLogic;
using Xunit;
using System.Collections.Generic;
using FluentAssertions;

namespace AnagramSolver.BusinessLogic.Tests
{
    public class AnagramProcessingTests
    {
        [Fact]
        public void GetAnagrams_ShouldReturnTwoWordAnagram()
        {
            var map = new Dictionary<string, List<string>>
            {
                ["aimsv"] = new List<string> {"visma"},
                ["aaikkprt"] = new List<string> { "praktika"}
            };

            int maxResults = 10;

            int anagramOutput = 2;

            var solver = new AnagramProcessing(map, maxResults, anagramOutput);

            var input = "aaaiikkmprstv";

            var results = solver.GetAnagrams(input);

            results.Should().Contain("visma praktika");

            
        }

        [Fact]
        public void GetAnagrams_ShouldReturnSingleWordAnagram()
        {
            var map = new Dictionary<string, List<string>>
            {
                ["aimsv"] = new List<string> { "visma" }
            };

            int maxResults = 10;

            int anagramOutput = 1;

            var solver = new AnagramProcessing(map, maxResults, anagramOutput);

            var input = "aimsv";

            var results = solver.GetAnagrams(input);

            results.Should().Contain("visma");
        }

        [Fact]
        public void GetAnagrams_WhenEmptyInput_ShouldReturnEmptyList()
        {
            var map = new Dictionary<string, List<string>>();

            int maxResults = 10;

            int anagramOutput = 10;

            var solver = new AnagramProcessing(map, maxResults, anagramOutput);

            var input = "";

            var results = solver.GetAnagrams(input);

            results.Should().BeEmpty();
        }
        [Fact]
        public void GetAnagrams_WhenNoMatchingAnagrams_ShouldReturnEmptyList()
        {
            var map = new Dictionary<string, List<string>>();

            int maxResults = 10;

            int anagramOutput = 10;

            var solver = new AnagramProcessing(map, maxResults, anagramOutput);

            var input = "aaaaaaaaaaaaaaaaaaaa";

            var results = solver.GetAnagrams(input);

            results.Should().BeEmpty();
        }

        [Fact]
        public void GetAnagrams_WhenMoreThanMaxResults_ShouldReturnMaxResults()
        {
            var map = new Dictionary<string, List<string>>
            {
                ["abc"] = new List<string> { "bca", "cab", "bac" },
                ["def"] = new List<string> { "fed", "edf", "efd"}
            };
            int maxResults = 2;
            int anagramOutput = 2;

            var solver = new AnagramProcessing(map, maxResults, anagramOutput);

            var input = "abcdef";

            var results = solver.GetAnagrams(input);

            results.Count.Should().BeLessThanOrEqualTo(maxResults);
        }

    }
}
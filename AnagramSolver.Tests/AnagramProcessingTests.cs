using System.Text;
using AnagramSolver.BusinessLogic;
using Xunit;
using System.Collections.Generic;
using FluentAssertions;

namespace AnagramSolver.Tests
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

            var solver = new AnagramProcessing(map, maxResults);

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

            var solver = new AnagramProcessing(map, maxResults);

            var input = "aimsv";

            var results = solver.GetAnagrams(input);

            results.Should().Contain("visma");
        }

        [Fact]
        public void GetAnagrams_WhenEmptyInput_ShouldReturnEmptyList()
        {
            var map = new Dictionary<string, List<string>>();

            int maxResults = 10;

            var solver = new AnagramProcessing(map, maxResults);

            var input = "";

            var results = solver.GetAnagrams(input);

            results.Should().BeEmpty();
        }
        [Fact]
        public void GetAnagrams_WhenNoMatchingAnagrams_ShouldReturnEmptyList()
        {
            var map = new Dictionary<string, List<string>>();

            int maxResults = 10;

            var solver = new AnagramProcessing(map, maxResults);

            var input = "aaaaaaaaaaaaaaaaaaaa";

            var results = solver.GetAnagrams(input);

            results.Should().BeEmpty();
        }

    }
}
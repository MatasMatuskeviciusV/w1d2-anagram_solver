using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnagramSolver.Contracts;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramProcessing : IAnagramSolver
    {
        private Dictionary<string, List<string>> _map;
        private readonly int _maxResults;
        private readonly int _anagramOutput;

        private Dictionary<string, int[]> _keyCounts = new();
        private int _alphabetSize;
        private Dictionary<char, int> _charIndex = new();
        private List<string> _keys;

        public AnagramProcessing(Dictionary<string, List<string>> map, int maxResults, int anagramOutput)
        {
            _map = map;
            _maxResults = maxResults;
            _anagramOutput = anagramOutput;
        }


        public IList<string> GetAnagrams(string myWords)
        {
            var results = new List<string>();

            BuildAlphabet(myWords);

            _keyCounts.Clear();

            _keys = _map.Keys.ToList();

            foreach (var key in _keys)
            {
                _keyCounts[key] = BuildCounts(key);
            }

            var inputCounts = BuildCounts(myWords);

            for(int target = 1; target <= _anagramOutput; target++)
            {
                var current = new List<string>();
                SearchExact(inputCounts, myWords.Length, current, results, target);

                if(results.Count >= _maxResults)
                {
                    return results;
                }
            }

            return results;
        }

        private void SearchExact(int[] remainingCounts, int remainingLetters, List<string> currentWords, List<string> results, int target)
        {
            if (results.Count >= _maxResults)
            {
                return;
            }

            if (remainingLetters == 0)
            {
                if (currentWords.Count == target)
                {
                    results.Add(string.Join(" ", currentWords));
                }

                return;
            }

            if(currentWords.Count >= target)
            {
                return;
            }

            foreach (var key in _keys)
            {
                if (key.Length > remainingLetters)
                {
                    continue;
                }

                if (!CanSubtract(remainingCounts, _keyCounts[key]))
                {
                    continue;
                }

                var newRemaining = Subtract(remainingCounts, _keyCounts[key]);

                foreach (var w in _map[key])
                {
                    currentWords.Add(w);
                    SearchExact(newRemaining, remainingLetters - key.Length, currentWords, results, target);
                    currentWords.RemoveAt(currentWords.Count - 1);

                    if(results.Count >= _maxResults)
                    {
                        return;
                    }
                }
            }
        }  

        private void BuildAlphabet(string userInput)
        {
            _charIndex.Clear();

            var set = new HashSet<char>();

            foreach(var key in _map.Keys)
            {
                foreach (var c in key)
                {
                    set.Add(c);
                }
            }

            foreach (var c in userInput)
            {
                set.Add(c);
            }

            int index = 0;

            foreach(var c in set)
            {
                _charIndex[c] = index++;
            }

            _alphabetSize = index;
        }

        private int[] BuildCounts(string s)
        {
            var counts = new int[_alphabetSize];

            foreach (char c in s)
            {
                counts[_charIndex[c]]++;
            }
            return counts;
        }

        private bool CanSubtract(int[] remaining, int[] remove)
        {
            for(int i = 0; i < remaining.Length; i++)
            {
                if (remove[i] > remaining[i])
                {
                    return false;
                }
            }

            return true;
        }

        private int[] Subtract(int[] remaining, int[] remove)
        {
            var result = new int[remaining.Length];
            
            for(int i = 0; i < remaining.Length; i++)
            {
                result[i] = remaining[i] - remove[i];
            }

            return result;
        }

    }
}

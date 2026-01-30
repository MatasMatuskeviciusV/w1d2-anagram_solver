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
        private int _maxResults;

        public AnagramProcessing(Dictionary<string, List<string>> map, int maxResults)
        {
            _map = map;
            _maxResults = maxResults;
        }

        public IList<string> GetAnagrams(string myWords)
        {
            var results = new List<string>();

            if (_map.TryGetValue(myWords, out var singleWords))
            {
                foreach(var w in singleWords)
                {
                    results.Add(w);
                    if(results.Count >= _maxResults)
                    {
                        return results;
                    }
                }
            }

            foreach(var key1 in _map.Keys)
            {
                if(key1.Length > myWords.Length)
                {
                    continue;
                }

                string remaining = RemoveLetters(myWords, key1);

                if(remaining == null)
                {
                    continue;
                }

                if (!_map.TryGetValue(remaining, out var secondWords))
                {
                    continue;
                }

                if(AddCombinations(_map[key1], secondWords, results, _maxResults))
                {
                    return results;
                }
            }
            return results;
        }

        private bool AddCombinations(List<string> firstWords, List<string> secondWords, List<string> results, int maxResults)
        {
            var combos = firstWords.SelectMany(w1 => secondWords, (w1, w2) => $"{w1} {w2}");

            foreach(var c in combos)
            {
                results.Add(c);
                if (results.Count >= maxResults)
                {
                    return true;
                }
            }
            return false;
        }

        private string RemoveLetters(string input, string removeKey)
        {
            string used = input;

            foreach(char c in removeKey)
            {
                int Index = used.IndexOf(c);
                if(Index == -1)
                {
                    return null;
                }

                used = used.Remove(Index, 1);
            }
            return used;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnagramSolver.Contracts;

namespace AnagramSolver.BusinessLogic
{
    public class anagramProcessing : IAnagramSolver
    {
        private Dictionary<string, List<string>> _map;
        private int _maxResults;

        public anagramProcessing(Dictionary<string, List<string>> map, int maxResults)
        {
            _map = map;
            _maxResults = maxResults;
        }

        public IList<string> GetAnagrams(string myWords)
        {
            var results = new List<string>();

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

                if (_map.ContainsKey(remaining))
                {
                    foreach(var w1 in _map[key1])
                    {
                        foreach(var w2 in _map[remaining])
                        {
                            results.Add(w1 + " " + w2);
                            if (results.Count >= _maxResults)
                            {
                                return results;
                            }
                        }
                    }
                }
            }
            return results;
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

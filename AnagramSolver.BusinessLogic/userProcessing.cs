using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic
{
    public class UserProcessing
    {
        private string _input;

        public UserProcessing(string input)
        {
            _input = input;
        }

        public List<string> GetWords()
        {
            return _input.ToLower().Split(' ').ToList();
        }

        public string CombineLetters(List<string> words)
        {
            return string.Join("", words);
        }

        public string GetSortedLetters(List<string> words)
        {
            var combined = CombineLetters(words);

            char[]arr = combined.ToArray();
            Array.Sort(arr);

            return new string(arr);
        }
    }
}

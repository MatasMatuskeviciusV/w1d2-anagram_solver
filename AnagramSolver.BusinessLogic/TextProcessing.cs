using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using AnagramSolver.Contracts;

namespace AnagramSolver.BusinessLogic
{
    public class TextProcessing : IWordRepository
    {
        private string _filePath;
        private int _minTxtLength;
        private List<string> _lines = new();
        private HashSet<string> _distinctWords = new();
        private Dictionary<string, List<string>> _anagramMap = new();

        public TextProcessing(string filePath, int minTxtLength)
        {
            _filePath = filePath;
            _minTxtLength = minTxtLength;
        }

     
        public void Reading()
        {
            _lines = File.ReadAllLines(_filePath, Encoding.UTF8).ToList();
            _distinctWords.Clear();    

            foreach (var line in _lines)
            {
                var word = line.Trim().ToLower();
                if(word.Length < _minTxtLength)
                {
                    continue;
                }
                
                _distinctWords.Add(word);
            }
        }

        public List<string> GetAllWords()
        {
            return _distinctWords.ToList();
        }

        private string SortLetters(string word)
        {
            char[] arr = word.ToCharArray();
            Array.Sort(arr);
            return new string(arr);
        }

        public void BuildAnagramMap(List<string> words) 
        { 
           _anagramMap.Clear();

            foreach(var word in words)
            {
                if (word.Length < _minTxtLength)
                {
                    continue;
                }

                var key = SortLetters(word);

                if (!_anagramMap.ContainsKey(key))
                {
                    _anagramMap[key] = new List<string>();
                }

                _anagramMap[key].Add(word);
            }
        }

        public Dictionary<string, List<string>> GetAnagramMap()
        {
            return _anagramMap;
        }
    }
}

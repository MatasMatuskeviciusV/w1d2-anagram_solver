using System.Text;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;

namespace AnagramSolver.Cli
{
    class Program
    {
        static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            var configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json").Build();
            int _minLength = int.Parse(configuration["minLength"]);
            int _maxResults = int.Parse(configuration["maxResults"]);

            string dictPath = @"C:\Users\matas.matuskevicius\source\repos\Anagram_2.1\AnagramSolver\zodynas.txt";

            var textProcessing = new TextProcessing(dictPath);

            IWordRepository wordRepository = textProcessing;

            textProcessing.Reading();
            var allWords = wordRepository.GetAllWords();
            textProcessing.BuildAnagramMap(allWords);
            var anagramMap = textProcessing.GetAnagramMap();

            Console.WriteLine("Enter words: ");
            string input = Console.ReadLine();

            var userProcessing = new UserProcessing(input);
            var words = userProcessing.GetWords();

            foreach(var word in words)
            {
                if(word.Length < _minLength)
                {
                    Console.WriteLine($"{word} is too short. Min length is {_minLength}.");
                    return;

                }
            }

            var sortedLetters = userProcessing.GetSortedLetters(words);

            var anagramProcessing = new AnagramProcessing(anagramMap, _maxResults);
            var results = anagramProcessing.GetAnagrams(sortedLetters);

            if(results.Count == 0)
            {
                Console.WriteLine("No anagrams found.");
            }

            else
            {
                Console.WriteLine("Anagrams: ");
                foreach(var word in results)
                {
                    Console.WriteLine(word);
                }
            }


        }
    }
}
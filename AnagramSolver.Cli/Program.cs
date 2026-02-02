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

            string dictPath = @"C:\Users\matas.matuskevicius\source\repos\Anagram_2.1\AnagramSolver\zodynas.txt";
            string appSettingsPath = @"C:\Users\matas.matuskevicius\source\repos\Anagram_2.1\AnagramSolver";

            var configuration = new ConfigurationBuilder().SetBasePath(appSettingsPath).AddJsonFile("appsettings.json").Build();
            int _minLength = int.Parse(configuration["minUserInputLength"]);
            int _maxResults = int.Parse(configuration["maxResults"]);
            int _anagramOutput = int.Parse(configuration["anagramOutput"]);
            int _maxTxtLength = int.Parse(configuration["minTxtInputLength"]);

            var textProcessing = new TextProcessing(dictPath, _maxTxtLength);

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
                    Console.WriteLine($"Your word is too short. Min length is {_minLength}.");
                    return;

                }
            }

            var sortedLetters = userProcessing.GetSortedLetters(words);

            var anagramProcessing = new AnagramProcessing(anagramMap, _maxResults, _anagramOutput);
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
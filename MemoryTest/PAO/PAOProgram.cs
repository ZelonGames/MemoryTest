using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest.PAO
{
    public static class PAOProgram
    {
        public static async Task RunPractice(string themeCharacters)
        {
            var letterDigit = await PAOData.LoadPAO("Files/LetterDigit.json");
            var paoPractice = new PAOPractice(letterDigit!);
            paoPractice.StoryToNumber(themeCharacters);
            Console.ReadLine();
        }
        public static async Task RunGetStory(string sequence)
        {
            var paoData = await PAOData.LoadPAO("Files/ChessPAO.json");
            string a = paoData!.GetLongStoryFromSequence(sequence);
            Console.WriteLine(a);
            Console.ReadLine();
        }

        public static async Task RunGetWordsFromNumberLoop(bool strictMatch, bool use3DigitSystem)
        {
            string? input = null;
            var numberToWordConverter = new NumberToWordConverter();
            await numberToWordConverter.LoadWords();

            while (true)
            {
                input = Console.ReadLine();

                do
                {
                    if (input != null)
                    {
                        Console.Clear();
                        GetWordsFromNumber(numberToWordConverter, input, strictMatch, use3DigitSystem);
                        Console.WriteLine(input);
                    }
                } while (input == null);
            }
        }


        private static void GetWordsFromNumber(NumberToWordConverter numberToWordConverter, string number, bool strictMatch, bool useVowelSystem)
        {
            var matchingWords = numberToWordConverter.GetWordsMatchingNumber(number, strictMatch, useVowelSystem);

            int amountOfSyllables = 0;
            foreach (string word in matchingWords.strictWords.OrderBy(x => x.GetAmountOfSyllablesInPhoneticWord(numberToWordConverter)))
            {
                int newAmountOfSyllables = word.GetAmountOfSyllablesInPhoneticWord(numberToWordConverter);
                if (amountOfSyllables < newAmountOfSyllables)
                {
                    amountOfSyllables = newAmountOfSyllables;
                    Console.WriteLine("\n" + amountOfSyllables);
                }
                Console.WriteLine(word);
            }

            Console.WriteLine("\n----");

            amountOfSyllables = 0;
            foreach (string word in matchingWords.nonStrictWords.OrderBy(x => x.GetAmountOfSyllablesInPhoneticWord(numberToWordConverter)))
            {
                int newAmountOfSyllables = word.GetAmountOfSyllablesInPhoneticWord(numberToWordConverter);
                if (amountOfSyllables < newAmountOfSyllables)
                {
                    amountOfSyllables = newAmountOfSyllables;
                    Console.WriteLine("\n" + amountOfSyllables);
                }
                Console.WriteLine(word);
            }
        }

        public static async Task RunGetWordsFromNumbers(bool strictMatch, string[] numbers)
        {
            var numberConverter = new NumberToWordConverter();
            await numberConverter.LoadWords();
            var words = new Dictionary<string, string>();

            foreach (string number in numbers)
            {
                foreach (string matchingWord in numberConverter.GetWordsMatchingNumber(number, strictMatch).strictWords)
                    words.TryAdd(matchingWord, number);
            }

            foreach (var word in words.OrderBy(x => Convert.ToInt32(x.Value)))
                Console.WriteLine($"[{word.Value}] {word.Key}");

            Console.ReadLine();
        }
    }
}

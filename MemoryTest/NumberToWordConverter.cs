using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MemoryTest
{
    public class NumberToWordConverter
    {
        public Dictionary<string, string> PhoneticWords { get; private set; }

        public NumberToWordConverter()
        {
            PhoneticWords = new Dictionary<string, string>();
        }

        public async Task LoadWords()
        {
            string[] words = await File.ReadAllLinesAsync("Files/svenska-ipa.txt");

            foreach (string word in words)
            {
                string[] wordComponents = word.Split('\t');
                string normalSpelling = wordComponents[0].Replace("-", "");
                string phoneticSpelling = wordComponents[1].Split(",")[0];
                if (normalSpelling.Split(' ').Length > 1)
                    continue;

                // The last character in phoneticSpelling is a '/'
                if (phoneticSpelling[^3..^1] == "ɛr" && normalSpelling[^1..] == "a")
                    phoneticSpelling = phoneticSpelling[..^3] + "a" + phoneticSpelling[^1..];

                PhoneticWords.TryAdd(normalSpelling, phoneticSpelling);
            }
        }

        public (List<string> strictWords, List<string> nonStrictWords) GetWordsMatchingNumber(string number, bool strictMatch, bool useVowelSystem = false)
        {
            var strictWords = new List<string>();
            var nonStrictWords = new List<string>();

            foreach (var word in PhoneticWords)
            {
                bool isWordNotStrictlyMatched = false;
                bool isWordMatchingNumber = useVowelSystem ?
                    IsWordMatchingNumberVowelSystem(word.Key, number) :
                    IsWordMatchingNumber(word.Key, number, strictMatch, out isWordNotStrictlyMatched);
                if (isWordMatchingNumber)
                {
                    if (isWordNotStrictlyMatched)
                        nonStrictWords.Add(word.Key);
                    else
                        strictWords.Add(word.Key);
                }
            }

            return (strictWords, nonStrictWords);
        }

        private static (string importantDigits, string ignoredDigits) GetNumberData(string number)
        {
            string[] components = number.Split('?');
            string importantDigits = components[0];
            if (components.Length == 1)
                return (importantDigits, "");

            string ignoredDigits = components[1];
            return (importantDigits, ignoredDigits);
        }

        public bool IsWordMatchingNumber(string word, string number, bool strictMatch, out bool isWordNotStrictlyMatched)
        {
            isWordNotStrictlyMatched = false;
            if (!PhoneticWords.ContainsKey(word))
                return false;

            var numberData = GetNumberData(number);
            string importantDigits = numberData.importantDigits;
            string ignoredDigits = numberData.ignoredDigits;

            int currentDigitIndex = 0;
            string matchingCharacters = "";

            foreach (char character in PhoneticWords[word])
            {
                bool isPhoneticConsonant = character.IsPhoneticConsonant();

                if (!isPhoneticConsonant)
                    continue;

                if (currentDigitIndex >= importantDigits.Length)
                {
                    if (!strictMatch)
                        isWordNotStrictlyMatched = true;
                    return !strictMatch;
                }

                char currentDigit = importantDigits[currentDigitIndex];

                if (isPhoneticConsonant)
                {
                    bool shouldIgnoreSound = false;
                    foreach (char ignoredDigit in ignoredDigits)
                    {
                        if (SoundHelper.majorPhoneticSounds[ignoredDigit].Any(x => x == character))
                        {
                            shouldIgnoreSound = true;
                            break;
                        }
                    }

                    if (shouldIgnoreSound)
                        continue;

                    if (!SoundHelper.majorPhoneticSounds[currentDigit].Any(x => x == character))
                        return false;

                    matchingCharacters += currentDigit;
                    currentDigitIndex++;
                }
                else
                    return false;
            }

            return matchingCharacters == importantDigits;
        }

        public bool IsWordMatchingNumberVowelSystem(string word, string number)
        {
            if (!PhoneticWords.ContainsKey(word))
                return false;

            number = number.ToLower();
            int currentDigitIndex = 0;
            int matchingConsonantSounds = 0;

            foreach (char character in PhoneticWords[word])
            {
                bool isPhoneticConsonant = character.IsPhoneticConsonant();
                bool isPhoneticVowel = character.IsPhoneticVowel();
                bool isPhoneticSound = currentDigitIndex is 0 or 2 && isPhoneticConsonant || currentDigitIndex == 1 && isPhoneticVowel;

                if (!isPhoneticSound)
                    continue;

                if (currentDigitIndex >= word.Length)
                    return false;

                if (currentDigitIndex == 3)
                    return matchingConsonantSounds == 2;

                Dictionary<char, string> phoneticSoundList =
                    isPhoneticConsonant ? SoundHelper.majorPhoneticSounds : SoundHelper.vowelPhoneticSounds;
                char currentCharacter = number[currentDigitIndex];

                if (isPhoneticSound && phoneticSoundList[currentCharacter].Any(x => x == character))
                {
                    currentDigitIndex++;
                    if (isPhoneticConsonant)
                        matchingConsonantSounds++;
                }
                else
                    return false;
            }

            return currentDigitIndex == 3 && matchingConsonantSounds == 2;
        }
    }
}

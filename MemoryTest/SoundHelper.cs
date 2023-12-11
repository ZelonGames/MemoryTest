using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest
{
    public static class SoundHelper
    {
        public static readonly Dictionary<char, string> majorPhoneticSounds = new()
        {
            { '0', "sz" },
            { '1', "tdʈɖ" },
            { '2', "nŋɳ" },
            { '3', "m" },
            { '4', "r" },
            { '5', "lɭ" },
            { '6', "jʝʃɧʂɕ" },
            { '7', "kg" },
            { '8', "fv" },
            { '9', "pb" },
        };

        public static readonly Dictionary<char, string> vowelPhoneticSounds = new()
        {
            { '0', "oå" }, // o ser ut som 0 och å låter nästan som o
            { '1', "i" }, // i ser ut som en 1:a
            { '2', "aɑ" }, // 2 ser ut som ett slarvigt a
            { '3', "eɛä" }, // E ser ut som en bakvänd 3:a
            { '4', "y" }, // fyyyyyra
            { '5', "ʉ" }, // uuut
            { '6', "ɪ" }, // sitt
            { '7', "ʏ" }, // skytt
            { '8', "öø" }, // 8 roterad 90 grader ser ut som två prickar på ö
            { '9', "ɵʊɔuœɵ" },
        };

        public static int GetAmountOfSyllablesInPhoneticWord(this string word, NumberToWordConverter numberToWordConverter)
        {
            if (!numberToWordConverter.PhoneticWords.ContainsKey(word))
                return 0;

            return numberToWordConverter.PhoneticWords[word].Where(x => x.IsPhoneticVowel()).Count();
        }

        public static char GetConsonant(this string word, int consonantIndex)
        {
            int foundConsonants = 0;
            foreach (char letter in word)
            {
                if (letter.IsConsonant())
                {
                    if (foundConsonants == consonantIndex)
                        return letter;

                    foundConsonants++;
                }
            }

            return ' ';
        }

        public static char GetVowel(this string word, int vowelIndex)
        {
            int foundVowels = 0;
            foreach (char letter in word)
            {
                if (letter.IsVowel())
                {
                    if (foundVowels == vowelIndex)
                        return letter;

                    foundVowels++;
                }
            }

            return ' ';
        }

        public static bool IsPhoneticConsonant(this char character)
        {
            if (IsVowel(character))
                return false;

            if (IsConsonant(character))
                return true;

            foreach (string sound in majorPhoneticSounds.Values)
            {
                if (sound.Any(x => x == character))
                    return true;
            }

            return false;
        }

        public static bool IsPhoneticVowel(this char character)
        {
            if (IsVowel(character))
                return true;

            if (IsConsonant(character))
                return false;

            foreach (string sound in vowelPhoneticSounds.Values)
            {
                if (sound.Any(x => x == character))
                    return true;
            }

            return false;
        }

        public static bool IsVowel(this char character)
        {
            return character is
                'a' or 'e' or 'i' or 'o' or 'u' or 'y' or 'å' or 'ä' or 'ö' or
                'A' or 'E' or 'I' or 'O' or 'U' or 'Y' or 'Å' or 'Ä' or 'Ö';
        }

        public static bool IsConsonant(this char character)
        {
            return character is
                'b' or 'c' or 'd' or 'f' or 'g' or 'h' or 'j' or 'k' or 'l' or 'm' or 'n' or 'p' or 'q' or 'r' or 's' or 't' or 'v' or 'w' or 'x' or 'z' or
                'B' or 'C' or 'D' or 'F' or 'G' or 'H' or 'J' or 'K' or 'L' or 'M' or 'N' or 'P' or 'Q' or 'R' or 'S' or 'T' or 'V' or 'W' or 'X' or 'Z';
        }
    }
}

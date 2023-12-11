using MemoryTest.PAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest
{
    internal class PAOPractice
    {
        private readonly PAOData paoData;
        private readonly Random rnd = new();

        public PAOPractice(PAOData paoData)
        {
            this.paoData = paoData;
        }

        public void StoryToNumber(string themeDigits)
        {
            string? input;
            string secondDigit;
            string number;
            string story = "";
            bool isCorrectInput = true;

            do
            {
                if (isCorrectInput)
                {
                    number = "";
                    for (int i = 0; i < 3; i++)
                    {
                        secondDigit = rnd.Next(1, 9).ToString();
                        int randomThemeDigitIndex = rnd.Next(0, themeDigits.Length);
                        number += themeDigits[randomThemeDigitIndex] + secondDigit + " ";
                    }
                    story = paoData.GetStoryFromNumber(number[..^1]);
                    Console.WriteLine(story);
                }

                input = Console.ReadLine();
                if (input != null && input.ToLower() == "r")
                {
                    Console.Clear();
                    Console.WriteLine(story);
                    Console.WriteLine(paoData.GetNumberFromStory(story));
                    isCorrectInput = true;
                }
                else if (input != null && paoData.GetStoryFromNumber(input) == story)
                {
                    Console.Clear();
                    Console.WriteLine("Correct");
                    isCorrectInput = true;
                }
                else
                {
                    Console.WriteLine("Try again");
                    Console.WriteLine(story);
                    isCorrectInput = false;
                }
            } while (input != null);
        }
    }
}

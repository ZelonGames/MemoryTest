using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MemoryTest.PAO.ChessSquarePAO
{
    public class ChessSquareToPAOController
    {
        public delegate void GeneratedSquaresHandler(List<string> generatedSquares);
        public delegate void GuessedHandler(string guess);
        public delegate void RevealedAnswerHandler(string answer);

        public event GeneratedSquaresHandler? GeneratedSquares;
        public event GuessedHandler? GuessedCorrect;
        public event GuessedHandler? GuessedWrong;
        public event RevealedAnswerHandler? RevealedAnswer;

        private readonly Random rnd = new();
        private readonly PAOData pAOData;

        public ChessSquareToPAOController(PAOData pAOData)
        {
            this.pAOData = pAOData;
        }

        public void Run(string input, List<string> squares)
        {
            if (input.ToLower() == "r")
                RevealAnswer(squares);
            else if (IsInputStoryCorrect(input, squares))
                GuessedCorrect?.Invoke(input);
            else
                GuessedWrong?.Invoke(input);
        }

        public void RevealAnswer(List<string> squares)
        {
            string sequence = "";
            squares.ForEach(x => sequence += x);
            string answer = pAOData.GetLongStoryFromSequence(sequence);
            RevealedAnswer?.Invoke(answer);
        }

        public void GenerateChessSquares(string columns, int min, int max)
        {
            var squares = new List<string>();
            int amountOfSquares = rnd.Next(min, max + 1);

            for (int i = 0; i < amountOfSquares; i++)
            {
                char column = columns[rnd.Next(0, columns.Length)];
                int row = rnd.Next(1, 9);
                string square = column + row.ToString();
                squares.Add(square);
            }

            GeneratedSquares?.Invoke(squares);
        }

        private bool IsInputStoryCorrect(string storyInput, List<string> squares)
        {
            string correctStory = "";
            
            for (int i = 0; i < squares.Count; i += 3)
            {
                string person = pAOData.PAOItems[squares[i]].Person!;
                correctStory += person + " ";

                if (i + 1 <= squares.Count)
                {
                    string action = pAOData.PAOItems[squares[i + 1]].Action!;
                    correctStory += action + " ";
                }

                if (i + 2 <= squares.Count)
                {
                    string @object = pAOData.PAOItems[squares[i + 2]].Object!;
                    correctStory += @object + " ";
                }
            }

            return storyInput.ToLower() == correctStory[..^1].ToLower();
        }
    }
}

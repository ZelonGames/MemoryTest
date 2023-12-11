using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest.PAO.ChessSquarePAO
{
    public class ConsoleOutputHandler : ChessSquareOutputHandler
    {
        public override void PrecentOutput(List<string> generatedSquares)
        {
            string squareLine = "";

            foreach (string square in generatedSquares)
                squareLine += square + " ";

            Console.WriteLine(squareLine[..^1]);
        }

        public override void PrecentGuessedCorrectOutput(string guess)
        {
            Console.Clear();
            Console.WriteLine("Correct!");
        }

        public override void PrecentGuessedWrongOutput(string guess)
        {
            Console.WriteLine("Wrong");
        }

        public override void PrecentRevealedAnswerOutput(string answer)
        {
            Console.Clear();
            Console.WriteLine(answer);
        }
    }
}

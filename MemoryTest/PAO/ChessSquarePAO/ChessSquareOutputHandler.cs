using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest.PAO.ChessSquarePAO
{
    public abstract class ChessSquareOutputHandler
    {
        public abstract void PrecentOutput(List<string> generatedSquares);
        public abstract void PrecentGuessedWrongOutput(string guess);
        public abstract void PrecentGuessedCorrectOutput(string guess);
        public abstract void PrecentRevealedAnswerOutput(string answer);
    }
}

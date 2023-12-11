using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest.PAO.ChessSquarePAO
{
    public abstract class ChessSquareOutputHandlerAsync
    {
        public abstract Task PrecentOutput(List<string> generatedSquares);
        public abstract Task PrecentGuessedWrongOutput(string guess);
        public abstract Task PrecentGuessedCorrectOutput(string guess);
        public abstract Task PrecentRevealedAnswerOutput(string answer);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest.PAO.ChessSquarePAO
{
    public abstract class ChessSquareInputHandler
    {
        public delegate void ReceivedInputHandler(string input);
        public event ReceivedInputHandler? ReceivedInput;

        public ChessSquareInputHandler()
        {
        }

        public abstract void WaitForInput();

        protected void InvokeEvent(string input)
        { 
            ReceivedInput?.Invoke(input);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest.PAO.ChessSquarePAO
{
    public static class ChessSquareToPAOService
    {
        private static ChessSquareToPAOController? chessSquareToPAOController;
        private static ChessSquareInputHandler? chessSquareInputHandler;
        private static ChessSquareOutputHandler? chessSquareOutputHandler;
        private static List<string> squares = new();
        private static int minSquares;
        private static int maxSquares;
        private static string? columns;
        private static string? currentInput = null;

        public static void Run(
            ChessSquareToPAOController chessSquareToPAOController,
            ChessSquareInputHandler chessSquareInputHandler,
            ChessSquareOutputHandler chessSquareOutputHandler,
            string columns, int minSquares, int maxSquares)
        {
            ChessSquareToPAOService.chessSquareToPAOController = chessSquareToPAOController;
            ChessSquareToPAOService.chessSquareInputHandler = chessSquareInputHandler;
            ChessSquareToPAOService.chessSquareOutputHandler = chessSquareOutputHandler;
            ChessSquareToPAOService.minSquares = minSquares;
            ChessSquareToPAOService.maxSquares = maxSquares;
            ChessSquareToPAOService.columns = columns;

            chessSquareToPAOController.GeneratedSquares += OnGeneratedSquares;
            chessSquareToPAOController.GuessedCorrect += OnGuessedCorrect;
            chessSquareToPAOController.GuessedWrong += OnGuessedWrong;
            chessSquareToPAOController.RevealedAnswer += OnRevealedAnswer;
            chessSquareInputHandler.ReceivedInput += OnReceivedInput;

            chessSquareToPAOController.GenerateChessSquares(columns, minSquares, maxSquares);
        }

        private static void OnReceivedInput(string input)
        {
            currentInput = input;
        }

        private static void OnGeneratedSquares(List<string> generatedSquares)
        {
            squares = generatedSquares;
            chessSquareOutputHandler?.PrecentOutput(generatedSquares);
            chessSquareInputHandler?.WaitForInput();
            if (currentInput != null)
                chessSquareToPAOController?.Run(currentInput, squares);
        }

        private static void OnGuessedWrong(string guess)
        {
            chessSquareOutputHandler?.PrecentGuessedWrongOutput(guess);
            chessSquareInputHandler?.WaitForInput();
            chessSquareToPAOController?.Run(currentInput!, squares);
        }

        private static void OnGuessedCorrect(string guess)
        {
            chessSquareOutputHandler?.PrecentGuessedCorrectOutput(guess);
            chessSquareToPAOController?.GenerateChessSquares(columns, minSquares, maxSquares);
        }

        private static void OnRevealedAnswer(string answer)
        {
            chessSquareOutputHandler?.PrecentRevealedAnswerOutput(answer);
            chessSquareToPAOController?.GenerateChessSquares(columns, minSquares, maxSquares);
        }
    }
}

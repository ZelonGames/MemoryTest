// See https://aka.ms/new-console-template for more information

using MemoryTest;
using MemoryTest.PAO;
using MemoryTest.PAO.ChessSquarePAO;

await PAOProgram.RunGetWordsFromNumberLoop(false, false);
/*
var paoData = await PAOData.LoadPAO("Files/ChessPAO.json");
ChessSquareToPAOService.Run(
    new ChessSquareToPAOController(paoData!), 
    new ConsoleInputHandler(paoData!), 
    new ConsoleOutputHandler(), 
    "ABCDEFGH", 3, 3);*/
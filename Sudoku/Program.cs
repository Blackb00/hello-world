using System;
using System.Collections.Generic;
using static Sudoku.UserIO;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            IDeviceIO deviceIO = new ConsoleIO();
            GameInfo Game = new GameInfo(9);
            Game IsGame = new Game(deviceIO);
            Board board = new Board(Game, deviceIO);
            PhraseProvider phrase = new PhraseProvider("./Data/Phrases.json");
            deviceIO.WriteLn(phrase.getPhrase("welcome"));
            deviceIO.WriteLn(phrase.getPhrase("rules"));
            deviceIO.WriteLn(phrase.getPhrase("level"));
            int level;
            bool isNumber = Int32.TryParse(deviceIO.Read(), out level);
            FloatingSymbol[,] ArrFromBard;
            if (isNumber)
            {
                ArrFromBard = board.boardFill(level);
            }
            else
            {
                deviceIO.WriteLn(phrase.getPhrase("levelDefault"));
                ArrFromBard = board.boardFill(4);
            }
            List<FloatingSymbol> listt = IsGame.GetNumsFromUser();
            bool Result = IsGame.ArraysComparison(ArrFromBard, listt);
            deviceIO.setCursorPosition(board.PositionAfterBoard.Item1, board.PositionAfterBoard.Item2);
            if (Result) deviceIO.Write(phrase.getPhrase("goodCongrats"));
            else deviceIO.Write(phrase.getPhrase("badCongrats"));
            deviceIO.Read();
        }
    }
}

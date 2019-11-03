using System;
using static Sudoku.UserIO;
namespace Sudoku
{
    public class Board
    {
        private readonly IDeviceIO deviceInOut;
        private readonly GameInfo gameInfo;
        private readonly Random r = new Random();
        public (int, int) PositionAfterBoard { get; private set; }

        public Board(GameInfo gameInfo, IDeviceIO deviceIO)
        {
            this.gameInfo = gameInfo;
            this.deviceInOut =deviceIO ?? new ConsoleIO();
            this.PositionAfterBoard = (0, 0);
        }
        private (int,int) BoardDraw()
        { 
            ///////////////////////set the 0.0 /////////////////////////////////////
            (int, int) firstPosition = deviceInOut.getCursorPosition();
            int x = firstPosition.Item1 + 15;
            int y = firstPosition.Item2 + 2;
            deviceInOut.setCursorPosition(x, y);
            ///////////////////// left border of the board////////////////////////
            for (int i=0; i < 27; i++)
            {
                deviceInOut.WriteLn("|");
                deviceInOut.setX(x);

            }
            //////////////////// lower border of the board /////////////////////////
            (int, int) secondPosition = deviceInOut.getCursorPosition();
            deviceInOut.setCursorPosition(secondPosition.Item1+1, secondPosition.Item2 - 1);

            for (int i=0; i < 53; i++)
            {
                deviceInOut.Write("_");
            }
            /////////////////////// upper border of the board ////////////////////////
            deviceInOut.setCursorPosition(x+1,y-1);   
            for (int i = 0; i < 53; i++)
            {
                deviceInOut.Write("_");
            }
            /////////////////////// right border of the board ////////////////////////
            secondPosition = deviceInOut.getCursorPosition();
            deviceInOut.setCursorPosition(secondPosition.Item1, secondPosition.Item2+1);
            for (int i = 0; i < 27; i++)
            {
                deviceInOut.WriteLn("|");
                deviceInOut.setX(secondPosition.Item1);
            }
            /////////////////////// drawing the grid of the board //////////////////////
            int gridX = x + 6;                                        // vertical lines
            int gridY = y;
            for (int z=0; z<8; z++)   
            {
                deviceInOut.setCursorPosition(gridX, gridY);
                for (int i = 0; i < 27; i++)
                {
                    deviceInOut.WriteLn("|");
                    deviceInOut.setX(gridX);
                }
                gridX += 6;
            }
            gridX = x+1;                                        // horisontal lines
            gridY = y+2;
            for (int z = 0; z < 8; z++)
            {
                deviceInOut.setCursorPosition(gridX, gridY);
                for (int i = 0; i < 53; i++)
                {
                    deviceInOut.Write("_");
                }
                gridY += 3;
            }
            PositionAfterBoard = (x+65,y);
            return (x,y);
        }
        
        public FloatingSymbol[,] boardFill(int level)
        {
            int[,] mask = gameInfo.GetMaskedNums(level);

            FloatingSymbol[,] CellsAdresses  = new FloatingSymbol[mask.GetUpperBound(0)+1, mask.GetUpperBound(1)+1];
            (int, int) ZeroCoordinate = this.BoardDraw();
            int x = ZeroCoordinate.Item1;
            int y = ZeroCoordinate.Item2;
            deviceInOut.setCursorPosition(x + 3, y + 1);
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    deviceInOut.Write(mask[row, col].ToString());
                    x = deviceInOut.getCursorPosition().Item1;
                    y = deviceInOut.getCursorPosition().Item2;
                    
                    CellsAdresses[row,col].Position = (x, y);
                    CellsAdresses[row,col].Symbol = mask[row, col];
                    deviceInOut.setCursorPosition(x + 5, y);
                }
                y = deviceInOut.getCursorPosition().Item2;
                deviceInOut.setCursorPosition(ZeroCoordinate.Item1 + 3, y + 3);
            }
            return CellsAdresses;
        }
        
    }
}

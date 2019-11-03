using System.Collections.Generic;
using System.Linq;
using static Sudoku.UserIO;

namespace Sudoku
{
    public class Game
    {
        private readonly IDeviceIO deviceIO;
        public Game(IDeviceIO deviceIO)
        {
            this.deviceIO = deviceIO;
        }
        public List<FloatingSymbol> GetNumsFromUser()
        {
            List<FloatingSymbol> userNums = new List<FloatingSymbol>();
            deviceIO.Control(1, 1, userNums);
            return userNums;
        }
        private FloatingSymbol[,] ArrayModification(FloatingSymbol[,]arr1, List<FloatingSymbol> arr2)
        {
            foreach (FloatingSymbol fs in arr2)
            {
                for (int i = 0; i <= arr1.GetUpperBound(0); i++)
                {
                    for(int q = 0; q <= arr1.GetUpperBound(1); q++)
                    {
                        if(arr1[i, q].Position == fs.Position)
                        {
                            arr1[i, q].Symbol = fs.Symbol;
                        }
                        //int indx = Array.FindIndex<FloatingSymbol>(, x => x.Position == );
                        //arr1[indx].Symbol = fs.Symbol;
                    }            
                }
            }

            return arr1;
        }
        public bool ArraysComparison(FloatingSymbol[,] arr1, List<FloatingSymbol> arr2)
        {
            FloatingSymbol[,] ResultArr = ArrayModification(arr1, arr2);
            for (int i = 0; i <= ResultArr.GetUpperBound(0); i++)
            {
                int[] tempX = new int[ResultArr.GetUpperBound(1)+1];
                for (int q = 0; q <= ResultArr.GetUpperBound(1); q++)
                {
                    tempX[q] = ResultArr[i, q].Symbol;
                }
                bool duplicateInColExists = tempX.GroupBy(n => n).Any(g => g.Count() > 1);
                if (duplicateInColExists) return false;
            }
            for (int q = 0; q <= ResultArr.GetUpperBound(1); q++)
            {
                int[] tempY = new int[ResultArr.GetUpperBound(0)+1];
                for (int i = 0; i <= ResultArr.GetUpperBound(0); i++)
                {
                    tempY[i] = ResultArr[i, q].Symbol;
                }
                bool duplicateInColExists = tempY.GroupBy(n => n).Any(g => g.Count() > 1);
                if (duplicateInColExists) return false;
            }
            return true;
        }
    }
}

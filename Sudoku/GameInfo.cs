using System;
using System.Linq;

namespace Sudoku
{
    public class GameInfo
    {
        private readonly Random r = new Random();
        private readonly int value;
        private readonly int[] baseNums;
        public GameInfo(int value)
        {
            this.value = value;
            this.baseNums = new int[value];
            for (int i = 0; i < value; i++) baseNums[i] = i+1;
        }
        private int[,] GetNums()
        {
            int val = this.value;
            int[,] nums = new int[val, val];
            int[][] column = new int[val][];
            for (int i = 0; i < val; i++)
            {
                column[i] = new int[9];
            }
            for (int i = 0; i < 9; i++)
            {
                int[] row = new int[val];
                bool duplicateInColExists = true;
                while (duplicateInColExists)
                {
                    int[] messedNums = baseNums.OrderBy(x => r.Next()).ToArray();
                    for (int y = 0; y < val; y++)
                    {
                        int num = messedNums[y];
                        row[y] = num;
                        nums[i, y] = num;
                        column[y][i] = num;
                    };
                    for (int p = 0; p < val; p++)
                    {
                        int[] temp = column[p].Take(i + 1).ToArray();
                        duplicateInColExists = temp.GroupBy(n => n).Any(g => g.Count() > 1);
                        if (duplicateInColExists)
                        {
                            break;
                        }
                    }
                }
            }
            return nums;
        }
        public int[,] GetMaskedNums(int level)
        {
            int[,] arr = this.GetNums();
            if (level >= arr.GetUpperBound(0)) level = arr.Length - 1;
            for(int i=0; i < arr.GetUpperBound(0); i++)
            {
                int[] indxsForMask = this.baseNums.OrderBy(q => r.Next()).Take(level).ToArray();
                for (int q=0; q< arr.GetUpperBound(1); q++)
                {
                    if (Array.IndexOf(indxsForMask, q) != -1)
                    {
                        arr[i, q] = 0;
                    }
                }
            }
            return arr;
        }
    }
}

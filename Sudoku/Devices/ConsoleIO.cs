using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class ConsoleIO : UserIO, IDeviceIO
    {
       
        public (int, int) getCursorPosition()
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            return (x, y);

        }
        public string Read()
        {
            string str = Console.ReadLine();
            return str;
        }
        public void setCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
        public void setX(int x)
        {
            int y = Console.CursorTop;
            Console.SetCursorPosition(x, y);

        }
        public void Write(string str)
        {
            Console.Write(str);
        }
        public void WriteLn(string str)
        {
            Console.WriteLine(str);
        }
        public void Control(int stepX, int stepY, List<FloatingSymbol> userNums)
        {
            (int, int) Position;
           // List<FloatingSymbol> userNums = new List<FloatingSymbol>();
           while(true)
           {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        Position = this.getCursorPosition();
                        this.setCursorPosition(Position.Item1, Position.Item2 - stepY);
                        break;
                    case ConsoleKey.DownArrow:
                        Position = this.getCursorPosition();
                        this.setCursorPosition(Position.Item1, Position.Item2 + stepY);
                        break;
                    case ConsoleKey.LeftArrow:
                        Position = this.getCursorPosition();
                        this.setCursorPosition(Position.Item1 - stepX, Position.Item2);
                        break;
                    case ConsoleKey.RightArrow:
                        Position = this.getCursorPosition();
                        this.setCursorPosition(Position.Item1 + stepX, Position.Item2);
                        break;
                    case ConsoleKey.Spacebar:
                        userNums.Add(this.TakeNumber());
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
           }
        }
        public bool EnterKeyPressed()
        {
            if (Console.ReadKey(true).Key == ConsoleKey.Enter) return true;
            else return false;
        }
        public bool EscapeKeyPressed()
        {
            if (Console.ReadKey(true).Key == ConsoleKey.Escape) return true;
            else return false;
        }
        public FloatingSymbol TakeNumber()
        {
            FloatingSymbol fs = new FloatingSymbol();
            int num;
            fs.Position = (this.getCursorPosition().Item1+1, this.getCursorPosition().Item2);
            string str = this.Read();
            bool isNumber = Int32.TryParse(str, out num);
            if (isNumber) { fs.Symbol = num; }
            else { this.setCursorPosition(fs.Position.Item1, fs.Position.Item2); this.Write("*"); }
            
            return fs;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sudoku.UserIO;

namespace Sudoku
{
    public interface IDeviceIO
    {
        
        void Write(string str);
        void WriteLn(string str);
        string Read();
        (int, int) getCursorPosition();
        void setCursorPosition(int x, int y);
        void setX(int x);
        void Control(int stepX, int stepY, List<FloatingSymbol> userNums);
        bool EnterKeyPressed();
        bool EscapeKeyPressed();
        FloatingSymbol TakeNumber();
    }
}

namespace Sudoku
{
    public abstract class UserIO
    {
        public struct FloatingSymbol
        {
            public (int, int) Position { get; set; }
            public int Symbol { get; set; }
            public override string ToString()
            {
                return this.Symbol.ToString() +"  "+ this.Position.ToString();
            }
        }
    }
}

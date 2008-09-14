using BitArray = System.Collections.BitArray;

namespace Axel.Sudoku
{
    /// <summary> what a <c>IBoard</c> observer must do. </summary>
    public interface IObserver
    {
        void Set(int cell, int digit);
        void Possible(int cell, BitArray digits);
    }
}
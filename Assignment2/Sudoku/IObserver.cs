using BitArray = System.Collections.BitArray;

namespace Axel.Sudoku
{
  /// <summary> what a <see>IBoard</see> observer must do. </summary>
    public interface IObserver {
        /// <summary> a digit is entered into a cell. </summary>
        void Set( int cell, int digit );
        /// <summary> new list of candidates for a cell. </summary>
        void Possible( int cell, BitArray digits );
    }
}
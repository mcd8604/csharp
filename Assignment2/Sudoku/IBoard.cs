using IEnumerable = System.Collections.IEnumerable;

namespace Axel.Sudoku
{
    /// <summary> what a Sudoku Model must do. </summary>
    public interface IBoard
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void Set(int cell, int digit);
        IEnumerable Row(int cell);
        IEnumerable Column(int cell);
        IEnumerable Shape(int cell);
        IEnumerable Context(int cell);
    }
}

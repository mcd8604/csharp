using IEnumerable = System.Collections.IEnumerable;

namespace Axel.Sudoku
{
    /// <summary> what a Sudoku Model must do. </summary>
    public interface IBoard
    {
        /// <summary> add an observer. </summary>
        void AddObserver( IObserver observer );
        /// <summary> remove an observer. </summary>
        void RemoveObserver( IObserver observer );
        /// <summary> set a digit into a cell. </summary>
        void Set( int cell, int digit );
        /// <summary>Clear a digit which has previously been Set into a cell.</summary>
        void Clear( int cell );
        /// <summary> indices in same row. </summary>
        IEnumerable Row( int cell );
        /// <summary> indices in same column. </summary>
        IEnumerable Column( int cell );
        /// <summary> indices in same shape. </summary>
        IEnumerable Shape( int cell );
        /// <summary> indices in context of cell. </summary>
        IEnumerable Context( int cell );
    }
}

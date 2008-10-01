namespace Axel.Sudoku {
    /// <summary> a Sudoku Model that allows cells to be reset. </summary>
    public interface IClearableBoard : IBoard {

        /// <summary>Clear a digit which has previously been Set into a cell.</summary>
        void Clear( int cell );

    }
}

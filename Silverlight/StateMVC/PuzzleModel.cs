using System.Collections.Generic;
using System;


namespace TerryAndMike.SilverlightGame.StateMVC {
    /// <summary>
    /// Implements an IModel for managing pieces to a Sam Loyd-esque (n-1)-Puzzle, given n:=(rows*cols).
    /// </summary>
    /// <remarks>Note: The blank piece will still notify observers, indicating tile number 0.</remarks>
    public class PuzzleModel : IModel {

        private StateToView observers;

        private int[ , ] puzzle;
        private int rows, cols;
        private int blankRow, blankCol;

        private enum Direction { Up, Down, Left, Right };

        #region IModel Members

        /// <summary>
        /// Called from the controller to indicate that our view would like this cell made into the blank cell, if possible.
        /// </summary>
        /// <param name="row">Row value to identify candidate cell</param>
        /// <param name="col">Column value to identify candidate cell</param>
        public void ShiftMakeBlank( int row, int col ) {
            /**** Verify row, col are within puzzle ****/
            if ( row < 0 || row >= rows || col < 0 || col >= cols )
                return;

            /**** Make sure blank is in context ****/
            if ( !ContextContainsBlank(row, col) )
                return;

            /**** Move elements, notify observers as processed ****/
            Direction blankDir = GetBlankDirection( row, col );
            int addOrSubtract = 1;
            switch ( blankDir ) {
                case Direction.Down:
                    addOrSubtract = -1;
                    goto case Direction.Up;
                case Direction.Up:
                    for ( int r = blankRow; r != row; r += addOrSubtract ) {
                        puzzle[ r, col ] = puzzle[ r + addOrSubtract, col ];
                        observers( r, col, puzzle[ r, col ] );
                    }

                    break;

                case Direction.Right:
                    addOrSubtract = -1;
                    goto case Direction.Left;
                case Direction.Left:
                    for ( int c = blankCol; c != col; c += addOrSubtract ) {
                        puzzle[ row, c ] = puzzle[ row, c + addOrSubtract ];
                        observers( row, c, puzzle[ row, c ] );
                    }

                    break;

            }

            /**** Establish and blank new "blank element" ****/
            blankRow = row;
            blankCol = col;

            puzzle[ blankRow, blankCol ] = 0;
            observers( blankRow, blankCol, 0 );
        }

        /// <summary>
        /// Resets the puzzle board and re-places puzzle pieces, randomly picking an "empty piece"
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public virtual void Reset( int rows, int cols ) {
            /**** Quick validation ****/
            if (rows < 0 || cols < 0)
                return;

            puzzle = new int[ rows, cols ];
            this.rows = rows;
            this.cols = cols;

            /**** Randomly assign puzzle matrix values on [1,rows*cols] ****/
            List<int> values = new List<int>(puzzle.Length);
            for ( int i = 1; i <= puzzle.Length; ++i ) {
                values.Add(i);
            }

            Random rnd = new Random();
            int valuesIdx, puzzleIdx = 0;
            while ( values.Count > 1 ) {
                valuesIdx = rnd.Next( values.Count );
                puzzle[ puzzleIdx/cols, (puzzleIdx++)%cols ] = values[ valuesIdx ];
                values.RemoveAt( valuesIdx );
            }
            puzzle[ rows - 1, cols - 1 ] = values[ 0 ];

            /**** Randomly pick a piece to "blank out" (set to 0) ****/
            puzzleIdx = rnd.Next( puzzle.Length );
            blankRow = puzzleIdx / cols;
            blankCol = puzzleIdx % cols;
            puzzle[ blankRow, blankCol ] = 0;

            /**** Notify all observers of full state ****/
            SendFullStateToObservers();

        }

        /// <summary>
        /// Registers an observer to the model.
        /// </summary>
        /// <param name="view"></param>
        public void AddView( IView view ) {
            observers += view.StateUpdated;
        }

        /// <summary>
        /// Removes an observer from the model.
        /// </summary>
        /// <param name="view"></param>
        public void RemoveView( IView view ) {
            observers -= view.StateUpdated;
        }

        #endregion

        /// <summary>
        /// Updates all viewers with messages about the state of all puzzle elements.
        /// </summary>
        protected virtual void SendFullStateToObservers() {
            if ( observers == null )
                return;

            for ( int r = 0; r < rows; ++r ) {
                for ( int c = 0; c < cols; ++c ) {
                    observers( r, c, puzzle[ r, c ] );
                }
            }
        }

        /// <summary>
        /// Returns true if blank is in the Row/Col context of the provided coordinate, and not the same as the provided coordinate.
        /// </summary>
        /// <param name="row">Row, index origin 0</param>
        /// <param name="col">Column, index origin 0</param>
        /// <returns></returns>
        private bool ContextContainsBlank( int row, int col ) {
            return ( row == blankRow ^ col == blankCol );
        }

        //assumes ContextContainsBlank(), otherwise result is not accurate
        private Direction GetBlankDirection( int row, int col ) {
            if ( blankRow < row )
                return Direction.Up;
            else if ( blankRow > row )
                return Direction.Down;
            else if ( blankCol < col )
                return Direction.Left;
            else
                return Direction.Right;
        }

    }
}

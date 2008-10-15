using System;
using System.Collections.Generic;

namespace TerryAndMike.SilverlightGame.StateMVC
{
    public class BlackoutModel : IModel
    {
        protected StateToView observers;

        protected int[ , ] board;
        protected int rows, cols;


        #region IModel Members

        public void NotifyStateChange(int row, int col)
        {
            /**** Verify row, col are within puzzle ****/
            if ( row < 0 || row >= rows || col < 0 || col >= cols )
                return;

            board[ row, col ] = board[ row, col ] ^ 1; //flip bit
            observers( row, col, board[ row, col ] );

            if ( row > 0 ) {
                board[ row - 1, col ] = board[ row - 1, col ] ^ 1;
                observers( row - 1, col, board[ row - 1, col ] );
            }

            if ( row < rows - 1 ) {
                board[ row + 1, col ] = board[ row + 1, col ] ^ 1;
                observers( row + 1, col, board[ row + 1, col ] );
            }

            if ( col > 0 ) {
                board[ row, col - 1 ] = board[ row, col - 1 ] ^ 1;
                observers( row, col - 1, board[ row, col - 1 ] );
            }

            if ( col > cols - 1 ) {
                board[ row, col + 1 ] = board[ row, col + 1 ] ^ 1;
                observers( row, col + 1, board[ row, col + 1 ] );
            }
        }

        public void Reset(int rows, int cols)
        {
            /**** Quick validation ****/
            if ( rows < 0 || cols < 0 )
                return;

            board = new int[ rows, cols ];
            this.rows = rows;
            this.cols = cols;
            
            /**** Randomly assign board cells values 0 or 1 ****/
            Random rnd = new Random();
            for ( int i = 0; i < board.GetLength(0); ++i ) {
                for ( int j = 0; j < board.GetLength( 1 ); ++j ) {
                    board[ i, j ] = rnd.Next( 2 );
                }
            }

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
                    observers( r, c, board[ r, c ] );
                }
            }
        }
    }
}

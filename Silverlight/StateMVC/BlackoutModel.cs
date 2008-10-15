using System;
using System.Collections.Generic;

namespace TerryAndMike.SilverlightGame.StateMVC
{
    public class BlackoutModel : BoardModel
    {
        #region IModel Members

        public override void NotifyStateChange(int row, int col)
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

        #endregion


        /// <summary>
        /// Initialize values on a newly constructed board
        /// </summary>
        protected override void InitializeBoardValues() {
            /**** Randomly assign board cells values 0 or 1 ****/
            Random rnd = new Random();
            for ( int i = 0; i < board.GetLength( 0 ); ++i ) {
                for ( int j = 0; j < board.GetLength( 1 ); ++j ) {
                    board[ i, j ] = rnd.Next( 2 );
                }
            }
        }
    }
}

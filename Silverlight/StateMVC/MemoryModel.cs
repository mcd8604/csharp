using System;
using System.Collections.Generic;

namespace TerryAndMike.SilverlightGame.StateMVC
{
    public class MemoryModel : BoardModel
    {
        protected int[] upCell;

        public MemoryModel() : base() {
            upCell = new int[] { -1, -1 };
        }

        protected override void InitializeBoardValues() {
            for ( int r = 0; r < board.GetLength( 0 ); ++r )
                for ( int c = 0; c < board.GetLength( 1 ); ++c ) {
                    board[ r, c ] = 0;
                }
        }

        public override void NotifyStateChange( int row, int col ) {
            
            board[ row, col ] = 1;
            observers( row, col, 1 );

            if ( upCell[ 0 ] == -1 && upCell[ 1 ] == -1 ) {
                upCell[ 0 ] = row;
                upCell[ 1 ] = col;
            }
            /*else {
                
            }*/
        }
    }
}

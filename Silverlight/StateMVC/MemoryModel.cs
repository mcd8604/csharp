using System;
using System.Collections.Generic;

namespace TerryAndMike.SilverlightGame.StateMVC
{
    public class MemoryModel : BoardModel
    {
        protected const int MATCH_DISPLAY_TIME_MS = 1500;

        protected int[] upCell;
        protected bool[,] boardVisibility;
        protected int pairsLeftToMatch;
        
        public MemoryModel() : base() {
            upCell = new int[] { -1, -1 };
        }

        protected override bool ValidateBoardSize(int rows, int cols)
        {
            return base.ValidateBoardSize(rows, cols) && (rows * cols) % 2 == 0;
        }

        protected override void InitializeBoardValues() {
            /**** Instantiate & initialize boardVisibility matrix ****/
            boardVisibility = new bool[ rows, cols ];

            /**** Initialize pairsLeftToMatch ****/
            pairsLeftToMatch = board.Length / 2;

            /**** Randomly assign board values in pairs (12 cells have on [1,rows*cols] ****/
            List<int> imagePairsList = new List<int>(board.Length);
            for (int i = 1; i <= board.Length/2; ++i)
            {
                //add twice to imagePairsList (pairs)
                imagePairsList.Add(i);
                imagePairsList.Add(i);
            }

            Random rnd = new Random();
            int pairsListIdx, boardIdx = 0;
            while (imagePairsList.Count > 1)
            {
                pairsListIdx = rnd.Next(imagePairsList.Count);
                board[boardIdx / cols, (boardIdx++) % cols] = imagePairsList[pairsListIdx];
                imagePairsList.RemoveAt(pairsListIdx);
            }
            board[rows - 1, cols - 1] = imagePairsList[0];

        }

        public override void NotifyStateChange( int row, int col ) {
            /**** Verify row, col are within board ****/
            if (row < 0 || row >= rows || col < 0 || col >= cols)
                return;

            /**** Set visibility, check if match found ****/
            if ( upCell[ 0 ] == -1 && upCell[ 1 ] == -1 ) { //first of pair to be clicked
                if (boardVisibility[row, col] == true) //already matched (locked visible) cell clicked
                    return;

                upCell[ 0 ] = row;
                upCell[ 1 ] = col;
                boardVisibility[ row, col ] = true;
                visibleObservers( row, col, true );
            }
            else {
                if (upCell[0] == row && upCell[1] == col) { //clicked same cell again
                    boardVisibility[row, col] = false;
                    visibleObservers(row, col, false);
                }
                else {                                      //clicked a second cell, check for match
                    boardVisibility[row, col] = true;
                    visibleObservers(row, col, true);

                    if (board[upCell[0], upCell[1]] == board[row, col]) {   //if two selected match
                        --pairsLeftToMatch;
                    }
                    else {                                                  //two selected don't match
                        System.Threading.Thread.Sleep(MATCH_DISPLAY_TIME_MS);

                        boardVisibility[ upCell[0], upCell[1] ] = false;
                        visibleObservers( upCell[ 0 ], upCell[ 1 ], false );

                        boardVisibility[row, col] = false;
                        visibleObservers( row, col, false );
                    }
                }

                //reset upCell back to defaults
                upCell[0] = -1;
                upCell[1] = -1;
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace TerryAndMike.SilverlightGame.StateMVC
{
    /// <summary>
    /// Model for the memory matching game
    /// </summary>
    public class MemoryModel : BoardModel
    {
        /// <summary>
        /// Constant value for the number of milliseconds to display both wrong cells before hiding
        /// </summary>
        protected const int MATCH_DISPLAY_TIME_MS = 1500;

        /// <summary>
        /// Row and column of currently displayed ("up") cell
        /// </summary>
        protected int[] upCell;

        /// <summary>
        /// Matrix of visiblities of all cells on the board
        /// </summary>
        protected bool[,] boardVisibility;

        /// <summary>
        /// Countdown of pairs of elements left to find matches for.
        /// </summary>
        protected int pairsLeftToMatch;


        /// <summary>
        /// Construct a MemoryModel with the upCell initialized to default none-selected (-1, -1)
        /// </summary>
        public MemoryModel() : base() {
            upCell = new int[] { -1, -1 };
        }

        /// <summary>
        /// Returns whether the board size parameters provided are valid
        /// </summary>
        /// <param name="rows">Number of rows to create</param>
        /// <param name="cols">Number of cols to create</param>
        /// <returns>True if valid parameters supplied</returns>
        protected override bool ValidateBoardSize(int rows, int cols)
        {
            return base.ValidateBoardSize(rows, cols) && (rows * cols) % 2 == 0;
        }

        /// <summary>
        /// Initialize board visibility values, pairsLeftToMatch, and assign pairs of values.
        /// </summary>
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

        /// <summary>
        /// Notifies the model of action on this element (selected by mouse)
        /// </summary>
        /// <param name="row">The row number in the board.</param>
        /// <param name="col">The column number in the board.</param>
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

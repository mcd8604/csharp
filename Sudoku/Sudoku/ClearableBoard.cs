using System;
using Axel.Sudoku;

namespace TerryAndMike.Sudoku {
    /// <summary>
    /// A Sudoku board Model (MVC paradigm) implementation.
    /// </summary>
    class ClearableBoard : Board, IClearableBoard {
        public ClearableBoard( string[] boardData )
            : base( boardData ) {
        }

        protected override void InitializeCells() {
            cells = new ClearableCell[ dimension * dimension ];
            for ( int i = 0; i < cells.Length; ++i )
                cells[ i ] = new ClearableCell( dimension );
        }

        /// <summary>
        /// Clear a digit from a cell that has been set. 
        /// Send possible messages to cells within context.
        /// </summary>
        /// <param name="cell">The index of the cell to clear</param>
        public void Clear( int cell ) {
            ClearableCell clearableCell = cells[ cell ] as ClearableCell;
            if ( clearableCell == null ) {
                throw new InvalidOperationException( "Attempting to clear un-clearable cell id " + cell + "." );
            }

            int clearedDigit = cells[ cell ].Digit;
            clearableCell.Clear();

            //recover candidates
            foreach ( int i in Context( cell ) ) {
                if ( cells[ i ].Digit > 0 ) {
                    cells[ cell ].RemoveCandidate( cells[ i ].Digit );
                }

                // check if clearedDigit is in context of 'i'
                bool inContext = false;
                foreach ( int k in Context( i ) ) {
                    if ( cells[ k ].Digit == clearedDigit ) {
                        inContext = true;
                        break;
                    }
                }

                if ( !inContext )
                    ((ClearableCell)cells[ i ]).AddCandidate( clearedDigit );
            }

            foreach ( IObserver o in observers ) {
                foreach ( int i in Context( cell ) ) {
                    //only notify not already set
                    if ( !cells[ i ].IsSet ) {
                        o.Possible( i, cells[ i ].Candidates );
                    }
                }
                o.Possible( cell, cells[ cell ].Candidates );
            }
        }
    }
}
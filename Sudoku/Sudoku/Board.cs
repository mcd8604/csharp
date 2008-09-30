﻿using System;
using Axel.Sudoku;
using IEnumerable = System.Collections.IEnumerable;
using Enumerable = System.Linq.Enumerable;
using BitArray = System.Collections.BitArray;
using System.Collections.Generic;

namespace TerryAndMike.Sudoku
{
    /// <summary>
    /// A Sudoku board Model (MVC paradigm) implementation.
    /// </summary>
    class Board : IBoard
    {
        #region Fields

        private List<IObserver> observers;

        private readonly int dimension;

        private int[] shapes;

        private Cell[] cells;

        #endregion

        #region Properties

        /// <summary>
        /// Return dimmension of a single side of the board.
        /// </summary>
        public int Dimension
        {
            get { return dimension; }
        }

        /// <summary>
        /// Return a one-dimmensional array of shapeIds (index origin 1) indexed my cellId (origin 0)
        /// </summary>
        public int[] Shapes
        {
            get { return shapes; }
        }

        #endregion

        /// <summary>
        /// Initializes the Sudoku board.
        /// </summary>
        /// <param name="boardData">String array (character matrix) representing the board.</param>
        public Board(string[] boardData)
        {
            observers = new List<IObserver>();
            

            //Set dimmension of the board as height of input matrix
            dimension = boardData.Length;

            //For now assume square board, will check below
            shapes = new int[dimension * dimension];


            for (int row = 0; row < dimension; ++row)
            {
                for (int col = 0; col < boardData[row].Length; ++col)
                {
                    //Throw exception for non-square boards, not supported
                    if (dimension != boardData[row].Length)
                        throw new ArgumentException("Non-square boards not supported.");

                    shapes[(row * dimension) + col] = int.Parse(boardData[row][col].ToString());
                }
            }

            // Create each cell
            cells = new Cell[dimension * dimension];
            for (int i = 0; i < cells.Length; ++i)
                cells[i] = new Cell(dimension);

        }

        #region IBoard Members

        /// <summary> add an observer to the notification list. </summary>
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        /// <summary> remove an observer from notifications. </summary>
        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }


        /// <summary> set a digit into a cell. </summary>
        /// <remarks>
        /// Set requests a board to put a non-zero digit into a cell identified by an index between 0 and 80. 
        /// As a response, every IObserver which is known to the board is sent a Set and 
        /// maybe some Possible messages.
        /// </remarks>
        /// <param name="cell">The cell index, [0,80]</param>
        /// <param name="digit">The digit to set, [1,9]</param>
        public void Set(int cell, int digit)
        {
            //per instructions, restrict board to 81 cells
            if (digit != 0 && cell >= 0 && cell <= 80)
            {
                cells[cell].Set(digit);
                foreach (int i in Context(cell))
                {
                    cells[i].RemoveCandidate(digit);
                }

                foreach (IObserver o in observers)
                {
                    o.Set(cell, digit);

                    foreach (int i in Context(cell))
                    {
                        //only notify not already set
                        if ( !cells[ i ].IsSet ) {
                            o.Possible( i, cells[ i ].Candidates );
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Clear a digit from a cell that has been set. 
        /// Send possible messages to cells within context.
        /// </summary>
        /// <param name="cell">The index of the cell to clear</param>
        public void Clear(int cell)
        {
            int clearedDigit = cells[cell].Digit;
            cells[cell].Clear();

            //recover candidates
            foreach (int i in Context(cell))
            {
                if (cells[i].Digit > 0)
                {
                    cells[cell].RemoveCandidate(cells[i].Digit);
                }

                // check if clearedDigit is in context of 'i'
                bool inContext = false;
                foreach (int k in Context(i))
                {
                    if (cells[k].Digit == clearedDigit)
                    {
                        inContext = true;
                        break;
                    }
                }

                if(!inContext) 
                    cells[i].AddCandidate(clearedDigit);
            }

            foreach (IObserver o in observers)
            {
                foreach (int i in Context(cell))
                {
                    //only notify not already set
                    if (!cells[i].IsSet)
                    {
                        o.Possible(i, cells[i].Candidates);
                    }
                }
                o.Possible( cell, cells[cell].Candidates );
            }
        }

        /// <summary> indices in same row. </summary>
        /// <remarks> Note: input cell is not included in output enumeration</remarks>
        public IEnumerable Row(int cell)
        {
            int[] rowIndices = new int[dimension - 1];

                //assumes square board
            int startIndex = cell - (cell % dimension);
            int curIndex = startIndex;

            for (int i = 0; i < rowIndices.Length; ++curIndex)
            {
                if ( curIndex != cell ) {
                    rowIndices[ i++ ] = curIndex;
                }
            }
            
            return rowIndices;
        }

        /// <summary> indices in same column. </summary>
        /// <remarks> Note: input cell is not included in output enumeration</remarks>
        public IEnumerable Column(int cell)
        {
            int[] columnIndices = new int[dimension - 1];

                //assumes square board
            int startIndex = cell % dimension;
            int curIndex = startIndex;

            for (int i = 0; i < columnIndices.Length; curIndex += dimension )
            {
                if ( curIndex != cell ) {
                    columnIndices[ i++ ] = curIndex;
                }
            }

            return columnIndices;
        }

        /// <summary> indices in same shape. </summary>
        /// <remarks> Note: input cell is not included in output enumeration</remarks>
        public IEnumerable Shape(int cell)
        {
            //assume dimmension == number of cells in each shape
            int[] shapeIndices = new int[dimension - 1];
            int shapeID = shapes[cell];
            
            int curIndex = -1;

            //iterate through board to find shapes, stopping when 
            //'dimmension'-1 other cells in shape found or end of board reached
            for(int i = 0; i < shapes.Length && curIndex < shapeIndices.Length; ++i)
            {
                if (i != cell && shapes[i] == shapeID)
                    shapeIndices[ ++curIndex ] = i;
            }

            return shapeIndices;
        }

        /// <summary> indices in context of cell. </summary>
        /// <remarks> Note: input cell is not included in output enumeration</remarks>
        public IEnumerable Context(int cell)
        {
            //Assure that each enumerable is not null after casting with 'as'
            IEnumerable<int> row = Row( cell ) as IEnumerable<int> ?? new int[0],
                col = Column( cell ) as IEnumerable<int> ?? new int[0],
                shape = Shape( cell ) as IEnumerable<int> ?? new int[0];

            try {
                 return Enumerable.Union( Enumerable.Union( row, col ), shape );
            }
            catch ( ArgumentNullException ane ) {
                throw new Exception( 
                    "Error in calculating context for cell '" + cell + "'.  One of the arguements was null.",
                    ane );
            }
        }

        #endregion
    }
}

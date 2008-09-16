﻿using System;
using Axel.Sudoku;
using IEnumerable = System.Collections.IEnumerable;
using Enumerable = System.Linq.Enumerable;
using BitArray = System.Collections.BitArray;
using System.Collections.Generic;

namespace TerryAndMike.Sudoku
{
    /// <summary>
    /// A Board where the constructor uses a string array such as the one at the beginning of 
    /// board.txt to define the board size and the shapes -- equal digits define a shape.
    /// </summary>
    class Board : IBoard
    {
        #region Fields

        private List<IObserver> observers;

        private int dimension;

        private int[] shapes;

        private Cell[] cells;

        #endregion

        public Board(string[] boardData)
        {
            observers = new List<IObserver>();
            
            // Process board data
            dimension = boardData.Length;

            shapes = new int[dimension * dimension];

            for (int i = 0; i < dimension; ++i)
            {
                for (int k = 0; k < boardData[i].Length; ++k)
                {
                    shapes[(i * dimension) + k] = int.Parse(boardData[i][k].ToString());
                }
            }

            // Create each cell
            cells = new Cell[dimension * dimension];
            for (int i = 0; i < cells.Length; ++i)
                cells[i] = new Cell(i);

        }

        #region IBoard Members

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        /// <summary>
        /// Set requests a board to put a non-zero digit into a cell identified by an index between 0 and 80. 
        /// As a response, every IObserver which is known to the board is sent a Set and 
        /// maybe some Possible messages.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="digit"></param>
        public void Set(int cell, int digit)
        {
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
                        o.Possible(i, cells[i].Candidates);
                    }
                }
            }
        }

        public IEnumerable Row(int cell)
        {
            int[] rowIndices = new int[dimension - 1];

            int startIndex = cell - (cell % dimension);
            int curIndex;

            for (int i = 0; i < rowIndices.Length; ++i)
            {
                curIndex = startIndex + i;
                if (curIndex != cell)
                    rowIndices[i] = curIndex;
            }
            
            return rowIndices;
        }

        public IEnumerable Column(int cell)
        {
            int[] columnIndices = new int[dimension - 1];

            int colIndex = cell % dimension;
            int curIndex;

            for (int i = 0; i < columnIndices.Length; ++i)
            {
                curIndex = colIndex + (i * dimension);
                if (curIndex != cell)
                    columnIndices[i] = curIndex;
            }

            return columnIndices;
        }

        public IEnumerable Shape(int cell)
        {
            int[] shapeIndices = new int[dimension - 1];
            int shapeID = shapes[cell];
            
            int curIndex = -1;

            for(int i = 0; i < shapes.Length; ++i)
            {
                if (i != cell && shapes[i] == shapeID)
                    shapeIndices[++curIndex] = i;

                if (shapeIndices.Length - curIndex - 1 > 0)
                    break;
            }

            return shapeIndices;
        }

        public IEnumerable Context(int cell)
        {
            return Enumerable.Union(Enumerable.Union(Row(cell) as IEnumerable<int>, Column(cell) as IEnumerable<int>), Shape(cell) as IEnumerable<int>); 
        }

        #endregion
    }
}
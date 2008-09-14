using System;
using Axel.Sudoku;
using IEnumerable = System.Collections.IEnumerable;
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

        private Cell[] cells;

        #endregion

        public Board(string[] boardData)
        {
            observers = new List<IObserver>();

            // TODO: Process board data
            //cells = new Cell[81];
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

                foreach (IObserver o in observers)
                    o.Set(cell, digit);
            }
        }

        public IEnumerable Row(int cell)
        {
            throw new NotImplementedException();
        }

        public IEnumerable Column(int cell)
        {
            throw new NotImplementedException();
        }

        public IEnumerable Shape(int cell)
        {
            throw new NotImplementedException();
        }

        public IEnumerable Context(int cell)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

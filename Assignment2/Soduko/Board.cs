using System;
using Axel.Sudoku;
using IEnumerable = System.Collections.IEnumerable;

namespace TerryAndMike.Soduko
{
    /// <summary>
    /// A Board where the constructor uses a string array such as the one at the beginning of 
    /// board.txt to define the board size and the shapes -- equal digits define a shape.
    /// </summary>
    class Board : IBoard
    {
        #region IBoard Members

        public void AddObserver(IObserver observer)
        {
            throw new NotImplementedException();
        }

        public void RemoveObserver(IObserver observer)
        {
            throw new NotImplementedException();
        }

        public void Set(int cell, int digit)
        {
            throw new NotImplementedException();
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

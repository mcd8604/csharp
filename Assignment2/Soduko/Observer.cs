using System;
using Axel.Sudoku;
using BitArray = System.Collections.BitArray;

namespace TerryAndMike.Soduko
{
    /// <summary>
    /// An Observer which prints information about the messages it receives. 
    /// </summary>
    class Observer : IObserver
    {
        #region IObserver Members

        public void Set(int cell, int digit)
        {
            throw new NotImplementedException();
        }

        public void Possible(int cell, BitArray digits)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

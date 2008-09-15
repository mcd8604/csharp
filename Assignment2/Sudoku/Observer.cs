using System;
using Axel.Sudoku;
using BitArray = System.Collections.BitArray;
using StringBuilder = System.Text.StringBuilder;

namespace TerryAndMike.Sudoku
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
            StringBuilder sb = new StringBuilder("Possible " + cell + " ");
            for(int i = 0; i < digits.Length; ++i)
            {
                if (digits[i])
                    sb.Append(i + " ");
            }
            Console.WriteLine(sb);
        }

        #endregion
    }
}

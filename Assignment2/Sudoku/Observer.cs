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

        /// <summary>
        /// Notification that a digit has been set for this cell.
        /// </summary>
        /// <remarks>Presently this notification is written to stdout.</remarks>
        /// <param name="cell">Index to cell that has been set.</param>
        /// <param name="digit">Digit to which cell has been set.</param>
        public void Set(int cell, int digit)
        {
            Console.WriteLine("Set " + cell + " " + digit);
        }

        /// <summary>
        /// Notification that the candidate list has changed for this cell.
        /// </summary>
        /// <remarks>Presently this notification is written to stdout.</remarks>
        /// <param name="cell">Index to cell whose candidate list has changed.</param>
        /// <param name="digits">List of possible candidates, with <code>digits[0]=true</code> representing 1 as possible.</param>
        public void Possible(int cell, BitArray digits)
        {
            StringBuilder sb = new StringBuilder("Possible " + cell + " ");

            for( int i = 0; i < digits.Length; ++i )
            {
                if ( digits[i] )
                    sb.Append(i + 1 + " ");
            }
            Console.WriteLine(sb);
        }

        #endregion
    }
}

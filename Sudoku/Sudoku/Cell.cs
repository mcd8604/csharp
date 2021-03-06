﻿using BitArray = System.Collections.BitArray;
using IEnumerable = System.Collections.IEnumerable;
using System.Collections.Generic;

namespace TerryAndMike.Sudoku
{
    /// <summary>
    /// Stores the state of a particular cell, both its set value and its possible candidates.
    /// </summary>
    /// <remarks>
    /// At the present, once a cell has been set() it does not revert (no undo).  Also, cell identity is not tracked
    /// internally to all various means of identifying this cell.
    /// </remarks>
    class Cell
    {
        #region Fields
        
#if ORIGINAL
        private int? digit = null;
        private BitArray candidates;
#else
        protected int? digit = null;
        protected BitArray candidates;
#endif

        #endregion

        #region Properties

        /// <summary>
        /// Digit that has been set for this cell or 0 if !IsSet
        /// </summary>
        public int Digit
        {
            get { return digit ?? 0; }
        }

        /// <summary>
        /// List of candidate digits, with <code>Candidates[0] = true</code> meaning that 1 is in the candidate set.
        /// </summary>
        public BitArray Candidates
        {
            get { return candidates; }
        }

        /// <summary>
        /// Returns whether cell value has been set.
        /// </summary>
        public bool IsSet
        {
            get { return digit.HasValue; }
        }

        #endregion

        /// <summary>
        /// Create a new cell with all candidates valid.
        /// </summary>
        public Cell(int dimension)
        {
            candidates = new BitArray(dimension, true);
        }

        /// <summary>
        /// Set the cell to a particular digit.
        /// </summary>
        /// <param name="digit">The digit to set, [1,9]</param>
        public void Set(int digit)
        {
            if ( digit < 1 || digit > candidates.Length ) {
                throw new System.ArgumentException( "Digit set must be in the range [1,dimension]" );
            }

            this.digit = digit;

            //update candidates
            candidates.SetAll(false);
            candidates.Set(digit - 1, true);
        }

        /// <summary>
        /// Removes <code>digit</code> from the candidate set.
        /// </summary>
        /// <param name="digit">The digit to remove, [1,dimension]</param>
        public void RemoveCandidate( int digit ) {
            candidates.Set( digit - 1, false );
        }

    }
}

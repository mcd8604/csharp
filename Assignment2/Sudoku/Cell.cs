using BitArray = System.Collections.BitArray;
using IEnumerable = System.Collections.IEnumerable;
using System.Collections.Generic;

namespace TerryAndMike.Sudoku
{
    class Cell
    {
        #region Fields

        private int index;

        private int digit;

        private BitArray candidates;

        #endregion

        #region Properties

        public int Digit
        {
            get { return digit; }
        }

        public BitArray Candidates
        {
            get { return candidates; }
        }

        #endregion

        public Cell(int index)
        {
            this.index = index;

            candidates = new BitArray(9, true);
        }

        public void Set(int digit)
        {
            this.digit = digit;

            //update candidates
            candidates.SetAll(false);
            candidates.Set(digit - 1, true);
        }

        public void RemoveCandidate(int digit)
        {
            candidates.Set(digit - 1, false);
        }
    }
}

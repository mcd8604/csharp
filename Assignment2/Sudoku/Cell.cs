using BitArray = System.Collections.BitArray;

namespace TerryAndMike.Sudoku
{
    class Cell
    {
        #region Fields

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

        public Cell() { }

        public void Set(int digit)
        {
            this.digit = digit;
        }
    }
}

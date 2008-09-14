using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Soduko
{
    class Move : Digit
    {
        #region Fields

        // digit previous set in cell
        private int digit;

        #endregion

        #region Digit Members

        public bool equals(int digit)
        {
            throw new NotImplementedException();
        }

        public int[] digits()
        {
            throw new NotImplementedException();
        }

        public bool canBe(int digit)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

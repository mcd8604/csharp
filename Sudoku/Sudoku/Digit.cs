using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Soduko
{
    interface Digit
    {
        /// <summary>
        /// Returns true if the digit was entered by set.
        /// </summary>
        /// <param name="digit"></param>
        /// <returns></returns>
        Boolean equals(int digit);

        /// <summary>
        /// Returns the (cached) result of get.
        /// </summary>
        /// <returns></returns>
        int[] digits();

        /// <summary>
        /// returns true if the digit is a candidate.
        /// </summary>
        /// <param name="digit"></param>
        /// <returns></returns>
        Boolean canBe(int digit);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Xcel
{
    /// <summary>
    /// An XcelCommand that calculates the sum of an array of integers
    /// </summary>
    public class XcelSum : XcelCommand
    {
        /// <summary>
        /// The sum of the array of integers
        /// </summary>
        protected int? _sum = null;

        /// <summary>
        /// The string name of this command, returns "sum"
        /// </summary>
        public override string CommandName
        {
            get { return "sum"; }
        }

        #region XcelCommand Members

        /// <summary>
        /// Performs the calculation to determine the sum
        /// </summary>
        public override void Execute()
        {
            if (!_sum.HasValue)
            {
                _sum = _args.Sum();
            }
        }

        #endregion

        /// <summary>
        /// Returns a string representation of the sum
        /// </summary>
        public override string ToString()
        {
            return _sum.ToString();
        }
    }
}

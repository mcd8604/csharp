using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Xcel
{
    /// <summary>
    /// Command object for Xcel calculator model: finds median value in argument list.
    /// </summary>
    public class XcelMedian : XcelCommand
    {
        /// <summary>
        /// The median of the array of integers, or null if not yet calculated
        /// </summary>
        protected double? _median = null;

        /// <summary>
        /// The command name for XcelMedian, specifically "median"
        /// </summary>
        public override string CommandName
        {
            get { return "median"; }
        }

        #region XcelCommand Members

        /// <summary>
        /// Performs median value calculation on input argument list.
        /// </summary>
        public override void Execute()
        {
            if (!_median.HasValue)
            {
                int[] argsCopy = new int[_args.Length];
                _args.CopyTo(argsCopy, 0);
                Array.Sort(argsCopy);

                //even
                if (argsCopy.Length % 2 == 0)
                {
                    _median = ((argsCopy[argsCopy.Length / 2 - 1] + argsCopy[argsCopy.Length / 2]) / 2.0);
                }

                //odd
                else
                {
                    _median = argsCopy[argsCopy.Length / 2];
                }
            }
        }

        #endregion

        /// <summary>
        /// Returns median value of arguement list if Execute()ed, otherwise empty string.
        /// </summary>
        /// <returns>String representation of output value</returns>
        public override string ToString()
        {
            if (_median.HasValue)
                return _median.Value.ToString("0.###");
            else
                return string.Empty;
        }
    }
}

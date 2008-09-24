using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Xcel
{
    /// <summary>
    /// An XcelCommand that calculates the median of an array of integers
    /// </summary>
    public class XcelMedian : XcelCommand
    {
        /// <summary>
        /// The median of the array of integers
        /// </summary>
        protected double? _median = null;

        /// <summary>
        /// The string name of this command, returns "median"
        /// </summary>
        public override string CommandName
        {
            get { return "median"; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public XcelMedian() { }

        #region XcelCommand Members

        /// <summary>
        /// Performs the calculation to determine the median
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
        /// Returns a string representation of the median
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _median.Value.ToString("0.###");
        }
    }
}

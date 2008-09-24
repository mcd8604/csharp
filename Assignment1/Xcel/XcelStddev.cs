using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Xcel
{
    /// <summary>
    /// An XcelCommand that calculates the standard deviation of an array of integers
    /// </summary>
    public class XcelStdDev : XcelCommand
    {
        /// <summary>
        /// The standard deviation of the array of integers
        /// </summary>
        protected double? _stdDev = null;

        /// <summary>
        /// The string name of this command, returns "stdev"
        /// </summary>
        public override string CommandName
        {
            get { return "stdev"; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public XcelStdDev() { }

        #region XcelCommand Members

        /// <summary>
        /// Performs the calculation to determine the standard deviation
        /// </summary>
        public override void Execute()
        {
            if (!_stdDev.HasValue)
            {
                //mean
                double mean = _args.Average();

                //deviations
                double[] deviationsSquared = new double[_args.Length];
                for (int i = 0; i < _args.Length; ++i)
                {
                    deviationsSquared[i] = (_args[i] - mean) * (_args[i] - mean);
                }

                //variance
                double variance = deviationsSquared.Average();

                _stdDev = Math.Pow(variance, 0.5);
            }
        }

        #endregion

        /// <summary>
        /// Returns a string representation of the standard deviation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _stdDev.Value.ToString("0.###");
        }
    }
}

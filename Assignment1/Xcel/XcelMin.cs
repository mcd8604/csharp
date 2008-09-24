using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Xcel
{
    /// <summary>
    /// An XcelCommand that calculates the minimum of an array of integers
    /// </summary>
    public class XcelMin : XcelCommand
    {
        /// <summary>
        /// The minimum of the array of integers
        /// </summary>
        protected int? _min = null;

        /// <summary>
        /// The string name of this command, returns "min"
        /// </summary>
        public override string CommandName
        {
            get { return "min"; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public XcelMin() { }

        #region XcelCommand Members

        /// <summary>
        /// Performs the calculation to determine the minimum
        /// </summary>
        public override void Execute()
        {
            if (!_min.HasValue)
            {
                _min = _args.Min();
            }
        }

        #endregion

        /// <summary>
        /// Returns a string representation of the minimum
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _min.ToString();
        }
    }
}

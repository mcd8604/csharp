using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Xcel
{
    /// <summary>
    /// Command object for Xcel calculator model: finds maximum value in argument list.
    /// </summary>
    public class XcelMax : XcelCommand
    {
        /// <summary>
        /// Stores the calculated value of this command object, or null if not yet calculated
        /// </summary>
        protected int? _max = null;

        /// <summary>
        /// The command name for XcelMax, specifically "max"
        /// </summary>
        public override string CommandName
        {
            get { return "max"; }
        }

        #region XcelCommand Members

        /// <summary>
        /// Performs maximum value search on input argument list.
        /// </summary>
        public override void Execute()
        {
            if (!_max.HasValue)
            {
                _max = _args.Max();
            }
        }

        #endregion

        /// <summary>
        /// Returns maximum value of arguement list if Execute()ed, otherwise empty string.
        /// </summary>
        /// <returns>String representation of output value</returns>
        public override string ToString()
        {
            return _max.ToString();
        }
    }
}

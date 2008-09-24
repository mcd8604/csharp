using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Xcel
{
    /// <summary>
    /// Command object for Xcel calculator model: re-orders argument list descending.
    /// </summary>
    public class XcelDown : XcelCommand
    {
        /// <summary>
        /// Stores the calculated value of this command object, or null if not yet calculated
        /// </summary>
        protected int[] _down = null;

        /// <summary>
        /// The command name for XcelDown, specifically "down"
        /// </summary>
        public override string CommandName
        {
            get { return "down"; }
        }

        #region XcelCommand Members

        /// <summary>
        /// Performs descending sort on input argument list.
        /// </summary>
        public override void Execute()
        {
            if (_down == null)
            {
                _down = new int[_args.Length];
                Args.CopyTo(_down, 0);
                Array.Sort<int>(_down);
                Array.Reverse(_down);
            }
        }

        #endregion

        /// <summary>
        /// Returns arguement list sorted descending if Execute()ed, otherwise empty string.
        /// </summary>
        /// <returns>String representation of output value</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (int i in _down)
            {
                sb.Append(i);
                sb.Append(" ");
            }

            return sb.ToString();
        }
    }
}

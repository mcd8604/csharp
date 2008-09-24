using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Xcel
{
    /// <summary>
    /// Command object for Xcel calculator model: finds produc of all values in argument list.
    /// </summary>
    public class XcelProduct : XcelCommand
    {
        /// <summary>
        /// Stores the calculated value of this command object, or null if not yet calculated
        /// </summary>
        protected int? _product = null;

        /// <summary>
        /// The command name for XcelProduct, specifically "product"
        /// </summary>
        public override string CommandName
        {
            get { return "product"; }
        }

        #region XcelCommand Members

        /// <summary>
        /// Performs product calculation of each argument list item.
        /// </summary>
        public override void Execute()
        {
            if (!_product.HasValue)
            {
                _product = 1;
                foreach (int i in _args)
                    _product *= i;
            }
        }

        #endregion

        /// <summary>
        /// Returns product of all arguement list items if Execute()ed, otherwise empty string.
        /// </summary>
        /// <returns>String representation of output value</returns>
        public override string ToString()
        {
            return _product.ToString();
        }
    }
}

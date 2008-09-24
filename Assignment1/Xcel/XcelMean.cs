using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Xcel
{
    /// <summary>
    /// Command object for Xcel calculator model: finds mean value in argument list.
    /// </summary>
    public class XcelMean : XcelCommand
    {
        /// <summary>
        /// Stores the calculated value of this command object, or null if not yet calculated
        /// </summary>
        protected double? _mean = null;
        
        /// <summary>
        /// The command name for XcelMean, specifically "mean"
        /// </summary>
        public override string CommandName
        {
            get { return "mean"; }
        }

        /// <summary>
        /// Overrides default behavior to accept command name "mean" or alternate "average".
        /// </summary>
        /// <param name="commandName">Command name for lookup</param>
        /// <returns>Boolean value indicating if command name is valid for this object</returns>
        public override bool HasCommandName(string commandName)
        {
            return base.HasCommandName(commandName) ||
                    (commandName.ToLower() == "average");
        }

        #region XcelCommand Members

        /// <summary>
        /// Performs mean value calculation on input argument list.
        /// </summary>
        public override void Execute()
        {
            if (!_mean.HasValue)
            {
                _mean = _args.Average();
            }

        }

        #endregion

        /// <summary>
        /// Returns mean value of arguement list if Execute()ed, otherwise empty string.
        /// </summary>
        /// <returns>String representation of output value</returns>
        public override string ToString()
        {
            if (_mean.HasValue)
            {
                return _mean.Value.ToString("0.###");
            }
            else
                return string.Empty;
        }
    }
}

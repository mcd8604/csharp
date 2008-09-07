using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xcel
{
    public class XcelMean : XcelCommand
    {
        protected double _mean;

        protected override string CommandName
        {
            get { return "mean"; }
        }

        public override bool HasCommandName(string commandName)
        {
            return base.HasCommandName(commandName) ||
                    (commandName.ToLower() == "average");
        }

        public XcelMean() { }

        #region XcelCommand Members

        public override void Execute()
        {
            _mean = _args.Average();

        }

        #endregion

        public override string ToString()
        {
            return _mean.ToString("0.###");
        }
    }
}

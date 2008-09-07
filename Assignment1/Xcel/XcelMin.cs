using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xcel
{
    public class XcelMin : XcelCommand
    {
        protected int _min;

        protected override string CommandName
        {
            get { return "min"; }
        }

        public XcelMin() { }

        #region XcelCommand Members

        public override void Execute()
        {
            _min = _args.Min();
        }

        #endregion

        public override string ToString()
        {
            return _min.ToString();
        }
    }
}

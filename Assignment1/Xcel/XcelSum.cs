using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xcel
{
    public class XcelSum : XcelCommand
    {
        protected int _sum;

        protected override string CommandName
        {
            get { return "sum"; }
        }

        public XcelSum() { }

        #region XcelCommand Members

        public override void Execute()
        {
            _sum = _args.Sum();
        }

        #endregion

        public override string ToString()
        {
            return _sum.ToString();
        }
    }
}

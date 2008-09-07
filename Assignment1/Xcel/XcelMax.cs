using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xcel
{
    public class XcelMax : XcelCommand
    {
        protected int _max;

        protected override string CommandName
        {
            get { return "max"; }
        }

        public XcelMax() { }

        #region XcelCommand Members

        public override void Execute()
        {
            _max = _args.Max();
        }

        #endregion

        public override string ToString()
        {
            return _max.ToString();
        }
    }
}

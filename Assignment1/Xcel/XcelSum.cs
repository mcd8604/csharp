using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Xcel
{
    public class XcelSum : XcelCommand
    {
        protected int? _sum = null;

        public override string CommandName
        {
            get { return "sum"; }
        }

        public XcelSum() { }

        #region XcelCommand Members

        public override void Execute()
        {
            if (!_sum.HasValue)
            {
                _sum = _args.Sum();
            }
        }

        #endregion

        public override string ToString()
        {
            return _sum.ToString();
        }
    }
}

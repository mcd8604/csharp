using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Xcel
{
    public class XcelDown : XcelCommand
    {
        protected int[] _down = null;

        protected override string CommandName
        {
            get { return "down"; }
        }

        public XcelDown() { }

        #region XcelCommand Members

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xcel
{
    public class XcelUp : XcelCommand
    {
        protected int[] _up;

        protected override string CommandName
        {
            get { return "up"; }
        }

        public XcelUp() { }

        #region XcelCommand Members

        public override void Execute()
        {
            _up = new int[_args.Length];
            _args.CopyTo(_up, 0);
            Array.Sort<int>(_up);
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            foreach(int i in _up) 
            {
                sb.Append(i);
                sb.Append(" ");
            }

            return sb.ToString();
        }
    }
}

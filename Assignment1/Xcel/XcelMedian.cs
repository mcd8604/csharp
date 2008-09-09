using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xcel
{
    public class XcelMedian : XcelCommand
    {
        protected double? _median = null;

        protected override string CommandName
        {
            get { return "median"; }
        }

        public XcelMedian() { }

        #region XcelCommand Members

        public override void Execute()
        {
            if (!_median.HasValue)
            {
                int[] argsCopy = new int[_args.Length];
                _args.CopyTo(argsCopy, 0);
                Array.Sort(argsCopy);

                //even
                if (argsCopy.Length % 2 == 0)
                {
                    _median = ((argsCopy[argsCopy.Length / 2 - 1] + argsCopy[argsCopy.Length / 2]) / 2.0);
                }

                //odd
                else
                {
                    _median = argsCopy[argsCopy.Length / 2];
                }
            }
        }

        #endregion

        public override string ToString()
        {
            return _median.Value.ToString("0.###");
        }
    }
}

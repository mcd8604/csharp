using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xcel
{
    public class XcelStdDev : XcelCommand
    {
        protected double? _stdDev = null;

        protected override string CommandName
        {
            get { return "stdev"; }
        }

        public XcelStdDev() { }

        #region XcelCommand Members

        public override void Execute()
        {
            if (!_stdDev.HasValue)
            {
                //mean
                double mean = _args.Average();

                //deviations
                double[] deviationsSquared = new double[_args.Length];
                for (int i = 0; i < _args.Length; ++i)
                {
                    deviationsSquared[i] = (_args[i] - mean) * (_args[i] - mean);
                }

                //variance
                double variance = deviationsSquared.Average();

                _stdDev = Math.Pow(variance, 0.5);
            }
        }

        #endregion

        public override string ToString()
        {
            return _stdDev.Value.ToString("0.###");
        }
    }
}

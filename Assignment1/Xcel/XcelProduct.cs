using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Xcel
{
    public class XcelProduct : XcelCommand
    {
        protected int? _product = null;

        public override string CommandName
        {
            get { return "product"; }
        }

        public XcelProduct() { }

        #region XcelCommand Members

        public override void Execute()
        {
            if (!_product.HasValue)
            {
                _product = 1;
                foreach (int i in _args)
                    _product *= i;
            }
        }

        #endregion

        public override string ToString()
        {
            return _product.ToString();
        }
    }
}

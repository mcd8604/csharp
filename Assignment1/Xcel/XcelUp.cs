using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Xcel
{
    /// <summary>
    /// An XcelCommand that sorts an array of integers in ascending order.
    /// </summary>
    public class XcelUp : XcelCommand
    {
        /// <summary>
        /// The array of sorted integers
        /// </summary>
        protected int[] _up = null;

        /// <summary>
        /// The string name of this command, returns "up"
        /// </summary>
        public override string CommandName
        {
            get { return "up"; }
        }

        #region XcelCommand Members

        /// <summary>
        /// Performs the calculation to sort the arguments
        /// </summary>
        public override void Execute()
        {
            if (_up == null)
            {
                _up = new int[_args.Length];
                _args.CopyTo(_up, 0);
                Array.Sort<int>(_up);
            }
        }

        #endregion

        /// <summary>
        /// Returns a string representation of the sorted integers
        /// </summary>
        /// <returns></returns>
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

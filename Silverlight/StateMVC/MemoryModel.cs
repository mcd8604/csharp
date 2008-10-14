using System;
using System.Collections.Generic;

namespace TerryAndMike.SilverlightGame.StateMVC
{
    public class MemoryModel : IModel
    {

        #region IModel Members

        public void NotifyStateChange(int row, int col)
        {
            throw new NotImplementedException();
        }

        public void Reset(int rows, int cols)
        {
            throw new NotImplementedException();
        }

        public void AddView(IView view)
        {
            throw new NotImplementedException();
        }

        public void RemoveView(IView view)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

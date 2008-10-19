using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database
{
    class DB: IDB
    {

        DB() { }

        #region IDB Members

        bool IDB.Add(Match match, object[] tuple)
        {
            throw new NotImplementedException();
        }

        object[] IDB.Extract(Match match, Report report)
        {
            throw new NotImplementedException();
        }

        int IDB.Delete(Match match)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

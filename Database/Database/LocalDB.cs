using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axel.Database;

namespace TerryAndMike.Database
{
    class LocalDB : IModel
    {
        protected IDB database = new DB();

        #region IModel Members

        public int Count
        {
            get { return database.Count; }
        }

        public string[][] Search(string[] keys)
        {
            object[] result = database.Extract( tryMatchTuple => MatchTuples(keys,tryMatchTuple) , reportedTuple => reportedTuple);
            string[][] strRet = new string[result.Length][];
            int i = 0;

            foreach (object tuple in result) {
                if (tuple is string[])
                    strRet[++i] = (tuple as string[]);
            }
            return strRet;
        }

        public bool Enter(string[] tuple)
        {
            return database.Add( tryMatchTuple => MatchTuples(tuple,tryMatchTuple), tuple );
        }

        public bool Remove(string[] keys)
        {
            return database.Delete( tryMatchTuple => MatchTuples(keys,tryMatchTuple) ) > 0;
        }

        protected static bool MatchTuples(object[] searchTuple, object[] dbTuple)
        {
            if (searchTuple.Length != dbTuple.Length) return false;

            for (int i = 0; i < searchTuple.Length; ++i) {
                //if search tuple value is not null and doesn't match, return false
                if (searchTuple[i] != null && searchTuple[i] != dbTuple[i])
                    return false;
            }

            return true;
        }

        #endregion
    }
}

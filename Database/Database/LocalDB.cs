using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axel.Database;

namespace TerryAndMike.Database
{
    /// <summary>
    /// Implements the IModel façade interface, abstracts read/writes to the DB
    /// </summary>
    public class LocalDB : IModel
    {
        /// <summary>
        /// Backend database, stores all tuples and iterates over them to perform queries
        /// </summary>
        protected IDB database = new DB();

        #region IModel Members

        /// <summary> number of entries in database. </summary>
        public int Count
        {
            get { return database.Count; }
        }

        /// <summary> finds matching tuples. </summary>
        /// <returns> words to be shown in each field. </returns>
        public string[][] Search(string[] keys)
        {
            object[] result = database.Extract( tryMatchTuple => MatchTuples(keys,tryMatchTuple) , reportedTuple => reportedTuple);
            string[][] strRet = new string[result.Length][];
            int i = 0;

            foreach (object tuple in result) {
                if (tuple is string[])
                    strRet[i++] = (tuple as string[]);
            }
            return strRet;
        }

        /// <summary> adds (or replaces) a tuple. </summary>
        /// <returns> true if tuple was added or replaced, false otherwise </returns>
        public bool Enter(string[] tuple)
        {
            database.Add( tryMatchTuple => MatchTuples(tuple,tryMatchTuple), tuple );
            return true; //all insertions succeed, DB.add() is true only if a tuple was _replaced_
        }

        /// <summary> removes tuples matching key </summary>
        /// <returns> returns true if something was removed. </returns>
        public bool Remove(string[] keys)
        {
            return database.Delete( tryMatchTuple => MatchTuples(keys,tryMatchTuple) ) > 0;
        }

        /// <summary>
        /// Compares two sets of tuples
        /// </summary>
        /// <param name="searchTuple">Tuple from Contoller to perform matching with.</param>
        /// <param name="dbTuple">Tuple from the database</param>
        /// <returns>boolean, true if tuples match</returns>
        protected static bool MatchTuples(string[] searchTuple, object[] dbTuple)
        {
            if (searchTuple.Length != dbTuple.Length) return false;

            for (int i = 0; i < searchTuple.Length; ++i) {
                //if search tuple value is not null and doesn't match (string.equals), return false
                if (searchTuple[i] != null && searchTuple[i] != (string)dbTuple[i])
                    return false;
            }

            return true;
        }

        #endregion
    }
}

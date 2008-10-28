using System;
using System.Collections.Generic;
using System.Text;
using Axel.Database;

namespace TerryAndMike.Database
{
    /// <summary> wraps and synchronizes <c>DB</c> as <c>IModel</c>. </summary>
    public class RemoteDB : DB<string>, IModel
    {
        protected DBService database;

        /// <summary> number of tuples, delegate to <c>list</c>. </summary>
        public int Count { get { return 0; } }

        /// <returns> what was found. </returns>>
        public string[][] Search(string[] keys)
        {
            return null;
        }

        /// <returns> true if something was added (and not replaced). </returns>
        public bool Enter(string[] tuple)
        {
            return true;
        }

        /// <returns> true if something was removed. </returns>
        public bool Remove(string[] keys)
        {
            return true;
        }

    }
}


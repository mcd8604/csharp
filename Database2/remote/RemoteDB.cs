using System;
using System.Collections.Generic;
using System.Text;
using Axel.Database;
using DBServiceSoapClient = remote.dbservice.DBServiceSoapClient;
using ArrayOfString = remote.dbservice.ArrayOfString;

namespace TerryAndMike.Database
{
    /// <summary> wraps and synchronizes <c>DB</c> as <c>IModel</c>. </summary>
    public class RemoteDB : DB<string>, IModel
    {
        protected DBServiceSoapClient model = new DBServiceSoapClient();

        /// <summary> number of tuples, delegate to <c>list</c>. </summary>
        public int Count { get { return model.get_Count(); } }

        /// <returns> what was found. </returns>>
        public string[][] Search(string[] keys)
        {

#warning is there a better way to do this?
            ArrayOfString aosKeys = new ArrayOfString();
            foreach (string s in keys)
                aosKeys.Add(s);

            ArrayOfString[] result = model.Search(aosKeys);

            string[][] retArr = new string[result.Length][];
            for(int i = 0; i < retArr.Length; ++i)
                retArr[i] = result[i].ToArray();
            
            return retArr;
        }

        /// <returns> true if something was added (and not replaced). </returns>
        public bool Enter(string[] tuple)
        {
#warning is there a better way to do this?
            ArrayOfString aosKeys = new ArrayOfString();
            foreach (string s in tuple)
                aosKeys.Add(s);

            return model.Enter(aosKeys);
        }

        /// <returns> true if something was removed. </returns>
        public bool Remove(string[] keys)
        {
#warning is there a better way to do this?
            ArrayOfString aosKeys = new ArrayOfString();
            foreach (string s in keys)
                aosKeys.Add(s);

            return model.Remove(aosKeys);
        }

    }
}


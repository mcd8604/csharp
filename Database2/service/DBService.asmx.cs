using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LocalDB = Axel.Database.LocalDB;

namespace service
{
    /// <summary>
    /// Summary description for DBService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DBService : System.Web.Services.WebService
    {

        private static LocalDB database = new LocalDB();

        /// <summary> number of entries in database. </summary>
        public int Count
        {
            [WebMethod]
            get { return database.Count; }
        }

        /// <summary> finds matching tuples. </summary>
        /// <returns> words to be shown in each field. </returns>
        [WebMethod]
        public string[][] Search(string[] keys)
        {
            return database.Search(keys);
        }

        /// <summary> adds (or replaces) a tuple. </summary>
        /// <returns> true if something was added (not replaced). </returns>
        [WebMethod]
        public bool Enter(string[] tuple)
        {
            //DB.Add returns true if tuple is replaced
            return database.Enter(tuple);
        }

        /// <summary> removes tuples matching key </summary>
        /// <returns> returns true if something was removed. </returns>
        [WebMethod]
        public bool Remove(string[] keys)
        {
            return database.Remove(keys);
        }
    }
}

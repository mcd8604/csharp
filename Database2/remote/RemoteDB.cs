using System;
using System.Collections.Generic;
using System.Text;
using Axel.Database;
using DBServiceSoapClient = remote.dbservice.DBServiceSoapClient;
using ArrayOfString = remote.dbservice.ArrayOfString;
using System.ServiceModel;

namespace TerryAndMike.Database
{
    /// <summary> wraps and synchronizes <c>DB</c> as <c>IModel</c>. </summary>
    public class RemoteDB : DB<string>, IModel
    {
        /// <summary> The database service client </summary>
        protected DBServiceSoapClient model;

        /// <summary>
        /// Contains connection error event data
        /// </summary>
        public class ConnectionErrorArgs : EventArgs {
            /// <summary>
            /// The connection error message.
            /// </summary>
            protected readonly string errorMsg;
            /// <summary>
            /// The connection error message.
            /// </summary>
            public string ErrorMsg { get { return errorMsg; } }
            /// <summary>
            /// Initializes a new instance of the ConnectionErrorArgs class.
            /// </summary>
            /// <param name="msg">The connection error message.</param>
            public ConnectionErrorArgs(string msg) { this.errorMsg = msg; }
        }

        /// <summary>
        /// Raised when a connection endpoint error is found.
        /// </summary>
        public event EventHandler<ConnectionErrorArgs> ConnectionEndpointError;

        /// <summary>
        /// Creates a new RemoteDB client connection. 
        /// </summary>
        /// <remarks> Raises a ConnectionError if endpoint of client is not found.</remarks>
        public void Connect()
        {
            model = new DBServiceSoapClient();

            // Test connection
            Call((CallRoutine<int>)delegate{ return model.get_Count(); });
        }

        /// <summary> number of tuples, delegate to <c>list</c>. </summary>
        public int Count { get { return Call((CallRoutine<int>)delegate { return model.get_Count(); }); } }

        /// <returns> what was found. </returns>>
        public string[][] Search(string[] keys)
        {
            return Call((CallRoutine<string[][]>)delegate
            {
                ArrayOfString aosKeys = new ArrayOfString();
                foreach (string s in keys)
                    aosKeys.Add(s);

                ArrayOfString[] result = model.Search(aosKeys);

                string[][] retArr = new string[result.Length][];
                for (int i = 0; i < retArr.Length; ++i)
                    retArr[i] = result[i].ToArray();

                return retArr;
            });
        }

        /// <returns> true if something was added (and not replaced). </returns>
        public bool Enter(string[] tuple)
        {
            return Call((CallRoutine<bool>)delegate
            {
                ArrayOfString aosKeys = new ArrayOfString();
                foreach (string s in tuple)
                    aosKeys.Add(s);

                return model.Enter(aosKeys);
            });
        }

        /// <returns> true if something was removed. </returns>
        public bool Remove(string[] keys)
        {
            return Call((CallRoutine<bool>)delegate
            {
                ArrayOfString aosKeys = new ArrayOfString();
                foreach (string s in keys)
                    aosKeys.Add(s);

                return model.Remove(aosKeys);
            });
        }

        /// <summary>
        /// Defines a call routine
        /// </summary>
        /// <typeparam name="T">The return type of the call</typeparam>
        /// <returns>The specified type</returns>
        private delegate T CallRoutine<T>();

        /// <summary>
        /// Executes a call routine
        /// </summary>
        /// <typeparam name="T">The return type of the call</typeparam>
        /// <param name="d">The call routine</param>
        /// <returns>The return of the call routine</returns>
        private T Call<T>(CallRoutine<T> d)
        {
            T retVal = default(T);
            try
            {
                retVal = d();
            }
            catch (EndpointNotFoundException)
            {
                if (ConnectionEndpointError != null) ConnectionEndpointError(this, new ConnectionErrorArgs("Web service not found, make sure the service is running."));
            }
            return retVal;
        }
    }
}


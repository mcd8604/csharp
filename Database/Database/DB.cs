using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Database
{
    /// <summary>
    /// Implementation of a "Poor Man's" database
    /// </summary>
    class DB: IDB
    {
        /// <summary>
        /// Performs the result of a match.
        /// </summary>
        /// <param name="matchedTuple">The tuple that was matched.</param>
        private delegate void MatchResult(object[] matchedTuple);

        /// <summary>
        /// The list of tuples in the database.
        /// </summary>
        protected List<object[]> tuples = new List<object[]>();

        /// <summary>
        /// Iterates over tuples, calls result for each successful match.
        /// </summary>
        /// <param name="match">Delegate to perform match.</param>
        /// <param name="result">Delegate to perform result of a match.</param>
        /// <returns>Returns number of matched tuples.</returns>
        private int Search(Match match, MatchResult result)
        {
            int matches = 0;

            for (int i = 0; i < tuples.Count; ++i)
            {
                if (match(tuples[i]))
                {
                    if (result != null) result(tuples[i]); 
                    ++matches;
                }
            }

            return matches;
        }

        #region IDB Members

        /// <summary>
        /// Adds a tuple and returns true if it replaces a tuple with equal content as determined by the Match argument.
        /// </summary>
        /// <param name="match">The delegate to specify how a tuple matches.</param>
        /// <param name="tuple"></param>
        /// <returns></returns>
        bool IDB.Add(Match match, object[] tuple)
        {
            // First validate tuple 
            foreach(object o in tuple) if(o != null) return false;

            // Replace each matched tuple
#warning UNTESTED, does 'matchedTuple = tuple' actually replace the value in the array?
            int numMatched = Search(match, matchedTuple => matchedTuple = tuple);

            //add new tuple
            if (numMatched == 0) tuples.Add(tuple);

            return numMatched > 0;
        }

        /// <summary>
        /// Returns a, possibly empty, array of values; each array element is constructed by the Report argument from one tuple selected by the Match argument.
        /// </summary>
        /// <param name="match">The delegate to specify how a tuple matches.</param>
        /// <param name="report">The delegate to perform the formatting of the tuple.</param>
        /// <returns>An array of formatted tuples.</returns>
        object[] IDB.Extract(Match match, Report report)
        {
            List<object> result = new List<object>();

            Search(match, tuple => result.Add(report(tuple)));

            return result.ToArray();
        }

        /// <summary>
        /// Removes all tuples selected by the Match argument and returns how many tuples were removed. 
        /// </summary>
        /// <param name="match">The delegate that specifies how a tuple matches.</param>
        /// <returns>The number of matched tuples.</returns>
        int IDB.Delete(Match match)
        {
            return Search(match, matchedTuple => tuples.Remove(matchedTuple));
        }

        /// <summary>
        /// The number of tuples in the database.
        /// </summary>
        int IDB.Count
        {
            get { return tuples.Count;  }
        }

        #endregion
    }
}

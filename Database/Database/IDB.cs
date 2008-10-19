using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database
{
    /// <summary>
    /// Compares one tuple to another.
    /// </summary>
    /// <param name="tuple1">The first tuple to compare.</param>
    /// <param name="tuple2">The second tuple to compare.</param>
    /// <returns>True of tuple1 has equal content as tuple2.</returns>
    delegate bool Match(object[] tuple1, object[] tuple2);

    delegate void Report();

    /// <summary>
    /// Flat "Poor man's" database interface
    /// </summary>
    interface IDB
    {
        /// <summary>
        /// Adds a tuple and returns true if it replaces a tuple with equal content as determined by the Match argument.
        /// </summary>
        /// <param name="match"></param>
        /// <param name="tuple"></param>
        /// <returns></returns>
        bool Add (Match match, object[] tuple);

        /// <summary>
        /// Returns a, possibly empty, array of values; each array element is constructed by the Report argument from one tuple selected by the Match argument.
        /// </summary>
        /// <param name="?"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        object[] Extract (Match match, Report report);
            
        /// <summary>
        /// Removes all tuples selected by the Match argument and returns how many tuples were removed. 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        int Delete (Match match);            

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Database
{
    /// <summary>
    /// Determines if a tuple matches certain criteria
    /// </summary>
    /// <param name="tuple">The tuple to match.</param>
    /// <returns>True of tuple matches criteria</returns>
    public delegate bool Match(object[] tuple);

    /// <summary>
    /// Formats the tuple into a report.
    /// </summary>
    /// <param name="tuple">The tuple to format.</param>
    /// <returns>The formatted tuple.</returns>
    public delegate object Report(object[] tuple);

    /// <summary>
    /// Flat "Poor man's" database interface
    /// </summary>
    public interface IDB
    {
        /// <summary>
        /// Adds a tuple and returns true if it replaces a tuple with equal content as determined by the Match argument.
        /// </summary>
        /// <param name="match">The delegate to specify how a tuple matches.</param>
        /// <param name="tuple"></param>
        /// <returns></returns>
        bool Add (Match match, object[] tuple);

        /// <summary>
        /// Returns a, possibly empty, array of values; each array element is constructed by the Report argument from one tuple selected by the Match argument.
        /// </summary>
        /// <param name="match">The delegate to specify how a tuple matches.</param>
        /// <param name="report">The delegate to perform the formatting of the tuple.</param>
        /// <returns>An array of formatted tuples.</returns>
        object[] Extract (Match match, Report report);

        /// <summary>
        /// Removes all tuples selected by the Match argument and returns how many tuples were removed. 
        /// </summary>
        /// <param name="match">The delegate that specifies how a tuple matches.</param>
        /// <returns>The number of matched tuples.</returns>
        int Delete (Match match);

        /// <summary>
        /// The number of tuples in the database.
        /// </summary>
        int Count { get; }
    }
}

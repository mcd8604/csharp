using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xcel
{
    /// <summary>
    /// Creates instances of XcelCommands
    /// </summary>
    static class XcelCommandFactory
    {
        #region Declare instances of each base class for factory

        static XcelSum sumCmd = new XcelSum();
        static XcelMin minCmd = new XcelMin();
        static XcelMax maxCmd = new XcelMax();
        static XcelUp upCmd = new XcelUp();
        static XcelDown downCmd = new XcelDown();
        static XcelMedian medianCmd = new XcelMedian();
        static XcelMean meanCmd = new XcelMean();
        static XcelStdDev stdevCmd = new XcelStdDev();

        #endregion

        /// <summary>
        /// Determines which command object to create given a string command name.
        /// </summary>
        /// <param name="commandName">The name of the command to create.</param>
        /// <returns>The created command.</returns>
        public static XcelCommand GetCommand(string commandName)
        {
            if (sumCmd.HasCommandName(commandName))
            {
               return new XcelSum();
            }
            else if (minCmd.HasCommandName(commandName))
            {
                return new XcelMin();
            }
            else if (maxCmd.HasCommandName(commandName))
            {
                return new XcelMax();
            }
            else if (upCmd.HasCommandName(commandName))
            {
                return new XcelUp();
            }
            else if (downCmd.HasCommandName(commandName))
            {
                return new XcelDown();
            }
            else if (medianCmd.HasCommandName(commandName))
            {
                return new XcelMedian();
            }
            else if (meanCmd.HasCommandName(commandName))
            {
                return new XcelMean();
            }
            else if (stdevCmd.HasCommandName(commandName))
            {
                return new XcelStdDev();
            }
            else
            {
                return null;
            }
        }

    }
}

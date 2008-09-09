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

        private static XcelSum sumCmd = new XcelSum();
        private static XcelMin minCmd = new XcelMin();
        private static XcelMax maxCmd = new XcelMax();
        private static XcelUp upCmd = new XcelUp();
        private static XcelDown downCmd = new XcelDown();
        private static XcelMedian medianCmd = new XcelMedian();
        private static XcelMean meanCmd = new XcelMean();
        private static XcelStdDev stdevCmd = new XcelStdDev();

        #endregion

        private static Dictionary<int[], XcelCommand> _commandHistory = new Dictionary<int[], XcelCommand>();

        /// <summary>
        /// Determines which command object to create given a string command name.
        /// </summary>
        /// <param name="commandName">The name of the command to create.</param>
        /// <returns>The created command.</returns>
        private static XcelCommand GetCommand(string commandName)
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

        /// <summary>
        /// Gets a command object given the command name and argument list. 
        /// </summary>
        /// <remarks>
        /// If the command for a given set of arguments has already been calculated, 
        /// this will return the existant object.
        /// </remarks>
        /// <param name="commandName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static XcelCommand GetCommand(string commandName, int[] args)
        {
            int[] key = new int[args.Length + 1];
            args.CopyTo(key, 0);
            key[key.Length - 1] = commandName.GetHashCode();

            if (_commandHistory.ContainsKey(key))
            {
                return _commandHistory[key];
            }
            else
            {
                return GetCommand(commandName);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Xcel
{
    /// <summary>
    /// Base class for Command pattern.
    /// </summary>
    public abstract class XcelCommand
    {
        /// <summary>
        /// The primary name for the command object.
        /// </summary>
        public abstract string CommandName { get; }

        /// <summary>
        /// Input argument list, used by command objects to run calculations on.
        /// </summary>
        protected int[] _args;

        /// <summary>
        /// Retrieve copy of argument list array, or throw an exception if argument list is empty.
        /// </summary>
        public int[] Args
        {
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Input argument array requires one or more elements");
                }

                _args = new int[value.Length];
                value.CopyTo(_args, 0);
            }
            get { return _args; }
        }

        /// <summary>
        /// Determines if the given command name is associated with the command.
        /// </summary>
        /// <remarks>A command object may have multiple command names by which it may be referred.</remarks>
        /// <param name="commandName">Command name for lookup</param>
        /// <returns>Boolean value indicating if command name is valid for that object</returns>
        public virtual bool HasCommandName(string commandName)
        {
            return (commandName.ToLower() == CommandName);
        }

        /// <summary>
        /// Performs execution on a given command object, the specific behavior varying by each command object.
        /// </summary>
        public abstract void Execute();
        
    }
}

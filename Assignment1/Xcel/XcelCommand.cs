using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xcel
{
    /// <summary>
    /// Base class for Command pattern.
    /// </summary>
    public abstract class XcelCommand
    {
        protected abstract string CommandName { get; }
        protected int[] _args;
        
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
        /// <param name="commandName"></param>
        /// <returns></returns>
        public virtual bool HasCommandName(string commandName)
        {
            return (commandName.ToLower() == CommandName);
        }

        public abstract void Execute();
        
    }
}

using System.Collections.Generic;
using System.Reflection;
using System;

namespace TerryAndMike.Xcel
{
    /// <summary>
    /// Generates/returns instances of XcelCommands given command name.
    /// </summary>
    public static class XcelCommandFactory
    {
        /// <summary>
        /// Array of XcelCommands to be instansiated
        /// </summary>
        private static readonly XcelCommand[] commands;

        /// <summary>
        /// Retrieves all subclasses of XcelCommand to populate the commands array
        /// </summary>
        static XcelCommandFactory()
        {
            commands = new XcelCommand[0];
            Type[] assemblyTypes = Assembly.GetAssembly(typeof(XcelCommand)).GetTypes();

            foreach (Type t in assemblyTypes)
            {
                if (t.IsSubclassOf(typeof(XcelCommand)))
                {
                    Array.Resize<XcelCommand>(ref commands, commands.Length + 1);
                    commands[commands.Length - 1] = (XcelCommand)t.GetConstructor(new Type[0]).Invoke(null);
                }
            }
        }

        /// <summary>
        /// Retrieve a list of names for all XcelCommand objects.
        /// </summary>
        /// <returns>String array of command names.</returns>
        public static string[] GetCommandNames()
        {
            string[] commandNames = new string[commands.Length];
            for (int i = 0; i < commands.Length; ++i)
                commandNames[i] = commands[i].CommandName;
            return commandNames;
        }

        private static Dictionary<int[], XcelCommand> _commandHistory = new Dictionary<int[], XcelCommand>();

        /// <summary>
        /// Determines which command object to create given a string command name.
        /// </summary>
        /// <param name="commandName">The name of the command to create.</param>
        /// <returns>The created command.</returns>
        private static XcelCommand GetCommand(string commandName)
        {
            foreach (XcelCommand command in commands)
            {
                if (command.HasCommandName(commandName))
                    //return command.Clone();
                    //NOTE: Reflection is slow
                    return (XcelCommand)command.GetType().GetConstructor(new Type[0]).Invoke(null);
            }

            throw new ArgumentException("Invalid Command Name: " + commandName);
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

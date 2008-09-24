using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Xcel
{
    class Program
    {
        /// <summary>
        /// Execution entry point
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static void Main(string[] args)
        {
            // Process command line arguments
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Input argument array requires one or more elements");
                return;
            }

            int[] intArgs = new int[args.Length];
            for (int i = 0; i < args.Length; ++i)
                intArgs[i] = int.Parse(args[i]);

            // Read input commands
            string inputCmd;
            XcelCommand xcelCmd;


            while ((inputCmd = Console.ReadLine()) != null)
            {
                if (inputCmd.ToLower() == "exit")
                    return;

                // Process input command
                xcelCmd = XcelCommandFactory.GetCommand(inputCmd, intArgs);

                if (xcelCmd != null)
                {
                    try
                    {
                        xcelCmd.Args = intArgs;
                        xcelCmd.Execute();
                        Console.WriteLine(xcelCmd);
                    }
                    catch (ArgumentException ae)
                    {
                        Console.Error.WriteLine(ae.Message);
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine(e.Message);
                        Console.Error.WriteLine(e.StackTrace);
                    }
                }
            }

        }
    }
}

﻿using Console = System.Console;
using TerryAndMike.SilverlightGame.StateMVC;

namespace TerryAndMike.SilverlightGame.TestConsole
{
    /// <summary>
    /// A simple console test client, or driver, to test the PuzzleModel
    /// </summary>
    class PuzzleModelTestDriver : IView, IController
    {
        private IModel model = new PuzzleModel();

        public PuzzleModelTestDriver()
        {
            model.AddView(this);
        }

        #region IView Members

        public void StateUpdated(int row, int col, int tile)
        {
            //tile of zero indicates blank space
            if ( tile != 0 )
                Console.WriteLine(row+","+col+" "+tile);
        }

        #endregion

        #region IController Members

        public void NotifyStateChange(int row, int col)
        {
            model.NotifyStateChange(row, col);
        }

        public void Reset(int row, int col)
        {
            model.Reset(row, col);
        }

        #endregion



        static void Main(string[] args)
        {
            PuzzleModelTestDriver myDriver = new PuzzleModelTestDriver();
            string[] rowcol;
            char[] spaceCharArray = new char[] { ' ' };
            
            /**** Read in puzzle size and Reset() to initialize puzzle ****/
            do
            {
                Console.Write("Enter puzzle size as \"#rows #cols\": ");
                rowcol = Console.ReadLine().Split(spaceCharArray);
            } while (rowcol.Length != 2);

            myDriver.Reset(int.Parse(rowcol[0]), int.Parse(rowcol[1]));


            /**** Loop until "quit" token, simulating quits calling ShiftMakeBlank ****/
            string input = string.Empty;
            do
            {
                Console.Write("\nEnter \"row col\" of tile to 'click' or \"quit\" to quit: ");
                input = Console.ReadLine();
                rowcol = input.Split(spaceCharArray);
                if (rowcol.Length == 2)
                {
                    myDriver.NotifyStateChange(int.Parse(rowcol[0]), int.Parse(rowcol[1]));
                }
            } while (!input.Equals("quit", System.StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
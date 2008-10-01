using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Axel.Sudoku;

namespace TerryAndMike.Sudoku
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <remarks>
        /// Creates a board and displays a SudokuForm
        /// </remarks>
        [STAThread]
        static void Main(string[] args)
        {
            List<string> inputBuf = new List<string>();
            int blankIndex = 0; //records index of blank line between board and Set() commands

            //if input file passed as an arguement
            if (args.Length > 0)
            {
                inputBuf.AddRange(System.IO.File.ReadAllLines(args[0]));

                blankIndex = inputBuf.IndexOf(string.Empty);
            }

            //else read from stdin until EOL
            else
            {
                string strBuf;

                while ((strBuf = System.Console.ReadLine()) != null)
                {
                    if (strBuf == string.Empty)
                        blankIndex = inputBuf.Count;

                    inputBuf.Add(strBuf);
                }
            }


            //Construct Board
            string[] boardLines = new string[blankIndex];
            inputBuf.CopyTo(0, boardLines, 0, boardLines.Length);

            IClearableBoard myBoard = new ClearableBoard(boardLines);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GUI.SudokuForm sForm = new GUI.SudokuForm(myBoard.Dimension, myBoard.Shapes);
            
            sForm.CellSet += ((cellIndex, digit) => myBoard.Set(cellIndex, digit));
            sForm.CellCleared += (cellIndex => myBoard.Clear(cellIndex));

            myBoard.AddObserver(sForm.Observer);

            //Read Set() Parameters and send them to the Board.
            for (int i = blankIndex + 1; i < inputBuf.Count; ++i)
            {
                string[] line = inputBuf[i].Split(new char[] { ' ' });
                int cell = int.Parse(line[0]);
                int digit = int.Parse(line[1]);
                myBoard.Set(cell, digit);
            }


            //Set up and display a SudokuForm, bring focus to that form even in console app
            Application.Run(sForm);
            sForm.Activate();
        }
    }
}

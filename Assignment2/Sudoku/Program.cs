using File = System.IO.File;
using Array = System.Array;

namespace TerryAndMike.Sudoku
{
    /// <summary>
    /// A Test program which constructs a Board from lines read (up to a blank line) from standard input, 
    /// and which then reads Set  parameters, i.e., lines with a cell index and a digit separated by white space, 
    /// and sends them to the Board. 
    /// </summary>
    class Program
    {
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //construct a Board from lines read (up to a blank line) from standard input
            string[] allLines = File.ReadAllLines("board.txt");
            string[] boardLines;

            int blankIndex = Array.IndexOf(allLines, string.Empty);

            boardLines = new string[blankIndex];
            Array.Copy(allLines, 0, boardLines, 0, boardLines.Length);

            Board testBoard = new Board(boardLines);

            //reads Set  parameters, i.e., lines with a cell index and a digit separated by white space, and sends them to the Board.

            string[] setParams;
            setParams = new string[allLines.Length - (blankIndex + 1)];
            Array.Copy(allLines, blankIndex + 1, setParams, 0, setParams.Length);

            foreach (string param in setParams)
            {
                string[] line = param.Split(new char[] {' '} );
                int cell = int.Parse(line[0]);
                int digit = int.Parse(line[1]);
                testBoard.Set(cell, digit);
            }
        }
    }
}

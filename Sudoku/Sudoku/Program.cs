using System.Collections.Generic;

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
            List<string> inputBuf = new List<string>();
            int blankIndex=0; //records index of blank line between board and Set() commands

            //if input file passed as an arguement
            if ( args.Length > 0 ) {
                inputBuf.AddRange( System.IO.File.ReadAllLines( args[ 0 ] ) );

                blankIndex = inputBuf.IndexOf( string.Empty );
            }

            //else read from stdin until EOL
            else {
                string strBuf;

                while ( ( strBuf = System.Console.ReadLine() ) != null ) {
                    if ( strBuf == string.Empty )
                        blankIndex = inputBuf.Count;

                    inputBuf.Add( strBuf );
                }
            }


            //Construct Board
            string[] boardLines = new string[ blankIndex ];
            inputBuf.CopyTo(0, boardLines, 0, boardLines.Length);

            Board myBoard = new Board(boardLines);
            Observer myObserver = new Observer();
            myBoard.AddObserver(myObserver);

            
            
            //Read Set() Parameters and send them to the Board.
            for ( int i = blankIndex + 1; i < inputBuf.Count; ++i ) {
                string[] line = inputBuf[ i ].Split( new char[] { ' ' } );
                int cell = int.Parse( line[ 0 ] );
                int digit = int.Parse( line[ 1 ] );
                myBoard.Set( cell, digit );
            }


            
            System.Console.WriteLine("Press Enter to exit");
            System.Console.ReadLine();

        }
    }
}

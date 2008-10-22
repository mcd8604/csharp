using System;
using System.Collections.Generic;
using TextWriter = System.IO.TextWriter;
using TextReader = System.IO.TextReader;
using TerryAndMike.Database;
using Axel.Database;


namespace TerryAndMike.Database.TestApp {
    class DBTestApp {

        IModel model = new LocalDB();

        public void CommandParseLoop( TextWriter outStream, TextReader inStream ) {
            string command = string.Empty;
            string[] commandTokens;

            OutputInstructions( outStream );

            while ( !command.Equals( "quit", StringComparison.CurrentCultureIgnoreCase ) ) {

                outStream.Write( "Enter a command: " );
                command = inStream.ReadLine();
                commandTokens = command.Split( ' ' );

                switch ( commandTokens[ 0 ] ) {
                    case "add":
                    case "Add":
                        outStream.WriteLine( "adding" );
                        break;
                    case "extract":
                    case "Extract":
                        outStream.WriteLine( "extracting" );
                        break;
                    case "delete":
                    case "Delete":
                        outStream.WriteLine( "deleting" );
                        break;
                }
            }
        }

        private void OutputInstructions( TextWriter outStream ) {
            outStream.WriteLine( "Available commands:\n\tadd [word]...\n\textract value-position key-position pattern\n\tdelete key-position pattern" );
        }

        static void Main( string[] args ) {
            

            

        }
    }
}

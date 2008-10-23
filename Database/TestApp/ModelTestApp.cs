using System;
using System.Collections.Generic;
using TextWriter = System.IO.TextWriter;
using TextReader = System.IO.TextReader;
using TerryAndMike.Database;
using Axel.Database;


namespace TerryAndMike.Database.TestApp {
    class ModelTestApp {
        private const int TUPLE_LENGTH = 3;

        IModel model;

        public ModelTestApp() : this( new LocalDB() ) { }

        public ModelTestApp( IModel model ) {
            this.model = model;
        }

        public void CommandParseLoop( TextWriter outStream, TextReader inStream ) {
            string command = string.Empty;
            string[] commandTokens;

            OutputInstructions( outStream );

            while ( !command.Equals( "quit", StringComparison.CurrentCultureIgnoreCase ) ) {

                outStream.Write( "\nEnter a command: " );
                command = inStream.ReadLine().Trim();
                commandTokens = command.Split( ' ' );

                switch ( commandTokens[ 0 ] ) {
                    case "add":
                    case "Add":
                        OutputAddOperation( outStream, commandTokens );
                        break;
                    case "extract":
                    case "Extract":
                        OutputExtractOperation( outStream, commandTokens );
                        break;
                    case "delete":
                    case "Delete":
                        OutputDeleteOperation( outStream, commandTokens );
                        break;
                }
            }
        }

        private string[] GetPatternTuple( int keyFieldId, string key, int tupleLength ) {
            string[] patternInputArray = new string[ tupleLength ];
            for ( int i = 0; i < tupleLength; ++i )
                patternInputArray[ i ] = ( i == keyFieldId ) ? key : null;
            return patternInputArray;
        }

        /// <summary>
        /// Search all tuples for a key in a specified key field, extracting corresponding value field specified
        /// </summary>
        /// <param name="valueFieldId">Zero-based field id for the requested value field</param>
        /// <param name="keyFieldId">Zero-based field id for the search key</param>
        /// <param name="key">Key to search for in field 'keyFieldId'</param>
        /// <returns>Matrix (Nx2), representing N (matching length) 2-tuples, columns are key and value fields</returns>
        private string[ , ] ExtractFieldByPattern( int valueFieldId, int keyFieldId, string key ) {
            string[] patternInputArray = GetPatternTuple( keyFieldId, key, TUPLE_LENGTH );
            string[][] dbResults = model.Search( patternInputArray );
            string[ , ] resultMatrix = new string[ dbResults.Length, 2 ];

            for ( int i = 0; i < dbResults.Length; ++i ) {
                resultMatrix[ i, 0 ] = dbResults[ i ][ keyFieldId ];
                resultMatrix[ i, 1 ] = dbResults[ i ][ valueFieldId ];
            }
            return resultMatrix;
        }

        private void OutputExtractOperation( TextWriter outStream, string[] commandTokens ) {
            if ( commandTokens.Length != 4 )
                return;

            string[ , ] results = ExtractFieldByPattern( Int32.Parse( commandTokens[ 1 ] ), Int32.Parse( commandTokens[ 2 ] ), commandTokens[ 3 ] );

            //output results
            outStream.WriteLine( "(key, value)\n------------" );
            for ( int i = 0; i < results.GetLength( 0 ); ++i ) {
                outStream.WriteLine( '(' + results[ i, 0 ] + ", " + results[ i, 1 ] + ")" );
            }
        }

        private void OutputDeleteOperation( TextWriter outStream, string[] commandTokens ) {
            string[] patternInputArray = GetPatternTuple( Int32.Parse( commandTokens[ 1 ] ), commandTokens[ 2 ], TUPLE_LENGTH );
            if ( model.Remove( patternInputArray ) )
                Console.WriteLine( "All tuples matching input pattern have been removed.  " + model.Count + " tuples remain." );
            else
                Console.WriteLine( "Removal of tuples maching input pattern failed or no tuples were matched." );
        }

        private void OutputAddOperation( TextWriter outStream, string[] commandTokens ) {
            string output = string.Empty;
            string[] tuple = new string[ commandTokens.Length - 1 ];
            Array.Copy( commandTokens, 1, tuple, 0, tuple.Length );

            if ( model.Enter( tuple ) ) {
                for ( int i = 0; i < tuple.Length; ++i ) {
                    output += tuple[ i ];
                    if ( i + 1 < tuple.Length )
                        output += ", ";
                }

                outStream.WriteLine( "Tuple (" + output + ") successfully stored.  Total tuple count: " + model.Count );
            }
            else {
                outStream.WriteLine( "Error adding tuple, tuple was either duplicate or not added for another reason." );
            }
        }

        private void OutputInstructions( TextWriter outStream ) {
            outStream.WriteLine( "Available commands:\n\tadd [word]...\n\textract value-position key-position pattern\n\tdelete key-position pattern\n\tquit\n" );
        }

        static void Main( string[] args ) {
            ModelTestApp tester = new ModelTestApp( new LocalDB() );
            tester.CommandParseLoop( Console.Out, Console.In );         
        }
    }
}

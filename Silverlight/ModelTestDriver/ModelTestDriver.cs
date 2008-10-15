using Console = System.Console;
using TerryAndMike.SilverlightGame.StateMVC;

namespace TerryAndMike.SilverlightGame.TestConsole
{
    /// <summary>
    /// A simple console test client, or driver, to test the PuzzleModel
    /// </summary>
    class ModelTestDriver : IView, IController
    {
        private IModel model;

        public ModelTestDriver( IModel model )
        {
            this.model = model;
            this.model.AddView(this);
        }

        #region IView Members

        public void StateUpdated(int row, int col, int tile)
        {
            //tile of zero indicates blank space in PuzzleModel
            if ( !(model is PuzzleModel) || ((model is PuzzleModel) && (tile != 0)))
                Console.WriteLine(row+","+col+" "+tile);
        }


        public void StateVisibilityUpdated(int row, int col, bool visible)
        {
            Console.WriteLine(row+","+col+" "+"is now "+(visible?"":"not ")+"visible");
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
            IModel startModel = null;

            Console.Write( "Choose model type to test: \n\t(1) Puzzle \n\t(2) Blackout\n\t(3) Memory\n\n?: ");
            string modelType = Console.ReadLine();
            switch ( modelType ) {
                case "1":
                case "puzzle":
                case "Puzzle":
                    startModel = new PuzzleModel();
                    break;
                case "2":
                case "blackout":
                case "Blackout":
                    startModel = new BlackoutModel();
                    break;
                case "3":
                case "memory":
                case "Memory":
                    startModel = new MemoryModel();
                    break;
            }

            ModelTestDriver myDriver;

            try {
                myDriver = new ModelTestDriver( startModel );
                string[] rowcol;
                char[] spaceCharArray = new char[] { ' ' };

                /**** Read in puzzle size and Reset() to initialize puzzle ****/
                do {
                    Console.Write( "Enter board size as \"#rows #cols\": " );
                    rowcol = Console.ReadLine().Split( spaceCharArray );
                } while ( rowcol.Length != 2 );

                myDriver.Reset( int.Parse( rowcol[ 0 ] ), int.Parse( rowcol[ 1 ] ) );


                /**** Loop until "quit" token, simulating clicks calling NotifyStateChange ****/
                string input = string.Empty;
                do {
                    Console.Write( "\nEnter \"row col\" of tile to send state change or \"quit\" to quit: " );
                    input = Console.ReadLine();
                    rowcol = input.Split( spaceCharArray );
                    if ( rowcol.Length == 2 ) {
                        myDriver.NotifyStateChange( int.Parse( rowcol[ 0 ] ), int.Parse( rowcol[ 1 ] ) );
                    }
                } while ( !input.Equals( "quit", System.StringComparison.CurrentCultureIgnoreCase ) );
            }
            catch ( System.NullReferenceException ) {
                Console.Error.WriteLine( "Invalid choice." );
            }
        }


    }
}

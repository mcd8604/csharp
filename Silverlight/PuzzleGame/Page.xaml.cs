using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using IView = TerryAndMike.SilverlightGame.StateMVC.IView;
using IController = TerryAndMike.SilverlightGame.StateMVC.IController;
using StringBuilder = System.Text.StringBuilder;

namespace TerryAndMike.SilverlightGame.PuzzleGame
{
    /// <summary>
    /// Implements the IView for Puzzle 15
    /// </summary>
    public partial class Page : UserControl, IView
    {
        /// <summary>
        /// Creates a new instance of Page
        /// </summary>
        public Page()
        {
            InitializeComponent();
        }

        #region IView Members

        /// <summary>
        /// Updates the View to represent the state of the IModel.
        /// </summary>
        /// <param name="row">The row of the tile.</param>
        /// <param name="col">The column of the tile.</param>
        /// <param name="tile">The tile that was set.</param>
        public void StateUpdated(int row, int col, int tile)
        {
            StringBuilder sb = new StringBuilder(outputTextBox.Text);
            sb.AppendLine(row + "," + col + " " + tile);
            outputTextBox.Text = sb.ToString();
            ScrollOutputToBottom();            
        }

        #endregion

        #region Event Management

        /// <summary>
        /// Occurs when the reset button is pressed.
        /// </summary>
        public event StateMVC.State2 Reset;

        /// <summary>
        /// Occurs when a tile shift is entered.
        /// </summary>
        public event StateMVC.State2 ShiftMakeBlank;

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            if(Reset != null)
                Reset( App.NUM_ROWS, App.NUM_COLS );
        }

        private void inputTextBox_KeyDown( object sender, KeyEventArgs e ) {
            if ( ShiftMakeBlank != null && e.Key == Key.Enter ) {
                TextBox tbSender = sender as TextBox;
                if ( tbSender == null )
                    return;

                string[] inputCoordinates = tbSender.Text.Split(new char[] {' '});
                if ( inputCoordinates.Length != 2 ) {
                    outputTextBox.Text += "Error with formatting of input.\n";
                    inputTextBox.SelectAll();
                    ScrollOutputToBottom();
                    return;
                }

                int[] iInputCoordinates = new int[ 2 ];
                if ( !int.TryParse( inputCoordinates[ 0 ], out iInputCoordinates[ 0 ] ) ||
                     !int.TryParse( inputCoordinates[ 1 ], out iInputCoordinates[ 1 ] ) ) {

                    outputTextBox.Text += "Error parsing input as integers.\n";
                    inputTextBox.SelectAll();
                    ScrollOutputToBottom();
                    return;
                }
                else {
                    ShiftMakeBlank( iInputCoordinates[ 0 ], iInputCoordinates[ 1 ] );
                    tbSender.Text = "";
                }
            }
        }

        private void ScrollOutputToBottom() {
            //hack to scroll to bottom
            outputTextBox.Select( outputTextBox.Text.Length - 1, 1 );
        }

        #endregion
    }
}

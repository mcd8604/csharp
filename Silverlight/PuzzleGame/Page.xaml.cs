using System.Windows.Controls;
using IView = TerryAndMike.SilverlightGame.StateMVC.IView;
using IController = TerryAndMike.SilverlightGame.StateMVC.IController;
using StringBuilder = System.Text.StringBuilder;

namespace TerryAndMike.SilverlightGame.PuzzleGame
{
    /// <summary>
    /// The IView implementation.
    /// </summary>
    public partial class Page : UserControl, IView
    {
        private IController controller;

        /// <summary>
        /// Creates a new instance of Page
        /// </summary>
        /// <param name="controller">The IContoller implement</param>
        public Page()
        {
            InitializeComponent();
            this.inputTextBox.KeyDown += new System.Windows.Input.KeyEventHandler(inputTextBox_KeyDown);
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
            StringBuilder sb = new StringBuilder(this.outputTextBox.Text);
            sb.AppendLine(row + "," + col + " " + tile);
            this.outputTextBox.Text = sb.ToString();
        }

        #endregion

#region Event Management

        /// <summary>
        /// Occurs when the reset button is pressed
        /// </summary>
        public event StateMVC.State Reset;

        private void resetButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Reset != null)
            {                
                Reset(4, 4, -1);
            }
        }

        /// <summary>
        /// Occurs when a move is entered
        /// </summary>
        public event StateMVC.State Move;

        void inputTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Move != null && e.Key == System.Windows.Input.Key.Enter)
            {
                int row = 0;
                int col = 0;
                string s = this.inputTextBox.Text;

                if (s.Length > 3)
                {
                    string[] sep = {" ", ",", ", "};
                    string[] strings = s.Split(sep, System.StringSplitOptions.RemoveEmptyEntries);

                    if (strings[0] != null)
                        row = int.Parse(strings[0]);

                    if (strings[1] != null)
                        col = int.Parse(strings[1]);
                }

                Move(row, col, -1);
            }
        }

#endregion
    }
}

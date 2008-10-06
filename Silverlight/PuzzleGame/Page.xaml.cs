using System.Windows.Controls;
using IView = TerryAndMike.SilverlightGame.StateMVC.IView;
using IController = TerryAndMike.SilverlightGame.StateMVC.IController;
using StringBuilder = System.Text.StringBuilder;

namespace TerryAndMike.SilverlightGame.PuzzleGame
{
    public partial class Page : UserControl, IView
    {
        private IController controller;

        /// <summary>
        /// Creates a new instance of Page
        /// </summary>
        /// <param name="controller">The IContoller implement</param>
        public Page(IController controller)
        {
            this.controller = controller;
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
            StringBuilder sb = new StringBuilder(this.outputTextBox.Text);
            sb.AppendLine(row + "," + col + " " + tile);
            this.outputTextBox.Text = sb.ToString();
        }

        #endregion

        private void resetButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            controller.Reset();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using IView = TerryAndMike.SilverlightGame.StateMVC.IView;
using State = TerryAndMike.SilverlightGame.StateMVC.StateToModel;

namespace Memory
{
    /// <summary>
    /// Implementation of IView for Memory
    /// </summary>
    public partial class Page : UserControl, IView
    {
        ClipTable clipTable;

        /// <summary>
        /// Creates a new instance of Page.
        /// </summary>
        public Page()
        {
            InitializeComponent();
            clipTable = new ClipTable();
            clipTable.ClipClicked += new State(clipTable_TileClicked);
            Grid.SetRow(clipTable, 1);
            Grid.SetColumnSpan(clipTable, 4);
            this.LayoutRoot.Children.Add(clipTable);
        }

        #region IView Members

        /// <summary>
        /// Updates the IView to represent the state of an IModel.
        /// </summary>
        /// <param name="row">The row of the tile.</param>
        /// <param name="col">The column of the tile.</param>
        /// <param name="tile">The tile that was set.</param>
        public void StateUpdated(int row, int col, int tile)
        {
            clipTable.SetClip(row, col, tile);
        }

        /// <summary>
        /// Updates the IView to represent the visibility state of an IModel.
        /// </summary>
        /// <param name="row">The row of the tile.</param>
        /// <param name="col">The column of the tile.</param>
        /// <param name="visible">True if visible, false of not.</param>
        public void StateVisibilityUpdated(int row, int col, bool visible)
        {
            clipTable.SetClipVisible(row, col, visible);
        }

        #endregion

        #region Event Management

        /// <summary>
        /// Occurs when the reset button is clicked.
        /// </summary>
        public event State Reset;

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            int col = 0;
            int row = 0;
            if (int.TryParse(colTextBox.Text, out col) && int.TryParse(rowTextBox.Text, out row))
            {
                if (row * col <= (ClipTable.NUM_IMAGES - 1) * 2)
                {
                    clipTable.Initialize(row, col, this.ActualWidth, this.LayoutRoot.RowDefinitions[1].ActualHeight);
                    if (Reset != null)
                    {
                        Reset(row, col);
                    }
                }
            }
        }

        /// <summary>
        /// Occurs when a tile shift input is received.
        /// </summary>
        public event State TileClicked;

        void clipTable_TileClicked(int row, int col)
        {
            if (TileClicked != null)
                TileClicked(row, col);
        }

        #endregion

    }
}

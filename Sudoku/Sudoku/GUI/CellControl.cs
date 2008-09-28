using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TerryAndMike.Sudoku.GUI
{
    /// <summary>
    /// A Cell which contains either one or nine labels with large or small digits. 
    /// Geometry based on the construction parameter: the size of one of the small, square labels. 
    /// </summary>
    public partial class CellControl : UserControl
    {
        private int labelSize;

        private Label[] labels = new Label[9];

        /// <summary>
        /// Creates a new instance of CellControl
        /// </summary>
        /// <param name="candidateLabelSize"></param>
        public CellControl(int candidateLabelSize)
        {
            this.labelSize = candidateLabelSize;
            InitializeComponent();

            /// Sets the size and position of candidate labels
            for (int i = 0; i < labels.Length; ++i)
            {
                Label l = new Label();
                l.AutoSize = true;
                l.Location = new System.Drawing.Point(3, 3);
                l.Margin = new System.Windows.Forms.Padding(0);
                l.Name = "label" + (i + 1);
                l.Size = new System.Drawing.Size(13, 13);
                l.Tag = (i + 1);
                l.Text = (i + 1).ToString();
                l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                l.MouseClick += new System.Windows.Forms.MouseEventHandler(this.candidate_MouseClick);
                this.Controls.Add(l);
                this.labels[i] = l;
            }
        }

        #region Event Management

        /// <summary>
        /// Selecting a candidate enters it into a big label in its cell and hides it from its context (row, column, and box). 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void candidate_MouseClick(object sender, MouseEventArgs e)
        {
            Label sLabel = sender as Label;
            if (sLabel != null)
            {
                int cVal = (int)sLabel.Tag;
                // TODO:
            }
        }
        
        // TODO: Selecting a big digit will remove it and redisplay the appropriate candidates in its context. 

        #endregion 
    }
}

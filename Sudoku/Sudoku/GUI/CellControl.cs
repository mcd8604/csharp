using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace TerryAndMike.Sudoku.GUI
{
    delegate void CellSetHandler(int cellIndex, int digit);
    delegate void CellClearHandler(int cellIndex);

    /// <summary>
    /// A Cell which contains either one or nine labels with large or small digits. 
    /// Geometry based on the construction parameter: the size of one of the small, square labels. 
    /// </summary>
    public partial class CellControl : UserControl
    {
        private readonly int labelSize;

        private readonly Label[] candidateLabels = new Label[9];

        private readonly int index;

        private int? digit;

        /// <summary>
        /// Creates a new instance of CellControl
        /// </summary>
        /// <param name="candidateLabelSize"></param>
        public CellControl(int candidateLabelSize, int dimension, int index)
        {
            this.labelSize = candidateLabelSize;
            this.index = index;
            InitializeComponent();

            /// Sets the size and position of candidate labels
            for (int i = 0; i < candidateLabels.Length; ++i)
            {
                Label l = new Label();
                l.AutoSize = true;

                // Square root of dimension equals number of labels per row and column
                l.Location = new System.Drawing.Point((i % (int)Math.Sqrt(dimension)) * candidateLabelSize, (i / (int)Math.Sqrt(dimension)) * candidateLabelSize);
                
                l.Margin = new System.Windows.Forms.Padding(0);
                l.Name = "label" + (i + 1);
                l.Size = new System.Drawing.Size(candidateLabelSize, candidateLabelSize);
                l.Tag = (i + 1);
                l.Text = (i + 1).ToString();
                l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                l.MouseClick += new System.Windows.Forms.MouseEventHandler(this.candidate_MouseClick);
                this.Controls.Add(l);
                this.candidateLabels[i] = l;
            }

            this.Width = this.candidateLabels[this.candidateLabels.Length - 1].Bounds.Right;
            this.Height = this.candidateLabels[this.candidateLabels.Length - 1].Bounds.Bottom;
        }

        #region Event Management

        event CellSetHandler CellSet;
        event CellClearHandler CellClear;

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
                this.digit = (int)sLabel.Tag;
                
                // TODO:
                Console.WriteLine("Index: " + index + ", Digit: " + digit);

                if (CellSet != null && digit != null)
                    CellSet(this.index, this.digit.Value);
            }
        }
        
        // TODO: Selecting a big digit will remove it and redisplay the appropriate candidates in its context. 

        #endregion 
    }
}

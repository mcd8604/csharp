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
    /// <summary>
    /// Delegate type for view to notify controller of a UI cell 'set' action.
    /// </summary>
    /// <remarks>View will not update until model notifies observers of 'set' action.</remarks>
    /// <param name="cellIndex">Index for cell that has been set</param>
    /// <param name="digit">Digit that cell has been set to</param>
    public delegate void SetEventHandler(int cellIndex, int digit);

    /// <summary>
    /// Delegate type for view to notify controller of a UI cell 'clear' (un-set) action.
    /// </summary>
    /// <remarks>View will not update until model notifies observers of 'possible' candidates for that cell.</remarks>
    /// <param name="cellIndex">Index for cell that is to be un-set</param>
    public delegate void ClearEventHandler(int cellIndex);

    /// <summary>
    /// A Cell which contains either one or nine labels with large or small digits. 
    /// Geometry based on the construction parameter: the size of one of the small, square labels. 
    /// </summary>
    public partial class CellControl : UserControl
    {
        private readonly int labelSize;

        private readonly Label[] candidateLabels;
        private readonly Label setLbl;

        private readonly int index;

        /// <summary>
        /// Creates a new instance of CellControl
        /// </summary>
        /// <param name="candidateLabelSize">The size (in px) of a candidate digit label</param>
        /// <param name="dimension">Dimmension of the board (cells on one side)</param>
        /// <param name="index">Index of cell in board, to establish identity</param>
        public CellControl(int candidateLabelSize, int dimension, int index)
        {
            this.labelSize = candidateLabelSize;
            candidateLabels = new Label[dimension];
            this.index = index;

            InitializeComponent();

            // Sets the size and position of candidate labels
            for (int i = 0; i < candidateLabels.Length; ++i)
            {
                Label lbl = new Label();
                lbl.AutoSize = true;

                // Square root of dimension equals number of labels per row and column
                lbl.Location = new System.Drawing.Point((i % (int)Math.Sqrt(dimension)) * candidateLabelSize, (i / (int)Math.Sqrt(dimension)) * candidateLabelSize);

                lbl.Margin = new Padding(0);
                lbl.Name = "label" + (i + 1);
                lbl.Size = new System.Drawing.Size(candidateLabelSize, candidateLabelSize);
                lbl.Tag = (i + 1);
                lbl.Text = (i + 1).ToString();
                lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                lbl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.candidate_MouseClick);
                this.Controls.Add(lbl);
                this.candidateLabels[i] = lbl;
            }

            setLbl = new Label();
            setLbl.AutoSize = false;
            setLbl.Location = new System.Drawing.Point(0, 0);
            setLbl.Margin = new System.Windows.Forms.Padding(0);
            setLbl.Name = "set";
            setLbl.Size = new System.Drawing.Size(Width, Height);
            setLbl.Text = "";
            Label label0 = this.candidateLabels[0];
            float ratio = label0.Font.SizeInPoints / label0.Font.Height;
            setLbl.Font = new Font(label0.Font.FontFamily, setLbl.Height * ratio);
            setLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            setLbl.MouseClick += new System.Windows.Forms.MouseEventHandler(candidate_MouseClick);
            setLbl.Visible = false;
            this.Controls.Add(setLbl);

            
        }

        #region Event Management

        /// <summary>
        /// Event queue for actions upon cell digit selection.
        /// </summary>
        /// <remarks>A CellControl's BoardControl adds a handler to this queue.</remarks>
        public event SetEventHandler CellSet;

        /// <summary>
        /// Event queue for actions upon cell digit clear.
        /// </summary>
        /// <remarks>A CellControl's BoardControl adds a handler to this queue.</remarks>
        public event ClearEventHandler CellCleared;

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
                //if unsetting a cell    
                if (sLabel == setLbl)
                {
                    CellCleared(this.index);
                }
                //else if setting a cell
                else
                {
                    int digit;
                    digit = (int)sLabel.Tag;

                    // TODO:
                    Console.WriteLine("Index: " + index + ", Digit: " + digit);

                    if (CellSet != null)
                        CellSet(this.index, digit);
                }
            }
        }
        
        // TODO: Selecting a big digit will remove it and redisplay the appropriate candidates in its context. 

        #endregion 

        #region Observer cell handlers

        /// <summary>
        /// Updates UI to hide candidates and show selected digit.
        /// </summary>
        /// <param name="value">Digit that has been selected for that cell</param>
        public void UpdateDigit( int value ) 
        {
#if DEBUG
            Console.WriteLine("Set " + index + ": " + value);
#endif

            foreach (Label l in candidateLabels)
            {
                l.Visible = false;
            }

            setLbl.Text = value.ToString();
            setLbl.Tag = value;
            setLbl.Visible = true;

        }

        /// <summary>
        /// Updates UI to hide any selected digit and show candidate list per candidates parameter.
        /// </summary>
        /// <param name="candidates">List of possible candidates, where index 0 true represents 1 as a candidate</param>
        public void UpdateCandidates( BitArray candidates )
        {
            if (candidates.Length != candidateLabels.Length)
            {
                throw new ArgumentException("Invalid candidates length.");
            }
            
            //if cell currently set, clear it
            setLbl.Visible = false;
#if DEBUG

            StringBuilder sb = new StringBuilder("Possible " + index + ": ");
#endif

            for (int i = 0; i < candidates.Length; ++i)
            {
                if (candidates[i])
                {
                    candidateLabels[i].Visible = true;
#if DEBUG
                    sb.Append(i + 1 + " ");
#endif
                }
                else
                {
                    candidateLabels[i].Visible = false;
                }
            }
#if DEBUG
            Console.WriteLine(sb);
#endif
        }

        #endregion
    }
}

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
    public delegate void SetEventHandler(int cellIndex, int digit);
    public delegate void ClearEventHandler(int cellIndex);

    /// <summary>
    /// A Cell which contains either one or nine labels with large or small digits. 
    /// Geometry based on the construction parameter: the size of one of the small, square labels. 
    /// </summary>
    public partial class CellControl : UserControl
    {
        private readonly int labelSize;

        private readonly Label[] candidateLabels = new Label[9];
        private readonly Label setLbl;

        private readonly int index;

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
            
            setLbl = new Label();
            setLbl.AutoSize = false;
            setLbl.Location = new System.Drawing.Point(0, 0);
            setLbl.Margin = new System.Windows.Forms.Padding(0);
            setLbl.Name = "set";
            setLbl.Size = new System.Drawing.Size(Width, Height);
            setLbl.Text = "";
            setLbl.Font = new Font(setLbl.Font.Name, setLbl.Font.SizeInPoints + 4);
            setLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            setLbl.MouseClick += new System.Windows.Forms.MouseEventHandler(candidate_MouseClick);
            setLbl.Visible = false;
            this.Controls.Add(setLbl);

            
        }

        #region Event Management

        public event SetEventHandler CellSet;
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

        public void UpdateCandidates(BitArray candidates)
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IObserver = Axel.Sudoku.IObserver;

namespace TerryAndMike.Sudoku.GUI
{
    /// <summary>
    /// A Board which contains 9x9 Cell objects with colors indicating the shapes. 
    /// </summary>
    public partial class BoardControl : UserControl, IObserver
    {        
        private int labelSize;

        private readonly CellControl[] cells;

        private readonly Color[] shapeColors;

        /// <summary>
        /// Creates a new instance of Board.
        /// </summary>
        internal BoardControl(int dimension, int[] shapes, int labelSize)
        {
            InitializeComponent();

            shapeColors = new Color[dimension];
            for (int i = 0; i < shapeColors.Length; ++i)
            {
                int r = (int)Math.Pow(3, i + 1) % 255;
                int g = (int)Math.Pow(3, i + 3) % 255;
                int b = (int)Math.Pow(3, i + 5) % 255;

                if (r < 128)
                    r += 128;
                if (g < 128)
                    g += 128;
                if (b < 128)
                    b += 128;

                shapeColors[i] = Color.FromArgb(r, g, b);
            }

            this.labelSize = labelSize;

            cells = new CellControl[dimension * dimension];

            //Create cells based on the size of one of the small, square labels. 
            for (int i = 0; i < cells.Length; ++i)
            {
                CellControl c = new CellControl(this.labelSize, dimension, i);
                c.Location = new System.Drawing.Point((i % dimension) * c.Width, (i / dimension) * c.Height);
                c.BackColor = shapeColors[shapes[i] - 1];
                c.BorderStyle = BorderStyle.Fixed3D;
                c.CellSet += new SetEventHandler(c_CellSet);
                c.CellCleared += new ClearEventHandler(c_CellCleared);
                this.Controls.Add(c);
                this.cells[i] = c;
            }

            this.Width = this.cells[this.cells.Length - 1].Bounds.Right;
            this.Height = this.cells[this.cells.Length - 1].Bounds.Bottom;
        }

        #region IObserver implementation

        /// <summary> a digit is entered into a cell. </summary>
        public void Set(int cell, int digit) {
            throw new NotImplementedException();
        }

        /// <summary> new list of candidates for a cell. </summary>
        public void Possible(int cell, System.Collections.BitArray digits) {
            throw new NotImplementedException();
        }

        #endregion


        #region Event Management

        public event SetEventHandler BoardCellSet;
        public event ClearEventHandler BoardCellCleared;

        private void c_CellSet(int cellIndex, int digit)
        {
            if (BoardCellSet != null)
                BoardCellSet(cellIndex, digit);
        }

        private void c_CellCleared(int cellIndex)
        {
            if (BoardCellCleared != null)
                BoardCellCleared(cellIndex);
        }

        #endregion
    }
}

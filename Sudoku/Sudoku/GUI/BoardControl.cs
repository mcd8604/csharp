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
    /// A Board which contains 9x9 Cell objects with colors indicating the shapes. 
    /// </summary>
    public partial class BoardControl : UserControl
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
            for (int i = 1; i <= shapeColors.Length; ++i)
            {
                shapeColors[i - 1] = Color.FromArgb((255 * i) % 255, (255 - 255 * i) % 255, 255 / i);
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
                this.Controls.Add(c);
                this.cells[i] = c;
            }

            this.Width = this.cells[this.cells.Length - 1].Bounds.Right;
            this.Height = this.cells[this.cells.Length - 1].Bounds.Bottom;
        }
    }
}

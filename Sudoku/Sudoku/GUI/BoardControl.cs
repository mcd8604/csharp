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
        private Board board;
        
        private int labelSize;

        private CellControl[] cells = new CellControl[81];

        /// <summary>
        /// Creates a new instance of Board.
        /// </summary>
        internal BoardControl(Board board, int labelSize)
        {
            this.board = board;
            this.labelSize = labelSize;

            this.Width = labelSize * 3 * this.board.Dimension;
            this.Height = labelSize * 3 * this.board.Dimension;

            //Create cells based on the size of one of the small, square labels. 
            for (int row = 0; row < this.board.Dimension; ++row)
            {
                for (int column = 0; column < this.board.Dimension; ++column)
                {
                    CellControl c = new CellControl(this.labelSize);
                    c.Size = new System.Drawing.Size(labelSize, labelSize);
                    c.Location = new System.Drawing.Point(row * labelSize, column * labelSize);
                    this.Controls.Add(c);
                    this.cells[(row * this.board.Dimension) + column] = c;
                }
            }

            InitializeComponent();
        }
    }
}

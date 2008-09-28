using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TerryAndMike.Sudoku.GUI
{
    public partial class SudokuForm : Form
    {
        private BoardControl boardControl;

        /// <summary>
        /// Creates a new instance of SudokuForm
        /// </summary>
        internal SudokuForm(int dimension, int[] shapes)
        {
            InitializeComponent();
            InitializeBoard(dimension, shapes);
        }

        /// <summary>
        /// Determines the size of one of the small, square labels, based on the font to be used
        /// and then produces a fixed-size window containing a Board.
        /// </summary>
        /// <param name="board">The Board</param>
        private void InitializeBoard(int dimension, int[] shapes)
        {
            Label fontLabel = new Label();
            int labelSize = (int)fontLabel.Font.GetHeight();
            this.boardControl = new BoardControl(dimension, shapes, labelSize);
            this.Controls.Add(boardControl);
        }


    }
}

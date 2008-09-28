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
        private Font font;

        private BoardControl boardControl;

        /// <summary>
        /// Creates a new instance of SudokuForm
        /// </summary>
        internal SudokuForm(Board board)
        {
            InitializeComponent();
            InitializeBoard(board);
        }

        /// <summary>
        /// Determines the size of one of the small, square labels, based on the font to be used
        /// and then produces a fixed-size window containing a Board.
        /// </summary>
        /// <param name="board">The Board</param>
        private void InitializeBoard(Board board)
        {
            this.font = new Font("Arial", 8);
            //TODO: get correct label size, 8 is arbitrary
            int labelSize = 8;
            this.Width = 81 * labelSize;
            this.Height = 81 * labelSize;
            this.boardControl = new BoardControl(board, labelSize);
        }
    }
}

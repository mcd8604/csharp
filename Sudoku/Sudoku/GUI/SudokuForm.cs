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
            this.boardControl.CellSet += new SetEventHandler(boardControl_CellSet);
            this.boardControl.CellCleared += new ClearEventHandler(boardControl_CellCleared);
            this.Controls.Add(boardControl);
        }

        #region Event Management

        public event SetEventHandler CellSet;
        public event ClearEventHandler CellCleared;

        private void boardControl_CellSet(int cellIndex, int digit)
        {
            if (CellSet != null)
                CellSet(cellIndex, digit);
        }

        private void boardControl_CellCleared(int cellIndex)
        {
            if (CellCleared != null)
                CellCleared(cellIndex);
        }

        #endregion

    }
}

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
    /// <summary>
    /// A Windows Forms form containing a single BoardControl, interfacing between it and the controller.
    /// </summary>
    public partial class SudokuForm : Form
    {
        private BoardControl boardControl;

        /// <summary>
        /// A property to access the containing BoardControl, which will observe the model.
        /// </summary>
        public Axel.Sudoku.IObserver BoardObserver
        {
            get { return boardControl; }
        }

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
        /// <param name="dimension">Number of cells on a single dimension of the Sudoku board</param>
        /// <param name="shapes">Array of shapes (index origin 1), indexed by cellId (origin 0)</param>
        private void InitializeBoard(int dimension, int[] shapes)
        {
            Label fontLabel = new Label();
            int labelSize = fontLabel.Font.Height;
            this.boardControl = new BoardControl(dimension, shapes, labelSize);
            this.boardControl.BoardCellSet += new SetEventHandler(boardControl_CellSet);
            this.boardControl.BoardCellCleared += new ClearEventHandler(boardControl_CellCleared);
            this.Controls.Add(boardControl);
        }

        #region Event Management

        /// <summary>
        /// Event queue for actions upon board notification to form of digit selection.
        /// </summary>
        /// <remarks>A SudokuForm's controller adds a handler (callback to the model) to this queue.</remarks>
        public event SetEventHandler CellSet;

        /// <summary>
        /// Event queue for actions upon board notification to form of digit clear.
        /// </summary>
        /// <remarks>A SudokuForm's controller adds a handler (callback to the model) to this queue.</remarks>
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

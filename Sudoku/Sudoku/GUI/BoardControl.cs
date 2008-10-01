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
    /// A Board which contains (dimension)x(dimension) Cell objects with colors indicating the shapes. 
    /// </summary>
    public partial class BoardControl : UserControl, IObserver
    {        
        private int labelSize;

        private readonly CellControl[] cells;

        /// <summary>
        /// Creates a new instance of Board.
        /// </summary>
        internal BoardControl(int dimension, int[] shapes, int labelSize)
        {
            InitializeComponent();

            this.labelSize = labelSize;

            cells = new CellControl[dimension * dimension];

            //Create cells based on the size of one of the small, square labels. 
            for (int i = 0; i < cells.Length; ++i)
            {
                CellControl c = new CellControl(this.labelSize, dimension, i);

                c.Width = (int)Math.Sqrt(dimension) * labelSize;
                c.Height = (int)Math.Sqrt(dimension) * labelSize;
                c.Margin = new Padding(0);
                c.Padding = new Padding(0);
                c.Location = new System.Drawing.Point((i % dimension) * c.Width, (i / dimension) * c.Height);
                c.BackColor = GetShapeColor(shapes[i] - 1);
                c.BorderStyle = BorderStyle.FixedSingle;
                c.CellSet += new SetEventHandler(c_CellSet);
                c.CellCleared += new ClearEventHandler(c_CellCleared);
                this.Controls.Add(c);
                this.cells[i] = c;
            }

            this.Width = this.cells[this.cells.Length - 1].Bounds.Right;
            this.Height = this.cells[this.cells.Length - 1].Bounds.Bottom;
        }

        /// <summary>
        /// Return a Color representing that shape, provided the shape's index.
        /// </summary>
        /// <param name="shapeId">Integer shape index, [0,dimension-1]</param>
        private static Color GetShapeColor( int shapeId ) {
            int r = (int)Math.Pow( 3, shapeId + 1 ) % 255;
            int g = (int)Math.Pow( 3, shapeId + 3 ) % 255;
            int b = (int)Math.Pow( 3, shapeId + 5 ) % 255;

            if ( r < 128 )
                r += 128;
            if ( g < 128 )
                g += 128;
            if ( b < 128 )
                b += 128;

            return Color.FromArgb( r, g, b );
        }


        #region IObserver implementation

        /// <summary> a digit is entered into a cell. </summary>
        public void Set(int cellIdx, int digit) {
            cells[cellIdx].UpdateDigit(digit);
        }

        /// <summary> new list of candidates for a cell. </summary>
        public void Possible(int cellIdx, System.Collections.BitArray digits) {
            cells[cellIdx].UpdateCandidates(digits);
        }

        #endregion


        #region Event Management

        /// <summary>
        /// Event queue for actions upon cell notification to board of digit selection.
        /// </summary>
        /// <remarks>A BoardControl's SudokuForm adds a handler to this queue.</remarks>
        public event SetEventHandler BoardCellSet;

        /// <summary>
        /// Event queue for actions upon cell notification to board of digit clear.
        /// </summary>
        /// <remarks>A BoardControl's SudokuForm adds a handler to this queue.</remarks>
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

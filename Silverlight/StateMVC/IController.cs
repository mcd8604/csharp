using System;
using System.Collections.Generic;

namespace TerryAndMike.SilverlightGame.StateMVC
{
    /// <summary>
    /// Defines a change in state in the model, which will be sent along to the view.
    /// </summary>
    /// <param name="row">The row of the tile.</param>
    /// <param name="col">The column of the tile.</param>
    /// <param name="tile">The tile to set.</param>
    public delegate void StateToView(int row, int col, int tile);

    /// <summary>
    /// Defines an event representing change in state of the view, to be sent to the model.
    /// </summary>
    /// <param name="row">The row of the tile.</param>
    /// <param name="col">The column of the tile.</param>
    public delegate void StateToModel(int row, int col);

    /// <summary>
    /// Associates an IModel with multiple IViews
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Updates the state of the model.
        /// </summary>
        /// <param name="row">The row of the tile.</param>
        /// <param name="col">The column of the tile.</param>
        /// 
        void ShiftMakeBlank(int row, int col);

        /// <summary>
        /// Initializes the state of the Model
        /// </summary>
        void Reset( int row, int col );


    }
}

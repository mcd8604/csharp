using System;
using System.Collections.Generic;

namespace TerryAndMike.SilverlightGame.StateMVC
{
    /// <summary>
    /// Defines a change in state.
    /// </summary>
    /// <param name="row">The row of the tile.</param>
    /// <param name="col">The column of the tile.</param>
    /// <param name="tile">The tile to set.</param>
    public delegate void State(int row, int col, int tile);

    /// <summary>
    /// Associates an IModel with multiple IViews
    /// </summary>
    public interface IController
    {

        // <summary>
        // The Model owned by the controller
        // </summary>
        /*IModel Model
        {
            get;
            set;
        }*/

        /// <summary>
        /// Registers view as an observer of the controller's model.
        /// </summary>
        /// <param name="view">The IView to add.</param>
        void AddView(IView view);

        /// <summary>
        /// Unregisters view as an observer of the controller's model.
        /// </summary>
        /// <param name="view">The IView to remove.</param>
        void RemoveView(IView view);

        /// <summary>
        /// Updates the state of the model.
        /// </summary>
        /// <param name="row">The row of the tile.</param>
        /// <param name="col">The column of the tile.</param>
        /// 
        void ShiftMakeTileBlank( int row, int col );

        /// <summary>
        /// Initializes the state of the Model
        /// </summary>
        void Reset( int row, int col );


    }
}

using System;

namespace TerryAndMike.MVC
{
    /// <summary>
    /// Represents the state of an IModel to the user.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Updates the IView to represent the state of an IModel.
        /// </summary>
        /// <param name="row">The row of the tile.</param>
        /// <param name="col">The column of the tile.</param>
        /// <param name="tile">The tile that was set.</param>
        void StateUpdated(int row, int col, int tile);
    }
}

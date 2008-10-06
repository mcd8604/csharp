using System;

namespace TerryAndMike.MVC
{
    /// <summary>
    /// Maintains state of tiles in context of rows and columns.
    /// Notifies View observers of changes in State. 
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Updates the state of the model; sets the row and column for the given tile.
        /// </summary>
        /// <param name="row">The row of the tile.</param>
        /// <param name="col">The column of the tile.</param>
        /// <param name="tile">The tile to set.</param>
        void SetState(int row, int col, int tile);

        /// <summary>
        /// Initializes the state of the Model
        /// </summary>
        void Reset();

        /// <summary>
        /// Adds a view as an observer.
        /// </summary>
        /// <param name="view">The IView to add.</param>
        void AddView(IView view);

        /// <summary>
        /// Removes a view as an observer.
        /// </summary>
        /// <param name="view">Thew IView to remove.</param>
        void RemoveView(IView view);
    }
}

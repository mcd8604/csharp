using System;

namespace TerryAndMike.SilverlightGame.StateMVC
{
    /// <summary>
    /// Maintains state of elements in context of rows and columns and
    /// notifies observers of changes in State. 
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Notifies the model of action on this element
        /// </summary>
        /// <param name="row">The row number in the board.</param>
        /// <param name="col">The column number in the board.</param>
        void NotifyStateChange(int row, int col);

        /// <summary>
        /// Initializes the state of the Model
        /// </summary>
        /// <param name="rows">The number of rows in the board.</param>
        /// <param name="cols">The number of cols in the board.</param>
        void Reset(int rows, int cols);

        /// <summary>
        /// Registers a view as an observer.
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

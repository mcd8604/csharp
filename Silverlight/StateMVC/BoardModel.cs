using System;
using System.Collections.Generic;

namespace TerryAndMike.SilverlightGame.StateMVC {
    /// <summary>
    /// Abstract class, implements common functionality for a general board's model
    /// </summary>
    public abstract class BoardModel : IModel {

        /// <summary>
        /// Queue of IView observers to notify of tile changes
        /// </summary>
        protected StateToView observers;

        /// <summary>
        /// Queue of IView observers to notify of visibility changes
        /// </summary>
        protected StateToViewVisible visibleObservers;

        /// <summary>
        /// A matrix representation of the board images.
        /// </summary>
        protected int[ , ] board;

        /// <summary>
        /// Number of rows and columns in the board
        /// </summary>
        protected int rows, cols;


        #region IModel Members

        /// <summary>
        /// Notifies the model of action on this element
        /// </summary>
        /// <param name="row">The row number in the board.</param>
        /// <param name="col">The column number in the board.</param>
        public abstract void NotifyStateChange( int row, int col );

        /// <summary>
        /// Initializes the state of the BoardModel, calls ValidateBoardSize(),
        /// InitializeBoardValues(), SendFullStateToObservers().
        /// </summary>
        /// <param name="rows">The number of rows in the board.</param>
        /// <param name="cols">The number of cols in the board.</param>
        public void Reset( int rows, int cols ) {
            /**** Quick validation ****/
            if (!ValidateBoardSize(rows, cols))
                return;

            board = new int[ rows, cols ];
            this.rows = rows;
            this.cols = cols;

            InitializeBoardValues();

            /**** Notify all observers of full state ****/
            SendFullStateToObservers();
        }

        /// <summary>
        /// Registers an observer to the model.
        /// </summary>
        /// <param name="view"></param>
        public void AddView( IView view ) {
            observers += view.StateUpdated;
            visibleObservers += view.StateVisibilityUpdated;
        }

        /// <summary>
        /// Removes an observer from the model.
        /// </summary>
        /// <param name="view"></param>
        public void RemoveView( IView view ) {
            observers -= view.StateUpdated;
            visibleObservers -= view.StateVisibilityUpdated;
        }

        #endregion

        /// <summary>
        /// Updates all viewers with messages about the state of all board cells.
        /// </summary>
        protected virtual void SendFullStateToObservers() {
            if ( observers == null )
                return;

            for ( int r = 0; r < rows; ++r ) {
                for ( int c = 0; c < cols; ++c ) {
                    observers( r, c, board[ r, c ] );
                }
            }
        }
        /// <summary>
        /// Returns whether the board size parameters provided are valid
        /// </summary>
        /// <param name="rows">Number of rows to create</param>
        /// <param name="cols">Number of cols to create</param>
        /// <returns>True if valid parameters supplied</returns>
        protected virtual bool ValidateBoardSize(int rows, int cols) {
            return (rows >= 0 && cols >= 0);
        }

        /// <summary>
        /// Do board type-specific initialization.
        /// </summary>
        protected abstract void InitializeBoardValues();
    }
}

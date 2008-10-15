using System;
using System.Collections.Generic;

namespace TerryAndMike.SilverlightGame.StateMVC {
    public abstract class BoardModel : IModel {

        //data members
        protected StateToView observers;
        protected StateToViewVisible visibleObservers;

        protected int[ , ] board;
        protected int rows, cols;


        #region IModel Members

        public abstract void NotifyStateChange( int row, int col );

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

        protected virtual bool ValidateBoardSize(int rows, int cols) {
            return (rows >= 0 && cols >= 0);
        }

        protected abstract void InitializeBoardValues();
    }
}

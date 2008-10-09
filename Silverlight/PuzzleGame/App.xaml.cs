﻿using System;
using System.Windows;
using IController = TerryAndMike.SilverlightGame.StateMVC.IController;
using IView = TerryAndMike.SilverlightGame.StateMVC.IView;
using IModel = TerryAndMike.SilverlightGame.StateMVC.IModel;

namespace TerryAndMike.SilverlightGame.PuzzleGame
{
    /// <summary>
    /// Starts up the view and acts as the controller.
    /// </summary>
    public partial class App : Application, IController
    {

#warning Temporary Implementation of IModel
        
        private class PuzzleModel : IModel
        {

            #region IModel Members

            public void SetState(int row, int col, int tile)
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
            }

            public void AddView(IView view)
            {
                throw new NotImplementedException();
            }

            public void RemoveView(IView view)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        /// <summary>
        /// The puzzle model
        /// </summary>
        private IModel model;

        /// <summary>
        /// Creates a new instance of App
        /// </summary>
        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            model = new PuzzleModel();

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {            
            this.RootVisual = new Page(this);
            model.AddView(this.RootVisual as IView);
        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight 2 Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }

        #region IController Members

        /// <summary>
        /// Registers view as an observer of the controller's model.
        /// </summary>
        /// <param name="view">The IView to add.</param>
        public void AddView(IView view)
        {
            model.AddView(view);
        }

        /// <summary>
        /// Unregisters view as an observer of the controller's model.
        /// </summary>
        /// <param name="view">The IView to remove.</param>
        public void RemoveView(IView view)
        {
            model.RemoveView(view);
        }

        /// <summary>
        /// Updates the state of the model.
        /// </summary>
        /// <param name="row">The row of the tile.</param>
        /// <param name="col">The column of the tile.</param>
        /// <param name="tile">The tile to set.</param>
        public void SetState(int row, int col, int tile)
        {
            model.SetState(row, col, tile);
        }

        /// <summary>
        /// Initializes the state of the Model
        /// </summary>
        public void Reset()
        {
            model.Reset();
        }

        #endregion
    }
}
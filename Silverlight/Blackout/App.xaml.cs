using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using IController = TerryAndMike.SilverlightGame.StateMVC.IController;
using IModel = TerryAndMike.SilverlightGame.StateMVC.IModel;
using State = TerryAndMike.SilverlightGame.StateMVC.StateToModel;
using BlackoutModel = TerryAndMike.SilverlightGame.StateMVC.BlackoutModel;

namespace Blackout
{
    public partial class App : Application, IController
    {
        private IModel model;

        public App()
        {
            model = new BlackoutModel();

            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Page p = new Page();
            p.Reset += new State(Reset);
            p.TileClicked += new State(NotifyStateChange);
            model.AddView(p);
            this.RootVisual = p;
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
        /// Updates the state of the model.
        /// </summary>
        /// <param name="row">The row of the tile.</param>
        /// <param name="col">The column of the tile.</param>
        public void NotifyStateChange(int row, int col)
        {
            if (model != null) model.NotifyStateChange(row, col);
        }

        /// <summary>
        /// Initializes the state of the Model
        /// </summary>
        /// <param name="row">The row of the tile.</param>
        /// <param name="col">The column of the tile.</param>
        public void Reset(int row, int col)
        {
            if(model != null) model.Reset(row, col);
        }

        #endregion
    }
}

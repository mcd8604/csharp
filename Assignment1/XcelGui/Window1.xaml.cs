using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TerryAndMike.XcelGui
{
    /// <summary>
    /// Interaction logic for XcelWindow.xaml
    /// </summary>
    public partial class XcelWindow : Window
    {
        private static List<XcelWindow> windows = new List<XcelWindow>();

        public XcelWindow()
        {
            InitializeComponent();
            LoadXcelCommands();
            windows.Add(this);
            dataTextBox.Focus();
        }

        private void LoadXcelCommands()
        {
            foreach (string cmd in Xcel.XcelCommandFactory.GetCommandNames())
                //specifically disallow 'up' and 'down' calculations
                if ( cmd != "up" && cmd != "down" )
                    commandComboBox.Items.Add(cmd);

            commandComboBox.SelectedIndex = 0;
        }

        private void Calculate() {
            string[] stringArgs = dataTextBox.Text.Split(new char[]{' '});
            List<int> args = new List<int>(stringArgs.Length);
            
            foreach ( string sarg in stringArgs ) {
                if (sarg == string.Empty)
                    continue;

                int iarg;
                if (!int.TryParse(sarg, out iarg))
                {
                    MessageBox.Show("Invalid Input, Enter only integer values");
                    return;
                }
                args.Add(iarg);
            }

            if (args.Count > 0)
            {
                Xcel.XcelCommand cmd = Xcel.XcelCommandFactory.GetCommand(commandComboBox.SelectedItem.ToString(), args.ToArray());
                cmd.Args = args.ToArray();
                cmd.Execute();
                foreach (XcelWindow w in windows)
                    w.outputTextBox.Text = cmd.ToString();
            }
        }

        #region Event Handlers 
        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
        }

        private void dataTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (XcelWindow w in windows)
            {
                if (w != this)
                {
                    w.dataTextBox.Text = dataTextBox.Text;
                }
            }

            if (commandComboBox != null)
                Calculate();
        }

        private void commandComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //update each window's dropdown to change selection
            foreach (XcelWindow w in windows)
            {
                w.commandComboBox.SelectedIndex = commandComboBox.SelectedIndex;
            }
            Calculate();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                switch (menuItem.Header.ToString())
                {
                    case "New":
                        XcelWindow xw = new XcelWindow();
                        xw.dataTextBox.Text = dataTextBox.Text;
                        xw.outputTextBox.Text = outputTextBox.Text;
                        xw.commandComboBox.SelectedIndex = commandComboBox.SelectedIndex;
                        xw.Show();
                        break;
                    case "Exit":
                        Environment.Exit(0);
                        break;
                }
            }
        }
        #endregion
    }
}

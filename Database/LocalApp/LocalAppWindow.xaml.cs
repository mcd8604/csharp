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
using IModel = Axel.Database.IModel;
using LocalDB = TerryAndMike.Database.LocalDB;

namespace TerryAndMike.Database.LocalApp
{
    /// <summary>
    /// Interaction logic for LocalAppWindow.xaml
    /// </summary>
    public partial class LocalAppWindow : Window
    {
        private IModel model;

        public LocalAppWindow()
        {
            model = new LocalDB();
            InitializeComponent();
        }

        /// <summary>
        /// Search the model using first line of tuple as keys
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            // Search the model on keys

            string[] keys = GetTuple();
            string[][] tuples = model.Search(keys);

            // Display the array elements

            StringBuilder nameBuilder = new StringBuilder();
            StringBuilder phoneBuilder = new StringBuilder();
            StringBuilder roomBuilder = new StringBuilder();
                        
            for (int i = 0; i < tuples.Length; i++)
            {
                nameBuilder.AppendLine(tuples[i][0]);
                phoneBuilder.AppendLine(tuples[i][1]);
                roomBuilder.AppendLine(tuples[i][2]);
            }

            nameTextBox.Text = nameBuilder.ToString();
            phoneTextBox.Text = phoneBuilder.ToString();
            roomTextBox.Text = roomBuilder.ToString();
        }

        private void enterBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] tuple = GetTuple();
            /*bool result = */model.Enter(tuple);
            countLabel.Content = model.Count;
        }

        private void removeBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] keys = GetTuple();
            /*bool result = */model.Remove(keys);
            countLabel.Content = model.Count;

            //clear output text boxes
            nameTextBox.Clear();
            phoneTextBox.Clear();
            roomTextBox.Clear();
        }

        /// <summary>
        /// Form 3-tuple from the first line of each of the textboxs, replacing empty strings with null
        /// </summary>
        /// <returns>String array (tuple) with null representing empty textboxes</returns>
        private string[] GetTuple() {
            return new string[] { nameTextBox.Text.Length > 0 ? nameTextBox.GetLineText(0) : null, 
                                phoneTextBox.Text.Length > 0 ? phoneTextBox.GetLineText(0) : null, 
                                roomTextBox.Text.Length > 0 ? roomTextBox.GetLineText(0) : null };
        }
    }
}

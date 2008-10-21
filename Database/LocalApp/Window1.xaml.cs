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

namespace LocalApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private IModel model;

        public Window1()
        {
            model = new LocalDB();
            InitializeComponent();
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            // Search the model on keys

            string[] keys = { nameTextBox.Text, phoneTextBox.Text, roomTextBox.Text };
            string[][] tuples = model.Search(keys);

            // Display the array elements

            StringBuilder nameBuilder = new StringBuilder();
            StringBuilder phoneBuilder = new StringBuilder();
            StringBuilder roomBuilder = new StringBuilder();
                        
            for (int i = 0; i < tuples.Length; i++)
            {
                nameBuilder.Append(tuples[i][0]);
                phoneBuilder.Append(tuples[i][1]);
                roomBuilder.Append(tuples[i][2]);
            }

            nameTextBox.Text = nameBuilder.ToString();
            phoneTextBox.Text = phoneBuilder.ToString();
            roomTextBox.Text = roomBuilder.ToString();
        }

        private void enterBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] tuple = { nameTextBox.Text, phoneTextBox.Text, roomTextBox.Text };
            model.Enter(tuple);

            countLabel.Content = model.Count;
        }

        private void removeBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] keys = { nameTextBox.Text, phoneTextBox.Text, roomTextBox.Text };
            model.Remove(keys);

            countLabel.Content = model.Count;
        }
    }
}

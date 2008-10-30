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
using Axel.Database;
using TerryAndMike.Database;

namespace app2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        /// <summary>Creates a new instance of Window1</summary>
        /// <remarks>Creates a controller which connects to this.</remarks>
        public Window1() {
            InitializeComponent();

            //Init remoteDB
            RemoteDB remoteDB = new RemoteDB();
            remoteDB.ConnectionEndpointError += (sender, connErrorArgs) => { MessageBox.Show(connErrorArgs.ErrorMsg, "Connection Error!"); };
            remoteDB.Connect();

            var controller = new ToggleController(new LocalDB(), remoteDB, new WorkQueue(),
            new Enable(isEnabled => {
              Toggle.IsEnabled = Search.IsEnabled = Enter.IsEnabled = Remove.IsEnabled = isEnabled; }),
            null,
            new Access(() => Size.Text, s => { Size.Text = s; }),
            new Access(() => Names.Text, s => { Names.Text = s; }),
            new Access(() => Phones.Text, s => { Phones.Text = s; }),
            new Access(() => Rooms.Text, s => { Rooms.Text = s; }));
            controller.doSize(null, null);
            Search.Click += new RoutedEventHandler(controller.doSearch);
            Enter.Click += new RoutedEventHandler(controller.doEnter);
            Remove.Click += new RoutedEventHandler(controller.doRemove);
            Size.MouseDown += new MouseButtonEventHandler(controller.doSize);
            Toggle.Click += new RoutedEventHandler(controller.doToggle);
            controller.DatabaseToggled += (database) => { dbLabel.Header = database == Databases.ONE ? "Local" : "Remote"; };
            dbLabel.Header = "Local";
        }
    }
}

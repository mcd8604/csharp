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
using State = TerryAndMike.SilverlightGame.StateMVC.StateToModel;

namespace Memory
{
    public partial class ClipTable : UserControl
    {
        public ClipTable()
        {
            InitializeComponent();
        }

        public void SetClip(int row, int col, int tile) {

        }

        #region Event Management

        public event State TileClicked;

        #endregion

        internal void Initialize(int row, int col, double width, double height)
        {
            throw new NotImplementedException();
        }
    }
}

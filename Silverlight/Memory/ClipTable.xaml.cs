using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using State = TerryAndMike.SilverlightGame.StateMVC.StateToModel;
using System.Text;

namespace Memory
{
    public partial class ClipTable : UserControl
    {
        private struct Tile
        {
            public int row;
            public int col;
            public int tileImg;
        }

        public const int NUM_IMAGES = 21;
        private BitmapImage[] images;

        private Canvas[,] clips;

        public ClipTable()
        {
            InitializeComponent();
            LoadImages();
        }

        /// <summary>
        /// Loads the images to be displayed on clips
        /// </summary>
        private void LoadImages()
        {
            images = new BitmapImage[NUM_IMAGES];

            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < NUM_IMAGES; ++i)
            {
                sb.Remove(0, sb.Length);
                sb.Append(@"images/");
                if(i < 10) {
                    sb.Append("0");
                }
                sb.Append(i.ToString());
                sb.Append(".jpg");
                images[i] = new BitmapImage(new System.Uri(sb.ToString(), System.UriKind.Relative));
            }
        }

        /// <summary>
        /// Initializes the ClipTable. 
        /// </summary>
        /// <param name="rows">Number of rows of clips.</param>
        /// <param name="cols">Number of columns of clips.</param>
        /// <param name="width">The width of the entire table of clips in pixels.</param>
        /// <param name="height">The height of the entire table of clips in pixels.</param>
        internal void Initialize(int rows, int cols, double width, double height)
        {
            double clipWidth = width / cols;
            double clipHeight = height / rows;

            this.clips = new Canvas[rows, cols];

            this.LayoutRoot.Children.Clear();

            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < cols; ++col)
                {
                    Canvas canvas = new Canvas();
                    canvas.Width = width;
                    canvas.Height = height;
                    canvas.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(canvas_MouseLeftButtonDown);

                    Rect rect = new Rect(col * clipWidth, row * clipHeight, clipWidth, clipHeight);

                    RectangleGeometry rectGeom = new RectangleGeometry();
                    rectGeom.SetValue(RectangleGeometry.RectProperty, rect);
                    canvas.Clip = rectGeom;

                    ImageBrush b = new ImageBrush();
                    b.ImageSource = images[0];
                    canvas.Background = b;

                    Tile t = new Tile();
                    t.row = row;
                    t.col = col;
                    t.tileImg = 0;
                    canvas.Tag = t;

                    this.clips[row, col] = canvas;

                    this.LayoutRoot.Children.Add(canvas);
                }
            }
        }

        /// <summary>
        /// Sets the image for a tile clip
        /// </summary>
        /// <param name="row">The row of the clip</param>
        /// <param name="col">The column of the clip</param>
        /// <param name="tile">The image index</param>
        public void SetClip(int row, int col, int tile)
        {
            Canvas canvas = this.clips[row, col];
            ImageBrush b = new ImageBrush();
            
            if(b != null) {

                b.ImageSource = images[tile];

                Tile t = (Tile)canvas.Tag;
                canvas.Background = b;
                
                t.tileImg = tile;
                canvas.Tag = t;

            }
        }
        
        /// <summary>
        /// Sets a clip's visibility
        /// </summary>
        /// <param name="row">The row of the clip</param>
        /// <param name="col">The column of the clip</param>
        /// <param name="visible">Whether the tile is visible or not</param>
        internal void SetClipVisible(int row, int col, bool visible)
        {
            Canvas canvas = this.clips[row, col];
            ImageBrush b = new ImageBrush();

            if (b != null)
            {
                Tile t = (Tile)canvas.Tag;

                if (visible)
                {
                    b.ImageSource = images[t.tileImg];
                }
                else
                {
                    b.ImageSource = images[0];
                }

                canvas.Background = b;
            }            
        }

        #region Event Management

        /// <summary>
        /// Occurs when a clip is clicked
        /// </summary>
        public event State ClipClicked;

        void canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            if (canvas != null && ClipClicked != null)
            {
                Tile t = (Tile)canvas.Tag;
                ClipClicked(t.row, t.col);
            }
        }

        #endregion
    }
}

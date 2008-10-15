using System.Windows.Controls;
using IView = TerryAndMike.SilverlightGame.StateMVC.IView;
using IModel = TerryAndMike.SilverlightGame.StateMVC.IModel;
using State = TerryAndMike.SilverlightGame.StateMVC.StateToModel;
using BitmapImage = System.Windows.Media.Imaging.BitmapImage;
using RectangleGeometry = System.Windows.Media.RectangleGeometry;
using System.Windows.Media;
using System.Windows;
using System.Collections.Generic;

namespace Blackout
{
    /// <summary>
    /// Maintains a table of clips from an image.
    /// </summary>
    public partial class ClipTable : UserControl
    {
        private struct Tile
        {
            public int row;
            public int col;
            public int tileImg;
        }

        private Canvas[,] clips;

        private BitmapImage img1;
        private BitmapImage img2;

        /// <summary>
        /// Creates a new instance of ClipTable
        /// </summary>
        public ClipTable()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the ClipTable. 
        /// </summary>
        /// <param name="image1Uri">Where the first BitmapImage is found.</param>
        /// <param name="image2Uri">Where the second BitmapImage is found.</param>
        /// <param name="rows">Number of rows of clips.</param>
        /// <param name="cols">Number of columns of clips.</param>
        /// <param name="width">The width of the entire table of clips in pixels.</param>
        /// <param name="height">The height of the entire table of clips in pixels.</param>
        public void Initialize(string image1Uri, string image2Uri, int rows, int cols, double width, double height)
        {
            double clipWidth = width / cols;
            double clipHeight = height / rows;

            this.clips = new Canvas[rows, cols];

            this.LayoutRoot.Children.Clear();

            img1 = new BitmapImage(new System.Uri(image1Uri, System.UriKind.Relative));
            img2 = new BitmapImage(new System.Uri(image2Uri, System.UriKind.Relative));

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
                    b.ImageSource = img1;

                    TranslateTransform tt = new TranslateTransform();
                    tt.X = 0 - col * clipWidth;
                    tt.Y = 0 - row * clipHeight;
                    b.Transform = tt;
                    canvas.Background = b;

                    //canvas.Visibility = Visibility.Collapsed;

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
        /// Occurs when a tile is clicked.
        /// </summary>
        public event State TileClicked;

        void canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            if (canvas != null && TileClicked != null)
            {
                Tile t = (Tile)canvas.Tag;
                TileClicked(t.row, t.col);
                SetClip(t.row, t.col, (t.tileImg + 1) % 2);
            }
        }

        /// <summary>
        /// Sets a tile's position and visibility.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="col">The column index.</param>
        /// <param name="tile">The tile index.</param>
        public void SetClip(int row, int col, int tile)
        {
            Canvas canvas = this.clips[row, col];
            ImageBrush b = canvas.Background as ImageBrush;
            if(b != null) {
                if (tile == 0)
                {
                    b.ImageSource = img1;
                }
                else if (tile == 1)
                {
                    b.ImageSource = img2;
                }
                Tile t = (Tile)canvas.Tag;
                t.tileImg = tile;
                canvas.Tag = t;
            }
        }
    }
}

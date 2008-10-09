using System.Windows.Controls;
using IView = TerryAndMike.SilverlightGame.StateMVC.IView;
using IModel = TerryAndMike.SilverlightGame.StateMVC.IModel;
using State = TerryAndMike.SilverlightGame.StateMVC.State2;
using BitmapImage = System.Windows.Media.Imaging.BitmapImage;
using RectangleGeometry = System.Windows.Media.RectangleGeometry;
using System.Windows.Media;
using System.Windows;
using System.Collections.Generic;

namespace PuzzleGame2
{
    /// <summary>
    /// Maintains a table of clips from an image.
    /// </summary>
    public partial class ClipTable : UserControl
    {
        private struct Tile
        {
            public Tile(int row, int col, int tile)
            {
                this.row = row;
                this.col = col;
                this.tile = tile;
            }

            public int row;
            public int col;
            public int tile;
        }

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
        /// <param name="imageUri">Where the BitmapImage is found.</param>
        /// <param name="rows">Number of rows of clips.</param>
        /// <param name="cols">Number of columns of clips.</param>
        /// <param name="width">The width of the entire table of clips in pixels.</param>
        /// <param name="height">The height of the entire table of clips in pixels.</param>
        public void Initialize(string imageUri, int rows, int cols, double width, double height)
        {
            this.Width = width;
            this.Height = height;
            double clipWidth = width / cols;
            double clipHeight = height / rows;

            this.LayoutRoot.Children.Clear();

            BitmapImage img = new BitmapImage(new System.Uri(imageUri, System.UriKind.Relative));

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
                    b.ImageSource = img;

                    TranslateTransform tt = new TranslateTransform();
                    tt.X = col * clipWidth;
                    tt.Y = row * clipHeight;
                    b.Transform = tt;
                    canvas.Background = b;

                    canvas.Visibility = Visibility.Collapsed;

                    canvas.Tag = new Tile(row, col, this.LayoutRoot.Children.Count);

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
                Tile rcp = (Tile)canvas.Tag;
                TileClicked(rcp.row, rcp.col);
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
            if (tile > 0)
            {
                Canvas canvas = this.LayoutRoot.Children[tile - 1] as Canvas;
                if(canvas != null) {
                    Rect rect = new Rect(col * canvas.Width, row * canvas.Height, canvas.Width, canvas.Height);

                    RectangleGeometry rectGeom = new RectangleGeometry();
                    rectGeom.SetValue(RectangleGeometry.RectProperty, rect);
                    this.LayoutRoot.Children[tile - 1].Clip = rectGeom;

                    this.LayoutRoot.Children[tile - 1].Visibility = Visibility.Visible;
                }
            }
        }
    }
}

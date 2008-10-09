using System.Windows.Controls;
using IView = TerryAndMike.SilverlightGame.StateMVC.IView;
using IModel = TerryAndMike.SilverlightGame.StateMVC.IModel;
using BitmapImage = System.Windows.Media.Imaging.BitmapImage;
using RectangleGeometry = System.Windows.Media.RectangleGeometry;
using System.Windows.Media;

namespace PuzzleGame2
{
    public partial class ClipTable : UserControl, IView
    {
        public ClipTable()
        {
            InitializeComponent();
        }

        public void Initialize(IModel puzzle, string imageUri, int rows, int cols, double width, double height)
        {
            this.Width = width;
            this.Height = height;
            double clipWidth = width / cols;
            double clipHeight = height / rows;

            BitmapImage img = new BitmapImage(new System.Uri(imageUri));

            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < cols; ++col)
                {
                    Canvas canvas = new Canvas();
                    /*canvas.Clip.Transform 

                    ImageBrush b = new ImageBrush();
                    b.ImageSource = img;
                    TranslateTransform tt = new TranslateTransform();
                    tt.X = 
                    canvas.Clip.Transform = ;*/
                }
            }
        }

        #region IView Members

        public void StateUpdated(int row, int col, int tile)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}

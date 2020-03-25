using System;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;

namespace PZ1_PR132_2016.Models
{
    class MyImage : MyShape
    {
        private Coordinates coordinates;
        private int width;
        private int height;
        private string path;

        public int Height { get => height; set => height = value; }
        public int Width { get => width; set => width = value; }
        public Coordinates Coordinates { get => coordinates; set => coordinates = value; }
        public string Path { get => path; set => path = value; }
        public MyImage(double x, double y, int width, int height, string path)
        {
            Path = path;   
            coordinates = new Coordinates(x, y);
            Width = width;
            Height = height;
        }


        public override Shape Draw()
        {
            if (Shape != null)
                return Shape;

            System.Windows.Shapes.Rectangle retVal = new System.Windows.Shapes.Rectangle();
            retVal.Margin = new System.Windows.Thickness(coordinates.X_coordinate, coordinates.Y_coordinate, 0, 0);
            retVal.Height = Height;
            retVal.Width = Width;

            retVal.Stroke = new SolidColorBrush();
            retVal.StrokeThickness = 0;
            retVal.Fill= new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(path, UriKind.Absolute))
            };
            retVal.MouseLeftButtonDown += RetVal_MouseLeftButtonDown;
            Shape = retVal;
            return Shape;


          
        }

        private void RetVal_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChangePropertiesWindow changePropertiesWindow = new ChangePropertiesWindow(this, MyShapeEnum.Image);
            changePropertiesWindow.ShowDialog();

        }
        public void UpdateShape( string path)
        {
            if (Shape == null)
                return;

            Shape.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(path, UriKind.Absolute))
            };

            Path = path;

        }
    }
}

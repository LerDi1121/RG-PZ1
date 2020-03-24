using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PZ1_PR132_2016.Models
{
    class MyRectangle : MyShape
    {
        private Coordinates coordinates;
        private int width;
        private int height;
        public MyRectangle(double x, double y, int width, int height,System.Windows.Media.Brush fillColor, System.Windows.Media.Brush borderColor, int borderThickness) : base(fillColor, borderColor, borderThickness)
        {
            coordinates = new Coordinates(x, y);
            Width = width;
            Height = height;
        }

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public override Shape Draw()
        {
            System.Windows.Shapes.Rectangle rectangle = new System.Windows.Shapes.Rectangle();
            rectangle.Height = Height;
            rectangle.Width = Width;
            rectangle.Fill = FilCollor;
            rectangle.Stroke = BorderColor;
            rectangle.StrokeThickness = (double)BorderThickness;
            rectangle.Margin = new System.Windows.Thickness(coordinates.X_coordinate, coordinates.Y_coordinate, 0, 0);
            rectangle.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            return rectangle;
        }
        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

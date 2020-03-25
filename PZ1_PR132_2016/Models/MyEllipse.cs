using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PZ1_PR132_2016.Models
{
    internal class MyEllipse : MyShape
    {
        private Coordinates coordinates;
        private int width;
        private int height;
        
        public MyEllipse( double x, double y, int width, int height, Brush fillColor, Brush borderColor, int borderThickness) : base(fillColor,borderColor,borderThickness)
        {
            coordinates = new Coordinates(x, y);
            Width = width;
            Height = height;
        }

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
       
        public override  Shape Draw()
        {
            if (Shape != null)
                return Shape;
            Ellipse retVal = new Ellipse();
            retVal.Height = Height;
            retVal.Width = Width;
            retVal.Fill = FilCollor;
            retVal.Stroke = BorderColor;
            retVal.StrokeThickness = (double)BorderThickness;
            retVal.Margin = new System.Windows.Thickness(coordinates.X_coordinate, coordinates.Y_coordinate,0,0);
            retVal.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            Shape = retVal;
            return Shape;
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangePropertiesWindow changePropertiesWindow = new ChangePropertiesWindow(this, MyShapeEnum.Elipse);
            changePropertiesWindow.ShowDialog();

        }
        public void UpdateShape(int width, int height, Brush fillColor, Brush borderColor, int borderThickness)
        {
            if (Shape == null)
                return;
            Shape.Height = height;
            Shape.Width = width;
            Shape.Fill = fillColor;
            Shape.Stroke = borderColor;
        }




    }
}

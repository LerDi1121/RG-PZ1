using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PZ1_PR132_2016.Models
{
    class MyPolygon : MyShape
    {
        private PointCollection pointColection;
        public MyPolygon(List<System.Windows.Point> points, Brush fillColor, Brush borderColor, int borderThickness) : base(fillColor, borderColor, borderThickness)
        {
            pointColection = new PointCollection(points);
        }

        public PointCollection PointColection { get => pointColection; set => pointColection = value; }

        public override Shape Draw()
        {
            if (Shape != null)
                return Shape;
            Polygon retVal = new Polygon();
            retVal.Stroke = BorderColor;
            retVal.Fill = FilCollor;
            retVal.StrokeThickness = BorderThickness;
            retVal.Points = PointColection;
            retVal.MouseLeftButtonDown += Polygon_MouseLeftButtonDown;
            Shape = retVal;
            return Shape;
        }

        private void Polygon_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChangePropertiesWindow changePropertiesWindow = new ChangePropertiesWindow(this, MyShapeEnum.Polygon);
            changePropertiesWindow.ShowDialog();

        }
        public void UpdateShape(Brush fillColor, Brush borderColor, int borderThickness)
        {
            Shape.Stroke = borderColor;
            Shape.Fill = fillColor;
            Shape.StrokeThickness = borderThickness;
            BorderThickness = borderThickness;
            FilCollor = fillColor;
            BorderColor = borderColor;
        }
    }
}

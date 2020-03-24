using System;
using System.Collections.Generic;
using System.Drawing;
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
        public MyPolygon(List<System.Windows.Point> points,  System.Windows.Media.Brush fillColor, System.Windows.Media.Brush borderColor, int borderThickness) : base(fillColor, borderColor, borderThickness)
        {
            pointColection = new PointCollection(points);
        }

        public PointCollection PointColection { get => pointColection; set => pointColection = value; }

        public override Shape Draw()
        {
            Polygon retVal = new Polygon();
            retVal.Stroke = BorderColor;
            retVal.Fill = FilCollor;
            retVal.StrokeThickness = BorderThickness;
            retVal.Points = PointColection;
            retVal.MouseLeftButtonDown += RetVal_MouseLeftButtonDown;
            Shape = retVal;
            return Shape;
        }

        private void RetVal_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

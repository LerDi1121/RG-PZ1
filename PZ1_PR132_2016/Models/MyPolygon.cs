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
        public MyPolygon(Brush fillColor, Brush borderColor, int borderThickness) : base(fillColor, borderColor, borderThickness)
        {
        }

        public override Shape Draw()
        {
            Polygon retVal = new Polygon();
            return retVal;
        }
    }
}

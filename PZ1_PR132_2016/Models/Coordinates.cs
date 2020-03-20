using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ1_PR132_2016.Models
{
    public class Coordinates
    {
        private double x_coordinate;
        private double y_coordinate;
        public Coordinates(double x = 0, double y = 0)
        {
            if (x < 0 || y < 0)
                throw new ArgumentOutOfRangeException();
            X_coordinate = x;
            Y_coordinate = y;
            
        }
        public double X_coordinate { get => x_coordinate; set => x_coordinate = value; }
        public double Y_coordinate { get => y_coordinate; set => y_coordinate = value; }
    }
}

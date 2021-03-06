﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PZ1_PR132_2016.Models
{
    public abstract class MyShape
    {
        private Brush filCollor;
        private Brush borderColor;
        private int borderThickness;
        private  Shape shape;
        public MyShape( Brush fillColor, Brush borderColor, int borderThickness)
        {
            FilCollor = fillColor;
            BorderColor = borderColor;
            BorderThickness = borderThickness;
            shape = null;
        }
        public MyShape()
        {
                
        }
        public Brush FilCollor { get => filCollor; set => filCollor = value; }
        public Brush BorderColor { get => borderColor; set => borderColor = value; }
        public int BorderThickness { get => borderThickness; set => borderThickness = value; }
        public Shape Shape { get => shape; set => shape = value; }

        public abstract Shape Draw();
       
       
    }
}

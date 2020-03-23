﻿using System;
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
            return retVal;


            //System.Drawing.Rectangle retVal = new System.Drawing.Rectangle((int)coordinates.X_coordinate,(int)coordinates.Y_coordinate,width,height);
            //Bitmap bitmap = new Bitmap( width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //using (Graphics graphics = Graphics.FromImage(bitmap))
            //{
            //    using (System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red))
            //    {
            //        graphics.FillRectangle(myBrush, retVal); // whatever
            //                                                 // and so on...
            //    }

            //}
            //bitmap.Save(path);
        }
    }
}
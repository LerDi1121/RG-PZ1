using Microsoft.Win32;
using PZ1_PR132_2016.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PZ1_PR132_2016
{
    /// <summary>
    /// Interaction logic for PropertiesWindow.xaml
    /// </summary>
    
        public enum MyShapeEnum { Elipse, Rectangle , Polygon , Image }
    public partial class PropertiesWindow : Window
    {
        private MyShapeEnum ShapeToDraw;
        static List<Button> AllButons;
        private Point PointToDraw;
        private string imgPath;
        private List<Point> pointsForPolygon;
        public PropertiesWindow( Point point)
        {
            InitializeComponent();
            AllButons = new List<Button>() {btnCancel,btnDraw, btnFindImage};

            cbBorder.ItemsSource = typeof(Colors).GetProperties();
            cbFill.ItemsSource = typeof(Colors).GetProperties();

            PointToDraw = point;
            ChooseTitle();
        }
        public PropertiesWindow(List<Point> points)
        {
            InitializeComponent();
            AllButons = new List<Button>() { btnCancel, btnDraw, btnFindImage };

            cbBorder.ItemsSource = typeof(Colors).GetProperties();
            cbFill.ItemsSource = typeof(Colors).GetProperties();

            pointsForPolygon = points;
            ChooseTitle();
        }

        void ChooseTitle()
        {
           foreach(var temp in MainWindow.BtnShape)
                if(temp.Value)
                {
                   switch(temp.Key)
                    {
                        case "btnEllipse":
                            Title = "Draw a Ellipse";
                            ShapeToDraw = MyShapeEnum.Elipse;
                            EllipseWind();
                            break;
                        case "btnRectangle":
                            Title = "Draw a Rectangle";
                            ShapeToDraw = MyShapeEnum.Rectangle;
                            RectangleWind();
                            break;
                        case "btnPolygon":
                            Title = "Draw a Polygon";
                            ShapeToDraw = MyShapeEnum.Polygon;
                            PolygonWind();
                            break;
                        case "btnImage":
                            Title = "Draw a Image";
                            ShapeToDraw = MyShapeEnum.Image;
                            ImageWind();
                            break;
                    }
                }
        }
        void PolygonWind()
        {
            tblokWidth.Style = (Style)FindResource("TextBlockDisabledStyle");
            tbHeight.Style = (Style)FindResource("TextBlockDisabledStyle");
            tbxWidth.IsEnabled = false;
            tboxHeight.IsEnabled = false;
            tbImage.Style = (Style)FindResource("TextBlockDisabledStyle");
            btnFindImage.Style = (Style)FindResource("ButtonDisabledStyle");
            btnFindImage.IsEnabled = false;
        }
        void RectangleWind()
        {
            EllipseWind();
        }
        void ImageWind()
        {
            tbBorderColor.Style=(Style) FindResource("TextBlockDisabledStyle");
            tbFillColor.Style= (Style)FindResource("TextBlockDisabledStyle");
            tbBorderColor.Style = (Style)FindResource("TextBlockDisabledStyle");
            tbBorderThicknes.Style = (Style)FindResource("TextBlockDisabledStyle");
            cbFill.IsEnabled = false;
            cbBorder.IsEnabled = false;
            tbBorderTh.IsEnabled = false;

        }
        void EllipseWind()
        {
            tbImage.Style =(Style) FindResource("TextBlockDisabledStyle");
            btnFindImage.IsEnabled = false;
            btnFindImage.Style = (Style)FindResource("ButtonDisabledStyle");//ButtonDisableStyle
        }
        private void MouseEnter(object sender, MouseEventArgs e)
        {
            foreach (Button temp in AllButons)
                if (temp.IsMouseOver && temp.IsEnabled)
                    MosueOverButtonStyle(temp);

        }
        private void MouseLeave(object sender, MouseEventArgs e)
        {
            foreach (Button temp in AllButons)
                if (!temp.IsMouseOver  && temp.IsEnabled)
                    CasualButtonStyle(temp);
        }
        private void CasualButtonStyle(Button button)
        {
            Style style = (Style)FindResource("ButtonStyle");
            button.Style = style;
        }
        private void MosueOverButtonStyle(Button button)
        {
            Style style = (Style)FindResource("ButtonStyleMouseOver") ;
            button.Style = style;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            Close();
        }

        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            MyShape RetVal = null;
            switch(ShapeToDraw)
            { case MyShapeEnum.Elipse:
                    if (!(ValidateTexBox(tbxWidth) && ValidateTexBox(tboxHeight) && ValidateTexBox(tbBorderTh)&& ValidateColor(cbBorder)&&ValidateColor(cbFill)))
                        return;
                    RetVal = CreateEllipse();
                
                    break;
                case MyShapeEnum.Rectangle:

                    if (!(ValidateTexBox(tbxWidth) && ValidateTexBox(tboxHeight) && ValidateTexBox(tbBorderTh) && ValidateColor(cbBorder) && ValidateColor(cbFill)))
                        return;
                    RetVal = CreateRectangle();

                   break;
                case MyShapeEnum.Image:
                    if (!(ValidateTexBox(tbxWidth) && ValidateTexBox(tboxHeight) && ValidateImagePath(imgPath)))
                        return;
                    RetVal = CreateImage();
                    break;
                case MyShapeEnum.Polygon:
                    if (!(ValidateColor(cbBorder) && ValidateColor(cbFill) && ValidateTexBox(tbBorderTh)))
                        return;
                    RetVal = Createpolygon();
                        break;

            }
            TransferClass.NewShape = RetVal;
      
       
            Close();
        }
         bool ValidateImagePath(string path)
        {
            return ValidateClass.ValidateImagePath(path);
        }
         bool ValidateColor(ComboBox comboBox)
        {
            if (!ValidateClass.ValidateColor(comboBox))
            {
                comboBox.BorderBrush = Brushes.Red;
                return false;
            }
            comboBox.BorderBrush = Brushes.LightGray;
            return true;
        }
         bool ValidateTexBox(TextBox textBox)
        {
            if (!ValidateClass.ValidateTexBox(textBox))
            {
                textBox.Style = (Style)FindResource("TextboxErrorStyle");
                return false;
            }

            return true;
        }

        MyShape CreateImage()
        {
            int width = Int32.Parse(tbxWidth.Text);
            int height = Int32.Parse(tboxHeight.Text);
            return new MyImage(PointToDraw.X, PointToDraw.Y, width, height, imgPath);
        }
        SolidColorBrush CreateColor(ComboBox comboBox)
        {
            var selectedItem = (PropertyInfo)comboBox.SelectedItem;
            Color color = (Color)selectedItem.GetValue(null, null);
            return new SolidColorBrush(color);
        }
        MyShape Createpolygon()
        {
            SolidColorBrush border = CreateColor(cbBorder);
            SolidColorBrush Fill = CreateColor(cbFill);
            int borderTh = Int32.Parse(tbBorderTh.Text);
            return new MyPolygon(pointsForPolygon, Fill, border, borderTh);
        }
        MyShape CreateRectangle()
        {
            SolidColorBrush border = CreateColor(cbBorder);
            SolidColorBrush Fill = CreateColor(cbFill);

            int width = Int32.Parse(tbxWidth.Text);
            int height = Int32.Parse(tboxHeight.Text);
            int borderTh = Int32.Parse(tbBorderTh.Text);

            return new MyRectangle(PointToDraw.X, PointToDraw.Y, width, height, Fill,border, borderTh);
        }
        MyShape CreateEllipse()
        {
            SolidColorBrush border = CreateColor(cbBorder);
            SolidColorBrush Fill = CreateColor(cbFill);

            int width = Int32.Parse(tbxWidth.Text);
            int height = Int32.Parse(tboxHeight.Text);
            int borderTh = Int32.Parse(tbBorderTh.Text);

           return new MyEllipse(PointToDraw.X, PointToDraw.Y, width, height,  Fill, border, borderTh);
        }
        private void btnFindImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png| sAll files (*.*)|*.*";
          
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select an image file.";
            
            if (dialog.ShowDialog() == true)
            {
                imgPath = dialog.FileName;
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri(imgPath);
                logo.EndInit();

                imgImage.Source = logo;
                
            }
        }
    }
}

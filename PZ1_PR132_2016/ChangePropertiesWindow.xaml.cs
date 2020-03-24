using Microsoft.Win32;
using PZ1_PR132_2016.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ChangePropertiesWindow.xaml
    /// </summary>
    public partial class ChangePropertiesWindow : Window
    {
        MyShape ShapeToChange;
        private string imgPath;
        static List<Button> AllButons;
        MyShapeEnum myShapeEnum;
        public ChangePropertiesWindow(MyShape shape, MyShapeEnum  myShapeEnum)
        {
           
            InitializeComponent();
            cbBorder.ItemsSource = typeof(Colors).GetProperties();
            cbFill.ItemsSource = typeof(Colors).GetProperties();
            AllButons = new List<Button>() { btnCancel, btnDraw, btnFindImage };
            this.myShapeEnum = myShapeEnum;
            ShapeToChange = shape;
            ChooseTitle();
            ChoseWindowsElelment();
        }
        void ChooseTitle()
        {
            Title = "Change the " + myShapeEnum.ToString();
           
        }
        void ChoseWindowsElelment()
        {
            switch(myShapeEnum)
            {
                case MyShapeEnum.Elipse:
                    EllipseWind();
                    EllipseData();
                    break;
                case MyShapeEnum.Image:
                    ImageWind();
                    break;
                case MyShapeEnum.Polygon:
                    PolygonWind();
                    break;
                case MyShapeEnum.Rectangle:
                    RectangleWind();
                    break;
            }
        }
        void EllipseData()
        {
            Ellipse shape = (Ellipse)ShapeToChange.Shape;
            tbBorderTh.Text= shape.StrokeThickness.ToString();
            tbxWidth.Text = shape.Width.ToString();
            tboxHeight.Text = shape.Height.ToString();
            var list = typeof(Colors).GetProperties();

            SolidColorBrush temp = (SolidColorBrush)shape.Fill;
            Color tempColor = temp.Color;
            var item = list.First(x => x.Name == tempColor.ToString();

            PropertyInfo colorProperty = typeof(Colors).GetProperties()
            .FirstOrDefault(p => Color.AreClose((Color)(shape.Fill.GetValue(null)), tempColor));
            cbBorder.SelectedItem = colorProperty;
          

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
            tbBorderColor.Style = (Style)FindResource("TextBlockDisabledStyle");
            tbFillColor.Style = (Style)FindResource("TextBlockDisabledStyle");
            tbBorderColor.Style = (Style)FindResource("TextBlockDisabledStyle");
            tbBorderThicknes.Style = (Style)FindResource("TextBlockDisabledStyle");
            cbFill.IsEnabled = false;
            cbBorder.IsEnabled = false;
            tbBorderTh.IsEnabled = false;

        }
        void EllipseWind()
        {
            tbImage.Style = (Style)FindResource("TextBlockDisabledStyle");
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
                if (!temp.IsMouseOver && temp.IsEnabled)
                    CasualButtonStyle(temp);
        }
        private void CasualButtonStyle(Button button)
        {
            Style style = (Style)FindResource("ButtonStyle");
            button.Style = style;
        }
        private void MosueOverButtonStyle(Button button)
        {
            Style style = (Style)FindResource("ButtonStyleMouseOver");
            button.Style = style;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }
        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
           
            Close();
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

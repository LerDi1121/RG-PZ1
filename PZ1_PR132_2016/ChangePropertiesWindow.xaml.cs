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
                    ImageData();
                    break;
                case MyShapeEnum.Polygon:
                    PolygonWind();
                    PolygonData();
                    break;
                case MyShapeEnum.Rectangle:
                    RectangleWind();
                    RectangleData();
                    break;
            }
        }
        void SelectColorInCombobox(ComboBox comboBox, Brush brush)
        {
            var list = typeof(Colors).GetProperties();
            string temp = GetColorName((SolidColorBrush)brush);
            var item = list.First(x => x.Name == temp);
            comboBox.SelectedItem = item;
        }
        void PolygonData()
        {
            Polygon shape = (Polygon)ShapeToChange.Shape;
            DataInTextBox(tbBorderTh, shape.StrokeThickness);
            SelectColorInCombobox(cbBorder, shape.Stroke);
            SelectColorInCombobox(cbFill, shape.Fill);
        }
        void DataInImage(string path)
        {
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(path);
            
            logo.EndInit();
            imgImage.Source = logo;
        }
        void DataInTextBox(TextBox textBox, double number)
        {
            textBox.Text = number.ToString();

        }
        void ImageData()
        {
            MyImage shape = (MyImage)ShapeToChange;
            DataInTextBox(tbxWidth, shape.Width);
            DataInTextBox(tboxHeight, shape.Height);
            DataInImage(shape.Path);


        }
        void RectangleData()
        {
            Rectangle shape = (Rectangle)ShapeToChange.Shape;
            DataInTextBox(tbBorderTh, shape.StrokeThickness);
            DataInTextBox(tbxWidth, shape.Width);
            DataInTextBox(tboxHeight, shape.Height);
            SelectColorInCombobox(cbBorder, shape.Stroke);
            SelectColorInCombobox(cbFill, shape.Fill);
        }
        void EllipseData()
        {
            Ellipse shape = (Ellipse)ShapeToChange.Shape;
            DataInTextBox(tbBorderTh, shape.StrokeThickness);          
            DataInTextBox(tbxWidth, shape.Width);          
            DataInTextBox(tboxHeight, shape.Height);
            SelectColorInCombobox(cbBorder, shape.Stroke);
            SelectColorInCombobox(cbFill, shape.Fill);
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
        private string GetColorName(SolidColorBrush brush)
        {
            var results = typeof(Colors).GetProperties().Where(
             p => (Color)p.GetValue(null, null) == brush.Color).Select(p => p.Name);
            return results.Count() > 0 ? results.First() : String.Empty;
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
            switch (myShapeEnum)
            {
                case MyShapeEnum.Elipse:
                    if (!(ValidateTexBox(tbxWidth) && ValidateTexBox(tboxHeight) && ValidateTexBox(tbBorderTh) && ValidateColor(cbBorder) && ValidateColor(cbFill)))
                        return;
                    UpdateEllipse((MyEllipse)ShapeToChange);
                   
                    break;
                case MyShapeEnum.Rectangle:

                    if (!(ValidateTexBox(tbxWidth) && ValidateTexBox(tboxHeight) && ValidateTexBox(tbBorderTh) && ValidateColor(cbBorder) && ValidateColor(cbFill)))
                        return;
                    UpdateRectangle((MyRectangle)ShapeToChange);
                                        break;
                case MyShapeEnum.Image:
                    if (!(ValidateTexBox(tbxWidth) && ValidateTexBox(tboxHeight) && ValidateImagePath(imgPath)))
                        return;
                    UpdateImage((MyImage)ShapeToChange);
                    break;
                case MyShapeEnum.Polygon:
                    if (!(ValidateColor(cbBorder) && ValidateColor(cbFill) && ValidateTexBox(tbBorderTh)))
                        return;
                    UpdatePolygon((MyPolygon)ShapeToChange);
                    break;
                    
            }
            MainWindow.ChangeShapeDel(ShapeToChange);
            Close();
        }
        void UpdatePolygon(MyPolygon polygon)
        {
            SolidColorBrush border = CreateColor(cbBorder);
            SolidColorBrush Fill = CreateColor(cbFill);
            int borderTh = Int32.Parse(tbBorderTh.Text);
            polygon.UpdateShape(Fill, border, borderTh);

        }
         void  UpdateImage(MyImage image)
        {
            int width = Int32.Parse(tbxWidth.Text);
            int height = Int32.Parse(tboxHeight.Text);

            image.UpdateShape(width, height, imgPath);
        }
        void UpdateRectangle(MyRectangle rectangle)
        {
            SolidColorBrush border = CreateColor(cbBorder);
            SolidColorBrush Fill = CreateColor(cbFill);

            int width = Int32.Parse(tbxWidth.Text);
            int height = Int32.Parse(tboxHeight.Text);
            int borderTh = Int32.Parse(tbBorderTh.Text);
            rectangle.UpdateShape(width, height, Fill, border, borderTh);
        }
        void UpdateEllipse(MyEllipse ellipse)
        {
            SolidColorBrush border = CreateColor(cbBorder);
            SolidColorBrush Fill = CreateColor(cbFill);

            int width = Int32.Parse(tbxWidth.Text);
            int height = Int32.Parse(tboxHeight.Text);
            int borderTh = Int32.Parse(tbBorderTh.Text);
            ellipse.UpdateShape(width, height, Fill, border, borderTh);
        }
        SolidColorBrush CreateColor(ComboBox comboBox)
        {
            var selectedItem = (PropertyInfo)comboBox.SelectedItem;
            Color color = (Color)selectedItem.GetValue(null, null);
            return new SolidColorBrush(color);
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

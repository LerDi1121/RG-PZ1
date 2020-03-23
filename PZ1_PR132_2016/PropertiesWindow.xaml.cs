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
    
        internal enum MyShapeEnum { Elipse, Rectangle , Polygon , Image }
    public partial class PropertiesWindow : Window
    {
        private MyShapeEnum ShapeToDraw;
        static List<Button> AllButons;
        private Point PointToDraw;
        private string imgPath;
        public PropertiesWindow( Point point)
        {
            InitializeComponent();
            AllButons = new List<Button>() {btnCancel,btnDraw, btnFindImage};

            cbBorder.ItemsSource = typeof(Colors).GetProperties();
            cbFill.ItemsSource = typeof(Colors).GetProperties();

            PointToDraw = point;
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
                            break;
                        case "btnPolygon":
                            Title = "Draw a Polygon";
                            ShapeToDraw = MyShapeEnum.Polygon;
                            break;
                        case "btnImage":
                            Title = "Draw a Image";
                            ShapeToDraw = MyShapeEnum.Image;
                            ImageWind();
                            break;
                    }
                }
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

                    break;
                case MyShapeEnum.Image:
                    if (!(ValidateTexBox(tbxWidth) && ValidateTexBox(tboxHeight) && ValidateImagePath()))
                        return;
                    RetVal = CreateImage();
                    break;
                case MyShapeEnum.Polygon:

                    break;

            }
            TransferClass.NewShape = RetVal;
      
       
            Close();
        }
        bool ValidateImagePath()
        {
            if(imgPath.Equals(string.Empty))
            {
                MessageBox.Show("Please select an image file!");
                return false;
            }
            return true;
        }
        bool ValidateColor(ComboBox comboBox)
        {
            if (cbBorder.SelectedItem == null)
            {
                comboBox.BorderBrush = Brushes.Red;
                MessageBox.Show("Select color!");
                return false;
            }
            comboBox.BorderBrush = Brushes.LightGray;

            return true;
        }
        bool ValidateTexBox( TextBox textBox)
        {
            if(textBox.Text.Length==0 || string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Style = (Style)FindResource("TextboxErrorStyle");
                MessageBox.Show("Insert number!");
                return false;

            }

            int num = 0;
            if(!Int32.TryParse(textBox.Text, out num))
            {
                textBox.Style = (Style)FindResource("TextboxErrorStyle");
                MessageBox.Show("Insert number!");
                return false;
            }
            if(num<0)
            {
                textBox.Style = (Style)FindResource("TextboxErrorStyle");
                MessageBox.Show("Number must be greater than zero!");
                return false;
            }
            textBox.Style = (Style)FindResource("TextboxStyle");
            return true;
        }
        MyShape CreateImage()
        {
            int width = Int32.Parse(tbxWidth.Text);
            int height = Int32.Parse(tboxHeight.Text);
            return new MyImage(PointToDraw.X, PointToDraw.Y, width, height, imgPath);
        }
        MyShape CreateEllipse()
        {
            var selectedItem = (PropertyInfo)cbBorder.SelectedItem;
            Color color = (Color)selectedItem.GetValue(null, null);
            SolidColorBrush border = new SolidColorBrush(color);

            selectedItem = (PropertyInfo)cbFill.SelectedItem;
            color = (Color)selectedItem.GetValue(null, null);
            SolidColorBrush Fill = new SolidColorBrush(color);
            int width = Int32.Parse(tbxWidth.Text);
            int height = Int32.Parse(tboxHeight.Text);
            int borderTh = Int32.Parse(tbBorderTh.Text);

           return new MyEllipse(PointToDraw.X, PointToDraw.Y, width, height, border, Fill, borderTh);
        }
        private void btnFindImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png|vAll files (*.*)|*.*";
          
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

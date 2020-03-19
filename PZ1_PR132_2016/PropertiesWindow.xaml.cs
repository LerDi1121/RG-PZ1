using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class PropertiesWindow : Window
    {
        static List<Button> AllButons;
        public PropertiesWindow()
        {
            InitializeComponent();
            AllButons = new List<Button>() {btnCancel,btnDraw };
            cbBorder.ItemsSource = typeof(Colors).GetProperties();

            cbFill.ItemsSource = typeof(Colors).GetProperties();
            ChooseTitle();
        }
        private void MouseEnter(object sender, MouseEventArgs e)
        {
            foreach (Button temp in AllButons)
                if (temp.IsMouseOver)
                   MosueOverButtonStyle(temp);

        }
        void ChooseTitle()
        {
           foreach(var temp in MainWindow.dictionaries)
                if(temp.Value)
                {
                   switch(temp.Key)
                    {
                        case "btnEllipse":
                            Title = "Draw a Ellipse";
                            break;
                        case "btnRectangle":
                            Title = "Draw a Rectangle";
                            break;
                        case "btnPolygon":
                            Title = "Draw a Polygon";
                            break;
                        case "btnImage":
                            Title = "Draw a Image";
                            break;
                    }
                }
        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            foreach (Button temp in AllButons)
                if (!temp.IsMouseOver)
                    CasualButtonStyle(temp);
        }
        private void CasualButtonStyle(Button button)
        {
            Style style = FindResource("ButtonStyle") as Style;
            button.Style = style;
        }
        private void MosueOverButtonStyle(Button button)
        {
            Style style = FindResource("ButtonStyleMouseOver") as Style;
            button.Style = style;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

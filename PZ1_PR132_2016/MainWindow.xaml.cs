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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PZ1_PR132_2016
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Dictionary <string,bool> dictionaries ;
        static List<Button> AllButons;
        public MainWindow()
        {
            InitializeComponent();
            dictionaries = new Dictionary<string, bool>
            {
                { btnEllipse.Name,false },
                {btnImage.Name,false },
                {btnPolygon.Name,false},
                {btnRectangle.Name, false}
            };
            AllButons = new List<Button> { btnEllipse, btnImage, btnPolygon, btnRectangle, btnRedo, btnUndo, btnClear };
        }



        private void btnImage_Click(object sender, RoutedEventArgs e)
        {

            SelectButtonStyle(btnImage);
            CasualButtonStyle(btnPolygon);
            CasualButtonStyle(btnEllipse);
            CasualButtonStyle(btnRectangle);
            Selectbutton(btnImage.Name);

        }

            private void btnPolygon_Click(object sender, RoutedEventArgs e)
        {

            SelectButtonStyle(btnPolygon);
            CasualButtonStyle(btnImage);
            CasualButtonStyle(btnEllipse);
            CasualButtonStyle(btnRectangle);
            Selectbutton(btnPolygon.Name);    
        }

        private void btnEllipse_Click(object sender, RoutedEventArgs e)
        {

            SelectButtonStyle(btnEllipse);
            CasualButtonStyle(btnImage);
            CasualButtonStyle(btnPolygon);
            CasualButtonStyle(btnRectangle);
            Selectbutton(btnEllipse.Name);

         
        }

        private void btnRectangle_Click(object sender, RoutedEventArgs e)
        {


            SelectButtonStyle(btnRectangle);
            CasualButtonStyle(btnPolygon);
            CasualButtonStyle(btnEllipse);
            CasualButtonStyle(btnImage);
            Selectbutton(btnRectangle.Name);
        }
        private void Selectbutton(string btnName)
        {
            foreach (var temp in dictionaries)
            {
                if (temp.Key.Equals(btnName))
                {
                    dictionaries[temp.Key] = true;
                    break;
                }

            }
            Dictionary<string, bool> TempDictionaries = new Dictionary<string, bool>(dictionaries);
            foreach (var temp in dictionaries)
            {
                if (!temp.Key.Equals(btnName) && temp.Value)
                {
                    TempDictionaries[temp.Key] = false;
                }

            }
            dictionaries = TempDictionaries;
        }
        private void SelectButtonStyle(Button button)
        {
            Style style = FindResource("SelectedButtonStyle") as Style;
            button.Style = style;
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

        private void MouseEnter(object sender, MouseEventArgs e)
        {
            foreach(Button temp in AllButons)
                if(temp.IsMouseOver)
                    MosueOverButtonStyle(temp);
           
        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            foreach (Button temp in AllButons)
                if (!temp.IsMouseOver && dictionaries.ContainsKey(temp.Name))
                {
                    if(dictionaries[temp.Name])
                    {
                        SelectButtonStyle(temp);
                    }
                    else
                    CasualButtonStyle(temp);
                }
                  
                else if (!temp.IsMouseOver)
                    CasualButtonStyle(temp);
        }

        
    }
}

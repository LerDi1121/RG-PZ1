using PZ1_PR132_2016.Models;
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
        private static bool EnableDrawingPontForPolygon;
        static private List<Point> PointForPolygon;
        public static Dictionary<string, bool> BtnShape;
        static List<Button> AllButons;
     //   private Canvas MyCanvas = (Canvas)FindResource("ResourceCanvas"); //ResourceCanvas
        public MainWindow()
        {
            InitializeComponent();
            BtnShape = new Dictionary<string, bool>
            {
                { btnEllipse.Name,false },
                {btnImage.Name,false },
                {btnPolygon.Name,false},
                {btnRectangle.Name, false}
            };
            AllButons = new List<Button> { btnEllipse, btnImage, btnPolygon, btnRectangle, btnRedo, btnUndo, btnClear };
            EnableDrawingPontForPolygon = false;
            PointForPolygon = new List<Point>();
        }



        private void btnImage_Click(object sender, RoutedEventArgs e)
        {

            Selectbutton(btnImage.Name);
            SelectButtonStyle(btnImage);
            CasualButtonStyle(btnPolygon);
            CasualButtonStyle(btnEllipse);
            CasualButtonStyle(btnRectangle);
            DisableDrawingPoint();

        }

        private void btnPolygon_Click(object sender, RoutedEventArgs e)
        {

            Selectbutton(btnPolygon.Name);
            SelectButtonStyle(btnPolygon);
            EnebleDrawingPlygon();
            CasualButtonStyle(btnImage);
            CasualButtonStyle(btnEllipse);
            CasualButtonStyle(btnRectangle);

        }

        private void btnEllipse_Click(object sender, RoutedEventArgs e)
        {
            DisableDrawingPoint();
            Selectbutton(btnEllipse.Name);
            SelectButtonStyle(btnEllipse);
            CasualButtonStyle(btnImage);
            CasualButtonStyle(btnPolygon);
            CasualButtonStyle(btnRectangle);
        }

        private void btnRectangle_Click(object sender, RoutedEventArgs e)
        {
            DisableDrawingPoint();
            Selectbutton(btnRectangle.Name);

            CasualButtonStyle(btnPolygon);
            CasualButtonStyle(btnEllipse);
            CasualButtonStyle(btnImage);
            SelectButtonStyle(btnRectangle);
        }
        void EnebleDrawingPlygon()
        {
            if (!EnableDrawingPontForPolygon)
            {
                EnableDrawingPontForPolygon = true;
                PointForPolygon = new List<Point>();
            }
            else
            {
                DisableDrawingPoint();
            }
            
        }
        void DisableDrawingPoint()
        {
            if (EnableDrawingPontForPolygon)
            {
                EnableDrawingPontForPolygon = false;
            }
        }

    private void Selectbutton(string btnName)
        {

            foreach (var temp in BtnShape)
            {
                if (temp.Key.Equals(btnName))
                {
                    BtnShape[temp.Key] = !temp.Value;
                    break;
                }

            }
            Dictionary<string, bool> TempDictionaries = new Dictionary<string, bool>(BtnShape);
            foreach (var temp in BtnShape)
            {
                if (!temp.Key.Equals(btnName))
                    TempDictionaries[temp.Key] = false;
            }
            BtnShape = TempDictionaries;

        }
        private void SelectButtonStyle(Button button)
        {
            Style SelectStyle = FindResource("SelectedButtonStyle") as Style;
            if (BtnShape[button.Name])
                button.Style = SelectStyle;
            else
                CasualButtonStyle(button);
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
            foreach (Button temp in AllButons)
                if (temp.IsMouseOver)
                    MosueOverButtonStyle(temp);

        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            foreach (Button temp in AllButons)
                if (!temp.IsMouseOver && BtnShape.ContainsKey(temp.Name))
                {
                    if (BtnShape[temp.Name])
                    {
                        SelectButtonStyle(temp);
                    }
                    else
                        CasualButtonStyle(temp);
                }

                else if (!temp.IsMouseOver)
                    CasualButtonStyle(temp);
        }

        private void MyCanvas_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var temp in BtnShape)
                if (temp.Value)
                {
                    if (temp.Key.Equals(btnPolygon.Name) && EnableDrawingPontForPolygon)
                    {
                        PointForPolygon.Add(e.GetPosition(MyCanvas));
                        return;
                    }
                   
                    else
                    {
                        Point point = e.GetPosition(MyCanvas);
                        PropertiesWindow propertiesWindow = new PropertiesWindow(point);
                        propertiesWindow.ShowDialog();
                        AddShapeOnCanvas();
                    }

                }

        }
        void AddShapeOnCanvas()
        {
            MyShape shape = TransferClass.NewShape;
            if (shape == null)
                return;
            TransferClass.ActiveShape.Push(shape);
            MyCanvas.Children.Add(shape.Draw());
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {

            if (TransferClass.ClearShape.Count != 0)
            {
                UndoClearAction();
                return;
            }   
            if (TransferClass.ActiveShape.Count == 0)
                return;
            MyShape shape = TransferClass.ActiveShape.Pop();

            MyCanvas.Children.RemoveAt(MyCanvas.Children.Count - 1);
            TransferClass.Undo.Push(shape);
        }
        void UndoClearAction()
        {
            while (TransferClass.ClearShape.Count != 0)
            {
                MyShape temp = TransferClass.ClearShape.Pop();
                MyCanvas.Children.Add(temp.Draw());
                TransferClass.ActiveShape.Push(temp);
            }
        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            if (TransferClass.Undo.Count == 0)
                return;
            MyShape shape = TransferClass.Undo.Pop();
            MyCanvas.Children.Add(shape.Draw());
            TransferClass.ActiveShape.Push(shape);
        }

        private void MyCanvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!EnableDrawingPontForPolygon)
                return;

              PointForPolygon = new List<Point>(PointForPolygon);
                PropertiesWindow propertiesWindow = new PropertiesWindow(PointForPolygon);
                propertiesWindow.ShowDialog();
                PointForPolygon = new List<Point>();
                AddShapeOnCanvas();

            
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            while(TransferClass.ActiveShape.Count!=0)
            {
                TransferClass.ClearShape.Push(TransferClass.ActiveShape.Pop());
                MyCanvas.Children.Clear();
            }
        }
       
           
        void ChangeShape(Shape shape)
        {

        }
    }
}

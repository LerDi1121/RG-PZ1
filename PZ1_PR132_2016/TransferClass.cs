using PZ1_PR132_2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ1_PR132_2016
{
    public  class TransferClass
    {
        private static MyShape newShape;
        private static Stack<MyShape> forUndo;
        private static Stack<MyShape> forRedo;
        private static Stack<MyShape> activeShape;
        private static Stack<MyShape> clearShape;

        public static MyShape NewShape { 
            get 
            { MyShape temp = newShape;
                newShape = null;
                return temp;
            }
             set => newShape = value; }
        public static Stack<MyShape> ForUndo { get => forUndo; set => forUndo = value; }
        public static Stack<MyShape> ForRedo { get => forRedo; set => forRedo = value; }
        public static Stack<MyShape> ActiveShape { get => activeShape; set => activeShape = value; }
        public static Stack<MyShape> ClearShape  { get => clearShape; set => clearShape = value; }
    }
}

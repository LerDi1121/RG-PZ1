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
        private static Stack<MyShape> undo = new Stack<MyShape>();
        private static Stack<MyShape> activeShape = new Stack<MyShape>();
        private static Stack<MyShape> clearShape = new Stack<MyShape>();

        public static MyShape NewShape { 
            get 
            { MyShape temp = newShape;
                newShape = null;
                return temp;
            }
             set => newShape = value; }
  
        public static Stack<MyShape> ActiveShape { get => activeShape; set => activeShape = value; }
        public static Stack<MyShape> ClearShape  { get => clearShape; set => clearShape = value; }
        public static Stack<MyShape> Undo{ get => undo; set => undo = value; }
       
    }
}

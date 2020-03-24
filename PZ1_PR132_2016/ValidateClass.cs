using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PZ1_PR132_2016
{
    static class ValidateClass
    {
       static public bool ValidateImagePath(string path)
        {
            if (path.Equals(string.Empty))
            {
                MessageBox.Show("Please select an image file!");
                return false;
            }
            return true;
        }
        static public bool ValidateColor(ComboBox comboBox)
        {
            if (comboBox.SelectedItem == null)
            {
                comboBox.BorderBrush = Brushes.Red;
                MessageBox.Show("Select color!");
                return false;
            }
            comboBox.BorderBrush = Brushes.LightGray;

            return true;
        }
        static public bool ValidateTexBox(TextBox textBox)
        {
            if (textBox.Text.Length == 0 || string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show("Insert number!");
                return false;

            }

            int num = 0;
            if (!Int32.TryParse(textBox.Text, out num))
            {
                MessageBox.Show("Insert number!");
                return false;
            }
            if (num < 0)
            {
                MessageBox.Show("Number must be greater than zero!");
                return false;
            }

            return true;
        }

      
    }
}

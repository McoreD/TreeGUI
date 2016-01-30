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

namespace TreeGUI
{
    public partial class CustomMessageBox : UserControl
    {
        public CustomMessageBox(string text, string button1, string button2)
        {
            InitializeComponent();
            Text.Text = text;
            Button1.Content = button1;
            Button2.Content = button2;
        }
    }
}
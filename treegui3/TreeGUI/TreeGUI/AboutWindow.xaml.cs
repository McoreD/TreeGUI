using ShareX.HelpersLib;
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

namespace TreeGUI
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();

            txtLocation.AppendText(Assembly.GetAssembly(this.GetType()).Location);

            App.LoadedAssemblies.ForEach(x => lbAssemblies.Items.Add(x));
        }

        private void btnWebsite_Click(object sender, RoutedEventArgs e)
        {
            URLHelpers.OpenURL("https://github.com/McoreD/TreeGUI");
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
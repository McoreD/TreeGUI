using ShareX.HelpersLib;
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
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace TreeGUI
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            cboLoadSettingsHz.ItemsSource = Enumerable.Range(1, 24).ToArray();
            cboIndexHz.ItemsSource = Enumerable.Range(1, 24).ToArray();

            chkAlwaysOnTop.IsChecked = Program.Settings.AlwaysOnTop;
            tbIsDarkTheme.IsChecked = Program.Settings.IsDarkTheme;
            txtConfigFolder.Text = Program.Settings.ConfigFolder;

            cboLoadSettingsHz.SelectedValue = Program.Settings.LoadSettingsHz;

            chkIndexSetInterval.IsChecked = Program.Settings.IsIndexSetInterval;
            cboIndexHz.SelectedValue = Program.Settings.IndexsHz;

            chkIndexSetTime.IsChecked = Program.Settings.IsIndexSetTime;
            tpIndex.SelectedTime = Program.Settings.IndexTime;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Program.Settings.AlwaysOnTop = chkAlwaysOnTop.IsChecked ?? false;
            Program.Settings.IsDarkTheme = tbIsDarkTheme.IsChecked ?? false;
            Program.Settings.ConfigFolder = txtConfigFolder.Text;

            Program.Settings.LoadSettingsHz = (int)cboLoadSettingsHz.SelectedValue;

            Program.Settings.IsIndexSetInterval = chkIndexSetInterval.IsChecked ?? false;
            Program.Settings.IndexsHz = (int)cboIndexHz.SelectedValue;

            Program.Settings.IsIndexSetTime = chkIndexSetTime.IsChecked ?? false;
            Program.Settings.IndexTime = tpIndex.SelectedTime.Value;

            Program.SaveSettings();
        }

        private void btnBrowseConfigFolder_Click(object sender, RoutedEventArgs e)
        {
            string dir = HelpersLib.Helpers.BrowseFolder("Browse config folder...");
            if (!string.IsNullOrEmpty(dir))
            {
                txtConfigFolder.Text = dir;
            }
        }
    }
}
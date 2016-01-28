using Microsoft.Win32;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Program.LoadProgramSettings();
        }

        private void FileMenuItem_Click(object sender, RoutedEventArgs e)
        {
        }

        private void FileExitCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void FileExitCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void FileOpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
        }

        private void FileOpenCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ToolsConfigProperties_Click(object sender, RoutedEventArgs e)
        {
            ConfigWindow window = new ConfigWindow();
            window.ShowDialog();
        }

        private void ToolsSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow window = new SettingsWindow();
            window.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Program.SaveSettings();
        }
    }
}
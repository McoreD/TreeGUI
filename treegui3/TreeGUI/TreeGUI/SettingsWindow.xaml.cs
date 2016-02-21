using HelpersLib;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
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
        public ICommand ToggleThemeCommand { get; } = new SimpleCommand(o => ApplyTheme((bool)o));

        public SettingsWindow()
        {
            InitializeComponent();
            cboLoadSettingsHz.ItemsSource = Enumerable.Range(1, 24).ToArray();
            cboIndexHz.ItemsSource = Enumerable.Range(1, 24).ToArray();
            new SwatchesProvider().Swatches.ToList<Swatch>().ForEach<Swatch>(x => cboPrimaryColor.Items.Add(x.Name));

            chkAlwaysOnTop.IsChecked = Program.Settings.AlwaysOnTop;
            tbIsDarkTheme.IsChecked = Program.Settings.IsDarkTheme;
            cboPrimaryColor.SelectedItem = Program.Settings.PrimaryColor;

            txtConfigFolder.Text = Program.Settings.ConfigFolder;

            cboLoadSettingsHz.SelectedValue = Program.Settings.LoadSettingsHz;

            chkIndexSetInterval.IsChecked = Program.Settings.IsIndexSetInterval;
            cboIndexHz.SelectedValue = Program.Settings.IndexsHz;

            chkIndexSetTime.IsChecked = Program.Settings.IsIndexSetTime;
            tpIndex.SelectedTime = Program.Settings.IndexTime;
        }

        private static void ApplyTheme(bool isDarkTheme)
        {
            if (Program.Settings != null)
            {
                Program.Settings.IsDarkTheme = isDarkTheme;
                Program.Settings.TriggerSettingsChange();
            }

            new PaletteHelper().SetLightDark(isDarkTheme);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Program.Settings.AlwaysOnTop = chkAlwaysOnTop.IsChecked ?? false;
            Program.Settings.IsDarkTheme = tbIsDarkTheme.IsChecked ?? false;
            Program.Settings.PrimaryColor = cboPrimaryColor.SelectedItem.ToString();

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

        private void cboPrimaryColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Program.Settings.PrimaryColor = cboPrimaryColor.SelectedItem.ToString();
            new PaletteHelper().ReplacePrimaryColor(Program.Settings.PrimaryColor);
        }
    }
}
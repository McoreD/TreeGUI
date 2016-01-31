using MahApps.Metro.Controls;
using ShareX.IndexerLib;
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

namespace TreeGUI
{
    /// <summary>
    /// Interaction logic for ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : MetroWindow
    {
        public ConfigWindow()
        {
            InitializeComponent();
            pgConfigSettings.SelectedObject = Program.Config;
            pgIndexerSettings.SelectedObject = Program.Config.IndexerSettings;

            pgConfigSettings.PropertyValueChanged += PropertyGridConfigSettings_PropertyValueChanged;
            pgIndexerSettings.PropertyValueChanged += PgIndexerSettings_PropertyValueChanged;
        }

        private void PgIndexerSettings_PropertyValueChanged(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyValueChangedEventArgs e)
        {
            Program.ConfigEdited = true;
        }

        private void PropertyGridConfigSettings_PropertyValueChanged(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyValueChangedEventArgs e)
        {
            Program.ConfigEdited = true;
        }
    }
}
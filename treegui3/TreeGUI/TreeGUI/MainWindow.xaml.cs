using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using ShareX.HelpersLib;
using ShareX.IndexerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                if (args[1].EndsWith(".tgcj"))
                {
                    LoadConfig(args[1]);
                }
            }
            else
            {
                UpdateWindowUI();
            }
        }

        private void LoadConfig(string filePath)
        {
            if (Program.LoadConfig(filePath))
            {
                Program.ConfigFilePath = filePath;
                Program.Config.Folders.ForEach(x => lbFolders.Items.Add(x));
                UpdateWindowUI(Path.GetFileName(filePath));
            }
        }

        private void UpdateWindowUI(string configName = Program.ConfigNewFileName)
        {
            Title = $"TreeGUI - {configName}";
            miToolsConfig.Header = $"{configName} Properties...";
            btnMoveUp.IsEnabled = btnMoveDown.IsEnabled = lbFolders.Items.Count > 1;
            miFolderOpenDir.IsEnabled = lbFolders.SelectedIndex > -1;
            if (miFolderOpenDir.IsEnabled)
            {
                miFolderOpenDir.Header = $"Browse {Path.GetFileName(lbFolders.SelectedItem.ToString())}...";
            }
            miFolderOpenOutputDir.IsEnabled = Directory.Exists(Program.Config.OutputDirectory);
        }

        private async void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = await IsConfigNotSaved();

            Program.SaveSettings();
        }

        private async Task<bool> IsConfigNotSaved()
        {
            if (Program.ConfigEdited)
            {
                CustomMessageBox messageBox = new CustomMessageBox($"Do you want to save changes to {Program.ConfigFileName}?", "Yes", "No");
                string result = await DialogHost.Show(messageBox) as string;
                if (result.Equals("1", StringComparison.InvariantCultureIgnoreCase))
                {
                    return !Program.SaveConfig();
                }
            }

            return false;
        }

        #region File menu

        private void FileNewCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private async void FileNewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await IsConfigNotSaved())
            {
                Program.LoadNewConfig();
                lbFolders.Items.Clear();
                UpdateWindowUI();
            }
        }

        private async void FileOpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await IsConfigNotSaved())
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = Program.ConfigFileFilter;
                if (dlg.ShowDialog() == true)
                {
                    LoadConfig(dlg.FileName);
                }
            }
        }

        private void FileOpenCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void FileExitCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void FileSaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void FileSaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Program.SaveConfig();
        }

        private void FileSaveAs_Click(object sender, RoutedEventArgs e)
        {
            Program.SaveAsConfig();
        }

        private void FileExitCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion File menu

        #region Folders menu

        private void miFolderOpenDir_Click(object sender, RoutedEventArgs e)
        {
            if (lbFolders.SelectedIndex > -1)
            {
                string dir = lbFolders.SelectedItem.ToString();
                Helpers.OpenFolder(dir);
            }
        }

        private void miFolderOpenOutputDir_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(Program.Config.OutputDirectory))
            {
                Helpers.OpenFolder(Program.Config.OutputDirectory);
            }
        }

        #endregion Folders menu

        #region Tools menu

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

        #endregion Tools menu

        #region Help menu

        private void HelpVersionHistory_Click(object sender, RoutedEventArgs e)
        {
        }

        private void HelpAbout_Click(object sender, RoutedEventArgs e)
        {
        }

        #endregion Help menu

        #region Buttons

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (CommonFileDialog.IsPlatformSupported)
            {
                CommonOpenFileDialog dlg = new CommonOpenFileDialog();
                dlg.EnsureReadOnly = true;
                dlg.IsFolderPicker = true;
                dlg.AllowNonFileSystemItems = false;
                dlg.Multiselect = true;
                dlg.Title = "Select folder to index";

                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    foreach (string filename in dlg.FileNames)
                    {
                        lbFolders.Items.Add(filename);
                        Program.Config.Folders.Add(filename);
                    }

                    Program.ConfigEdited = true;
                }
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Program.ConfigEdited = lbFolders.SelectedItems.Count > 0;

            lbFolders.SelectedItems.Cast<string>().ToList().ForEach(x =>
            {
                Program.Config.Folders.Remove(x);
                lbFolders.Items.Remove(x);
            });
        }

        private void btnMoveUp_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnMoveDown_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnIndex_Click(object sender, RoutedEventArgs e)
        {
            IndexerHelper.Index(Program.Config);
        }

        #endregion Buttons

        private void lbFolders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateWindowUI();
        }
    }
}
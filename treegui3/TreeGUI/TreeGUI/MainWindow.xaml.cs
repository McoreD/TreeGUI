using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using ShareX.HelpersLib;
using ShareX.IndexerLib;
using System;
using System.Collections.Generic;
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
            UpdateWindowUI();
        }

        private void UpdateWindowUI(string configName = Program.ConfigNewFileName)
        {
            Title = $"TreeGUI - {configName}";
            miToolsConfig.Header = $"{configName} Properties...";
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
            if (!IsConfigNotSaved())
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = Program.ConfigFileFilter;
                if (dlg.ShowDialog() == true)
                {
                    LoadConfig(dlg.FileName);
                }
            }
        }

        private void LoadConfig(string filePath)
        {
            if (Program.LoadConfig(filePath))
            {
                Program.ConfigFilePath = filePath;
                Program.Config.Folders.ForEach(x => listBoxFolders.Items.Add(x));
                UpdateWindowUI(Path.GetFileName(filePath));
            }
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
            e.Cancel = IsConfigNotSaved();

            Program.SaveSettings();
        }

        private bool IsConfigNotSaved()
        {
            if (Program.ConfigEdited)
            {
                MessageBoxResult result = MessageBox.Show($"Do you want to save changes to {Program.ConfigFileName}?", "TreeGUI", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    return !Program.SaveConfig();
                else
                    return false;
            }

            return false;
        }

        private void HelpVersionHistory_Click(object sender, RoutedEventArgs e)
        {
        }

        private void HelpAbout_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (CommonFileDialog.IsPlatformSupported)
            {
                var dlg = new CommonOpenFileDialog();
                dlg.EnsureReadOnly = true;
                dlg.IsFolderPicker = true;
                dlg.AllowNonFileSystemItems = false;
                dlg.Multiselect = false;
                dlg.Title = "Select folder to index";
                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    listBoxFolders.Items.Add(dlg.FileName);
                    Program.Config.Folders.Add(dlg.FileName);
                    Program.ConfigEdited = true;
                }
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Program.ConfigEdited = listBoxFolders.SelectedItems.Count > 0;

            listBoxFolders.SelectedItems.Cast<string>().ToList<string>().ForEach(x =>
            {
                Program.Config.Folders.Remove(x);
                listBoxFolders.Items.Remove(x);
            });
        }

        private void btnIndex_Click(object sender, RoutedEventArgs e)
        {
            Program.Config.Folders.ForEach(x =>
            {
                string index = Indexer.Index(x, Program.Config.IndexerSettings);
                if (!string.IsNullOrEmpty(index))
                {
                    string dir = Program.Config.OutputMode == OutputMode.CustomDirectory ? Program.Config.OutputDirectory : x;
                    string fileName = Helpers.GetValidFileName($"{x} {Program.Config.FileName}.{Program.Config.IndexerSettings.Output.ToString().ToLower()}", " ");
                    string filePath = Helpers.GetUniqueFilePath(Path.Combine(dir, fileName));

                    if (Directory.Exists(dir))
                    {
                        using (StreamWriter sw = new StreamWriter(filePath))
                        {
                            sw.Write(index);
                        }
                    }
                }
            });
        }

        private void btnMoveUp_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnMoveDown_Click(object sender, RoutedEventArgs e)
        {
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

        private void FileNewCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void FileNewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!IsConfigNotSaved())
            {
                Program.LoadNewConfig();
                listBoxFolders.Items.Clear();
                UpdateWindowUI();
            }
        }
    }
}
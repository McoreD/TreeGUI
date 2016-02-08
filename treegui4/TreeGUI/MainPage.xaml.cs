using HelpersLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TreeLib;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TreeGUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            AppHelper.DefaultPersonalFolder = ApplicationData.Current.RoamingFolder.Path;
            DebugHelper.Init(AppHelper.LogsAppFilePath);
            AppHelper.LoadSettings();

            btnAdd.Click += new RoutedEventHandler(btnAdd_Click);
        }

        private async void LoadConfig(string filePath)
        {
            bool success = await AppHelper.LoadConfigAsync(filePath);

            if (success)
            {
                AppHelper.ConfigFilePath = filePath;
                lbFolders.Items.Clear();
                AppHelper.Config.Folders.ForEach(x => lbFolders.Items.Add(x));
                UpdateWindowUI();
            }
            else
            {
                // RecentFileList.RemoveFile(filePath);
            }
        }

        private void UpdateWindowUI()
        {
            Task.Run(() =>
            {
                string configName = File.Exists(AppHelper.ConfigFilePath) ? Path.GetFileName(AppHelper.ConfigFilePath) : AppHelper.ConfigNewFileName;
            });

            btnMoveUp.IsEnabled = btnMoveDown.IsEnabled = lbFolders.Items.Count > 1;
            btnIndex.IsEnabled = lbFolders.Items.Count > 0;
        }

        public Task<bool> SaveConfig()
        {
            if (!AppHelper.ConfigEdited) return null;

            if (!File.Exists(AppHelper.ConfigFilePath))
            {
                return SaveAsConfig();
            }
            else
            {
                AppHelper.Config.SaveAsync(AppHelper.ConfigFilePath);
                return null;
            }
        }

        public async Task<bool> SaveAsConfig()
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("TreeGUI config files (*.tgcj)", new List<string>() { ".tgcj" });
            savePicker.SuggestedFileName = AppHelper.ConfigNewFileName;

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                AppHelper.ConfigFilePath = file.Path;
                await AppHelper.Config.SaveAsync(AppHelper.ConfigFilePath);
                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                return status == FileUpdateStatus.Complete;
            }

            return false;
        }

        #region Buttons

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            FolderPicker dlg = new FolderPicker() { CommitButtonText = "Add" };
            dlg.SuggestedStartLocation = PickerLocationId.Desktop;
            dlg.FileTypeFilter.Add("*");
            StorageFolder folder = await dlg.PickSingleFolderAsync();
            if (folder != null)
            {
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                lbFolders.Items.Add(folder.Path);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnMoveUp_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnMoveDown_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void btnIndex_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog dlg = new MessageDialog(AppInfo.Version);
            dlg.Commands.Add(new UICommand("Ok") { Id = 0 });
            var result = await dlg.ShowAsync();
        }

        #endregion Buttons

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            AppHelper.SaveSettings();
        }

        private void appBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage));
        }

        private async void btnConfigOpen_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".tgcj");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFileToken", file);
                LoadConfig(file.Path);
            }
        }
    }
}
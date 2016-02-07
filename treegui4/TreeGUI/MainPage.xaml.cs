using HelpersLib;
using System;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
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
            btnAdd.Click += new RoutedEventHandler(btnAdd_Click);
        }

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
    }
}
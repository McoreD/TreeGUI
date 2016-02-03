using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TreeGUI
{
    public class RecentFileList : Separator
    {
        public IPersist Persister { get; set; }

        public int MaxNumberOfFiles { get; set; }
        public int MaxPathLength { get; set; }
        public MenuItem FileMenu { get; private set; }

        /// <summary>
        /// Used in: String.Format( MenuItemFormat, index, filepath, displayPath );
        /// Default = "_{0}:  {2}"
        /// </summary>
        public string MenuItemFormatOneToNine { get; set; }

        /// <summary>
        /// Used in: String.Format( MenuItemFormat, index, filepath, displayPath );
        /// Default = "{0}:  {2}"
        /// </summary>
        public string MenuItemFormatTenPlus { get; set; }

        public delegate string GetMenuItemTextDelegate(int index, string filepath);
        public GetMenuItemTextDelegate GetMenuItemTextHandler { get; set; }

        public event EventHandler<MenuClickEventArgs> MenuClick;

        private Separator _Separator = null;
        private List<RecentFile> _RecentFiles = null;

        public RecentFileList()
        {
            Persister = new ListPersister();

            MaxNumberOfFiles = 9;
            MaxPathLength = 50;
            MenuItemFormatOneToNine = "_{0}:  {2}";
            MenuItemFormatTenPlus = "{0}:  {2}";

            this.Loaded += (s, e) => HookFileMenu();
        }

        private void HookFileMenu()
        {
            MenuItem parent = Parent as MenuItem;
            if (parent == null) throw new ApplicationException("Parent must be a MenuItem");

            if (FileMenu == parent) return;

            if (FileMenu != null) FileMenu.SubmenuOpened -= _FileMenu_SubmenuOpened;

            FileMenu = parent;
            FileMenu.SubmenuOpened += _FileMenu_SubmenuOpened;
        }

        public List<string> RecentFiles { get { return Persister.RecentFiles(MaxNumberOfFiles); } }

        public void RemoveFile(string filepath)
        {
            Persister.RemoveFile(filepath, MaxNumberOfFiles);
        }

        public void InsertFile(string filepath)
        {
            Persister.InsertFile(filepath, MaxNumberOfFiles);
        }

        private void _FileMenu_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            SetMenuItems();
        }

        private void SetMenuItems()
        {
            RemoveMenuItems();

            LoadRecentFiles();

            InsertMenuItems();
        }

        private void RemoveMenuItems()
        {
            if (_Separator != null) FileMenu.Items.Remove(_Separator);

            if (_RecentFiles != null)
                foreach (RecentFile r in _RecentFiles)
                    if (r.MenuItem != null)
                        FileMenu.Items.Remove(r.MenuItem);

            _Separator = null;
            _RecentFiles = null;
        }

        private void InsertMenuItems()
        {
            if (_RecentFiles == null) return;
            if (_RecentFiles.Count == 0) return;

            int iMenuItem = FileMenu.Items.IndexOf(this);
            foreach (RecentFile r in _RecentFiles)
            {
                string header = GetMenuItemText(r.Number + 1, r.FilePath, r.DisplayPath);

                r.MenuItem = new MenuItem { Header = header };
                r.MenuItem.Click += MenuItem_Click;

                FileMenu.Items.Insert(++iMenuItem, r.MenuItem);
            }

            _Separator = new Separator();
            FileMenu.Items.Insert(++iMenuItem, _Separator);
        }

        private string GetMenuItemText(int index, string filepath, string displaypath)
        {
            GetMenuItemTextDelegate delegateGetMenuItemText = GetMenuItemTextHandler;
            if (delegateGetMenuItemText != null) return delegateGetMenuItemText(index, filepath);

            string format = (index < 10 ? MenuItemFormatOneToNine : MenuItemFormatTenPlus);

            return String.Format(format, index, filepath, displaypath);
        }

        private void LoadRecentFiles()
        {
            _RecentFiles = LoadRecentFilesCore();
        }

        private List<RecentFile> LoadRecentFilesCore()
        {
            List<string> list = RecentFiles;

            List<RecentFile> files = new List<RecentFile>(list.Count);

            int i = 0;
            foreach (string filepath in list)
                files.Add(new RecentFile(i++, filepath));

            return files;
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;

            OnMenuClick(menuItem);
        }

        protected virtual void OnMenuClick(MenuItem menuItem)
        {
            string filepath = GetFilepath(menuItem);

            if (String.IsNullOrEmpty(filepath)) return;

            EventHandler<MenuClickEventArgs> dMenuClick = MenuClick;
            if (dMenuClick != null) dMenuClick(menuItem, new MenuClickEventArgs(filepath));
        }

        private string GetFilepath(MenuItem menuItem)
        {
            foreach (RecentFile r in _RecentFiles)
                if (r.MenuItem == menuItem)
                    return r.FilePath;

            return String.Empty;
        }
    }
}
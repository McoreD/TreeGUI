using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGUI
{
    public class ListPersister : IPersist
    {
        public List<string> FileList = new List<string>();

        public void InsertFile(string filepath, int max)
        {
            Update(filepath, true, max);
        }

        public void RemoveFile(string filepath, int max)
        {
            Update(filepath, false, max);
        }

        private void Update(string filepath, bool insert, int max)
        {
            if (!insert || Program.Settings.MruFileList.Contains(filepath))
            {
                Program.Settings.MruFileList.Remove(filepath);
            }

            Program.Settings.MruFileList.Insert(0, filepath);

            if (Program.Settings.MruFileList.Count > max)
            {
                Program.Settings.MruFileList.RemoveAt(Program.Settings.MruFileList.Count - 1);
            }
        }

        public List<string> RecentFiles(int max)
        {
            return Program.Settings.MruFileList;
        }
    }
}
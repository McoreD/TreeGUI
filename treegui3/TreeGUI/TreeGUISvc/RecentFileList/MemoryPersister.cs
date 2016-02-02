using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGUI
{
    public class MemoryPersister : IPersist
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
            List<string> old = Program.Settings.MRU2.FileList;

            List<string> list = new List<string>(old.Count + 1);

            if (insert) list.Add(filepath);

            CopyExcluding(old, filepath, list, max);

            Program.Settings.MRU2.FileList = list;
        }

        private void CopyExcluding(List<string> source, string exclude, List<string> target, int max)
        {
            foreach (string s in source)
                if (!String.IsNullOrEmpty(s))
                    if (!s.Equals(exclude, StringComparison.OrdinalIgnoreCase))
                        if (target.Count < max)
                            target.Add(s);
        }

        public List<string> RecentFiles(int max)
        {
            return FileList;
        }
    }
}
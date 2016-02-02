using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TreeGUI
{
    internal class RecentFile
    {
        public int Number = 0;
        public string FilePath { get; set; }

        public string DisplayPath { get { return Path.GetFileName(FilePath); } }

        public MenuItem MenuItem = null;

        public RecentFile(int number, string filepath)
        {
            this.Number = number;
            this.FilePath = filepath;
        }
    }
}
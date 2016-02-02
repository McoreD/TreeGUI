using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGUI
{
    public class MenuClickEventArgs : EventArgs
    {
        public string Filepath { get; private set; }

        public MenuClickEventArgs(string filepath)
        {
            this.Filepath = filepath;
        }
    }
}
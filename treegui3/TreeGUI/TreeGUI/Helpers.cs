using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGUI
{
    public static class Helpers
    {
        public static void OpenFolder(string folderPath)
        {
            if (!string.IsNullOrEmpty(folderPath))
            {
                if (Directory.Exists(folderPath))
                {
                    Process.Start("explorer.exe", folderPath);
                }
                else
                {
                    //  MessageBox.Show(Resources.Helpers_OpenFolder_Folder_not_exist_ + "\r\n" + folderPath, "ShareX", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
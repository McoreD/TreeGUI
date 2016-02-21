using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TreeGUI
{
    public class Settings : SettingsBaseEx<Settings>
    {
        [Category("Indexer Service"), Description("Interval in hours at which settings will be reloaded")]
        public int LoadSettingsHz { get; set; } = 2;

        public bool IsIndexSetInterval { get; set; } = true;

        [Category("Indexer Service"), Description("Interval in hours at which config files will be indexed")]
        public int IndexsHz { get; set; } = 24;

        public bool IsIndexSetTime { get; set; }
        public DateTime IndexTime { get; set; }

        [Category("Indexer Service"), Description("Folder where TreeGUI config files can be saved for the Windows Service to access")]
        public string ConfigFolder { get; set; }

        [Category("App"), Description("Folder where TreeGUI config files can be saved for the Windows Service to access")]
        public bool AlwaysOnTop { get; set; }

        [Category("App")]
        public bool IsDarkTheme { get; set; }
        public string PrimaryColor { get; set; }

        [Browsable(false)]
        public List<string> MruFileList { get; set; } = new List<string>();
    }
}
using ShareX.HelpersLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGUI
{
    public class Settings : SettingsBase<Settings>
    {
        [Category("Indexer Service")]
        public int ProcessPriorityId { get; set; }

        [Category("Indexer Service"), Description("Folder where TreeGUI config files can be saved for the Windows Service to access.")]
        public string ConfigFolder { get; set; }

        [Category("UI")]
        public bool AlwaysOnTop { get; set; }

        [Category("UI")]
        public bool MinimizeToTray { get; set; }

        public Settings()
        {
            this.ApplyDefaultPropertyValues();
        }
    }
}
using System.ComponentModel;

namespace TreeGUI
{
    public class Settings : SettingsBaseEx<Settings>
    {
        [Category("Indexer Service"), Description("Interval in hours at which settings will be reloaded")]
        public int LoadSettingsHz { get; set; } = 2;

        [Category("Indexer Service"), Description("Interval in hours at which config files will be indexed")]
        public int IndexsHz { get; set; } = 24;

        [Category("Indexer Service"), Description("Folder where TreeGUI config files can be saved for the Windows Service to access")]
        public string ConfigFolder { get; set; }

        [Category("App"), Description("Folder where TreeGUI config files can be saved for the Windows Service to access")]
        public bool AlwaysOnTop { get; set; }

        [Category("App")]
        public bool IsDarkTheme { get; set; }
    }
}
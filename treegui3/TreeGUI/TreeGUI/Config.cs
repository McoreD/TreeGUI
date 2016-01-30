using ShareX.HelpersLib;
using ShareX.IndexerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGUI
{
    public class Config : SettingsBase<Config>
    {
        [Browsable(false)]
        public List<string> Folders { get; set; } = new List<string>();

        [Browsable(false)]
        public IndexerSettings IndexerSettings { get; set; } = new IndexerSettings() { BinaryUnits = true };

        [Description("Output mode for index files")]
        public OutputMode OutputMode { get; set; } = OutputMode.SameDirectory;

        [Description("Custom output directory for index files")]
        public string OutputDirectory { get; set; }

        [Description("Index file name")]
        public string FileName { get; set; } = "Index";
    }
}
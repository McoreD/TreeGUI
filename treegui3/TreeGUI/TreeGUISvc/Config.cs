﻿using ShareX.IndexerLib;
using System.Collections.Generic;
using System.ComponentModel;

namespace TreeGUI
{
    public class Config : SettingsBaseEx<Config>
    {
        [Browsable(false)]
        public List<string> Folders { get; set; } = new List<string>();

        [Browsable(false)]
        public IndexerSettings IndexerSettings { get; set; } = new IndexerSettings() { BinaryUnits = true };

        [Description("Output mode for index files")]
        public OutputMode OutputMode { get; set; } = OutputMode.SameDirectory;

        [Description("Custom output directory for index files")]
        public string CustomDirectory { get; set; }

        [Description("Custom subfolders in yyyy-MM-dd format in the custom directory")]
        public bool CustomDirectoryWithSubfolders { get; set; } = true;

        [Description("Index file name")]
        public string FileName { get; set; } = "Index";

        [Description("Prepend date in ISO 8601 format to index file names")]
        public bool PrependDate { get; set; } = true;
    }
}
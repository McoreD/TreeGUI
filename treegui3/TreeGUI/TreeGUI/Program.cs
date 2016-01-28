using ShareX.HelpersLib;
using ShareX.IndexerLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGUI
{
    public static class Program
    {
        public static readonly string DefaultPersonalFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TreeGUI");

        private static string CustomPersonalPath { get; set; }
        public static Settings Settings { get; private set; }
        public static IndexerSettings Config { get; private set; }
        public static string ConfigName
        {
            get;
        } = "Config1";

        public static bool ConfigEdited { get; set; }

        public static string PersonalFolder
        {
            get
            {
                if (!string.IsNullOrEmpty(CustomPersonalPath))
                {
                    return Helpers.ExpandFolderVariables(CustomPersonalPath);
                }

                return DefaultPersonalFolder;
            }
        }

        public static string SettingsFilePath
        {
            get
            {
                return Path.Combine(PersonalFolder, "Settings.json");
            }
        }

        public static object Application { get; private set; }

        public static void LoadProgramSettings()
        {
            Settings = Settings.Load(SettingsFilePath);
            Config = new IndexerSettings();
        }

        public static void SaveSettings()
        {
            if (Settings != null) Settings.Save(SettingsFilePath);
        }

        public static void LoadNewConfig()
        {
            ConfigEdited = false;
            Config = new IndexerSettings();
        }
    }
}
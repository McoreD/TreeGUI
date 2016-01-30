using Microsoft.Win32;
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
        public const string ConfigFileFilter = "TreeGUI config files (*.tgc)|*.tgc";
        public static readonly string DefaultPersonalFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TreeGUI");

        private static string CustomPersonalPath { get; set; }
        public static Settings Settings { get; private set; }
        public static Config Config { get; private set; } = new Config();

        public static string ConfigFilePath { get; set; }
        public const string ConfigNewFileName = "Config1";
        public static string ConfigFileName
        {
            get
            {
                return string.IsNullOrEmpty(ConfigFilePath) ? ConfigNewFileName : Path.GetFileName(ConfigFilePath);
            }
        }

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

        public static void LoadProgramSettings()
        {
            Settings = Settings.Load(SettingsFilePath);
        }

        public static bool LoadConfig(string filePath)
        {
            if (File.Exists(filePath))
            {
                Config = Config.Load(filePath);
                return true;
            }

            return false;
        }

        public static void SaveSettings()
        {
            if (Settings != null) Settings.Save(SettingsFilePath);
        }

        public static bool SaveConfig()
        {
            if (!File.Exists(ConfigFilePath))
            {
                return SaveAsConfig();
            }
            else
            {
                ConfigEdited = false;
                Config.SaveAsync(ConfigFilePath);
                return true;
            }
        }

        public static bool SaveAsConfig()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = ConfigFileFilter;
            if (dlg.ShowDialog() == true)
            {
                ConfigFilePath = dlg.FileName;
                Config.SaveAsync(ConfigFilePath);
                ConfigEdited = false;
                return true;
            }

            return false;
        }

        public static void LoadNewConfig()
        {
            ConfigFilePath = "";
            ConfigEdited = false;
            Config = new Config();
        }
    }
}
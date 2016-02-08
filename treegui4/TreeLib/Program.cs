using HelpersLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeLib
{
    public static class Program
    {
        public const string ConfigFileFilter = "TreeGUI config files (*.tgcj)|*.tgcj";
        public static string DefaultPersonalFolder = null;

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
                    return CustomPersonalPath;
                }

                if (string.IsNullOrEmpty(DefaultPersonalFolder))
                    throw new Exception("DefaultPersonalFolder is empty. Please provide ApplicationData folder.");

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

        public static string LogsAppFilePath
        {
            get
            {
                string logsFolder = Path.Combine(PersonalFolder, "Logs");
                string filename = string.Format("TreeGUI-Log-{0:yyyy-MM}.txt", DateTime.Now);
                return Path.Combine(logsFolder, filename);
            }
        }

        public static string LogsSvcFilePath
        {
            get
            {
                string logsFolder = Path.Combine(PersonalFolder, "Logs");
                string filename = string.Format("TreeGUISvc-Log-{0:yyyy-MM}.txt", DateTime.Now);
                return Path.Combine(logsFolder, filename);
            }
        }

        public static void LoadSettings()
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

        public static void LoadNewConfig()
        {
            ConfigFilePath = "";
            ConfigEdited = false;
            Config = new Config();
        }
    }
}
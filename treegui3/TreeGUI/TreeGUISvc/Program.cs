using Microsoft.Win32;
using ShareX.HelpersLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace TreeGUI
{
    public static class Program
    {
        public const string ConfigFileFilter = "TreeGUI config files (*.tgcj)|*.tgcj";
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

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new TreeGUISvc()
                };
                ServiceBase.Run(ServicesToRun);
            }
            else if (args.Length == 1)
            {
                switch (args[0])
                {
                    case "-install":
                        InstallService();
                        StartService();
                        break;
                    case "-uninstall":
                        StopService();
                        UninstallService();
                        break;
                }
            }
        }

        private static bool IsInstalled()
        {
            using (ServiceController controller = new ServiceController("TreeGUISvc"))
            {
                try
                {
                    ServiceControllerStatus status = controller.Status;
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        private static bool IsRunning()
        {
            using (ServiceController controller = new ServiceController("TreeGUISvc"))
            {
                if (!IsInstalled()) return false;
                return (controller.Status == ServiceControllerStatus.Running);
            }
        }

        private static AssemblyInstaller GetInstaller()
        {
            AssemblyInstaller installer = new AssemblyInstaller(typeof(TreeGUISvc).Assembly, null);
            installer.UseNewContext = true;
            return installer;
        }

        private static void InstallService()
        {
            if (IsInstalled()) return;

            try
            {
                using (AssemblyInstaller installer = GetInstaller())
                {
                    IDictionary state = new Hashtable();
                    try
                    {
                        installer.Install(state);
                        installer.Commit(state);
                    }
                    catch
                    {
                        try
                        {
                            installer.Rollback(state);
                        }
                        catch { }
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private static void UninstallService()
        {
            if (!IsInstalled()) return;
            try
            {
                using (AssemblyInstaller installer = GetInstaller())
                {
                    IDictionary state = new Hashtable();
                    try
                    {
                        installer.Uninstall(state);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private static void StartService()
        {
            if (!IsInstalled()) return;

            using (ServiceController controller = new ServiceController("TreeGUISvc"))
            {
                try
                {
                    if (controller.Status != ServiceControllerStatus.Running)
                    {
                        controller.Start();
                        controller.WaitForStatus(ServiceControllerStatus.Running,
                            TimeSpan.FromSeconds(10));
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        private static void StopService()
        {
            if (!IsInstalled()) return;

            using (ServiceController controller = new ServiceController("TreeGUISvc"))
            {
                try
                {
                    if (controller.Status != ServiceControllerStatus.Stopped)
                    {
                        controller.Stop();
                        controller.WaitForStatus(ServiceControllerStatus.Stopped,
                             TimeSpan.FromSeconds(10));
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
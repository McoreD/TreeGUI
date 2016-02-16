using ShareX.HelpersLib;
using System;
using System.Collections;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;

namespace TreeGUI
{
    public static class Program
    {
        public const string ConfigFileFilter = "TreeGUI config files (*.tgcj)|*.tgcj";
        public static readonly string DefaultPersonalFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TreeGUI");

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
            else
            {
                switch (args[0])
                {
                    case "-install":
                        TreeGUISvc.UserName = args[1];
                        TreeGUISvc.Password = args[2];
                        InstallService();
                        StartService();
                        break;
                    case "-uninstall":
                        StopService();
                        UninstallService();
                        break;
                    case "-start":
                        StartService();
                        break;
                    case "-stop":
                        StopService();
                        break;
                    case "-restart":
                        RestartService();
                        break;
                    case "-index":
                        Index();
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

        public static void StartService()
        {
            if (!IsInstalled()) return;

            using (ServiceController controller = new ServiceController("TreeGUISvc"))
            {
                try
                {
                    if (controller.Status != ServiceControllerStatus.Running)
                    {
                        controller.Start();
                        controller.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        public static void StopService()
        {
            if (!IsInstalled()) return;

            using (ServiceController controller = new ServiceController("TreeGUISvc"))
            {
                try
                {
                    if (controller.Status != ServiceControllerStatus.Stopped)
                    {
                        controller.Stop();
                        controller.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        public static void RestartService()
        {
            if (!IsInstalled()) return;

            StopService();
            StartService();
        }

        public static void Index()
        {
            if (!IsInstalled()) return;

            using (ServiceController controller = new ServiceController("TreeGUISvc"))
            {
                try
                {
                    controller.Refresh();
                    if (controller.Status != ServiceControllerStatus.Running)
                    {
                        controller.Start();
                    }
                    controller.ExecuteCommand((int)ServiceCommand.Index);
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
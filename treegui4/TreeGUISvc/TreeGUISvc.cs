using HelpersLib;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using TreeLib;

namespace TreeGUISvc
{
    public partial class TreeGUISvc : ServiceBase
    {
        private static Timer timerIndexer = new Timer();
        private static Timer timerSettingsReader = new Timer();

        internal static string UserName { get; set; }
        internal static string Password { get; set; }

        public TreeGUISvc()
        {
            InitializeComponent();
            this.EventLog.Log = "Application";
            DebugHelper.Init(TreeLib.Program.LogsSvcFilePath);

            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;
        }

        protected override void OnStart(string[] args)
        {
            WriteLog($"Reading settings from {TreeLib.Program.SettingsFilePath}");

            TreeLib.Program.LoadSettings();
            UpdateTimers();

            timerIndexer.Elapsed += TimerIndexer_Elapsed;
            timerIndexer.Start();

            timerSettingsReader.Elapsed += TimerSettingsReader_Elapsed;
            timerSettingsReader.Start();

            base.OnStart(args);
        }

        private void UpdateTimers()
        {
            timerSettingsReader.Interval = TreeLib.Program.Settings.LoadSettingsHz * 3600 * 1000;
            timerIndexer.Interval = TreeLib.Program.Settings.IndexsHz * 3600 * 1000;
        }

        private void TimerSettingsReader_Elapsed(object sender, ElapsedEventArgs e)
        {
            WriteLog($"Settings reloaded. Working directory: {TreeLib.Program.Settings.ConfigFolder}");
            TreeLib.Program.LoadSettings();
            UpdateTimers();
        }

        private void TimerIndexer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Directory.Exists(TreeLib.Program.Settings.ConfigFolder))
            {
                var configFiles = Directory.GetFiles(TreeLib.Program.Settings.ConfigFolder, "*.tgcj", SearchOption.AllDirectories);
                configFiles.ToList<string>().ForEach(tgcjFile =>
                {
                    try
                    {
                        WriteLog($"Reading {tgcjFile}");
                        IndexerHelper.Index(Config.Load(tgcjFile));
                    }
                    catch (Exception ex)
                    {
                        WriteLog("Indexing", ex);
                    }
                });
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        protected override void OnPause()
        {
            timerIndexer.Enabled = false;
            base.OnPause();
        }

        protected override void OnContinue()
        {
            timerIndexer.Enabled = true;
            base.OnContinue();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
        }

        protected override void OnCustomCommand(int command)
        {
            base.OnCustomCommand(command);
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            return base.OnPowerEvent(powerStatus);
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);
        }

        private void WriteLog(string msg, Exception ex = null)
        {
            if (ex == null)
            {
                DebugHelper.WriteLine(msg);
                EventLog.WriteEntry(msg, EventLogEntryType.Information);
            }
            else
            {
                DebugHelper.WriteException(ex, msg);
                EventLog.WriteEntry(ex.Message + "\n" + ex.StackTrace, EventLogEntryType.Error);
            }
        }
    }
}
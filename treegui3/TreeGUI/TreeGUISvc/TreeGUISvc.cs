using ShareX.HelpersLib;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace TreeGUI
{
    public partial class TreeGUISvc : ServiceBase
    {
        private Timer timerIndexer = new Timer();
        private Timer timerSettingsReader = new Timer();
        private Timer timerTimeOfDay = new Timer() { Interval = 1000 };

        private StringBuilder debug = new StringBuilder();

        internal static string UserName { get; set; }
        internal static string Password { get; set; }

        public TreeGUISvc()
        {
            InitializeComponent();
            this.EventLog.Log = "Application";
            DebugHelper.Init(Program.LogsSvcFilePath);

            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;
        }

        protected override void OnStart(string[] args)
        {
            WriteLog($"Reading settings from {Program.SettingsFilePath}");

            Program.LoadSettings();

            UpdateTimerSettingsReader();

            timerSettingsReader.Elapsed += TimerSettingsReader_Elapsed;
            timerSettingsReader.Start();

            timerIndexer.Interval = Program.Settings.IndexsHz * 3600 * 1000; // everytime interval is updated, count resets
            timerIndexer.Elapsed += TimerIndexer_Elapsed;
            timerIndexer.Start();

            timerTimeOfDay.Elapsed += TimerTimeOfDay_Elapsed;
            timerTimeOfDay.Start();

            base.OnStart(args);
        }

        private void TimerTimeOfDay_Elapsed(object sender, ElapsedEventArgs e)
        {
            //debug.AppendLine(Program.Settings.IsIndexSetTime.ToString());
            //debug.AppendLine($"DateTime.Now.Hour {DateTime.Now.Hour}");
            //debug.AppendLine($"DateTime.Now.Minute {DateTime.Now.Minute}");
            //debug.AppendLine($"Program.Settings.IndexTime.Hour.Hour {Program.Settings.IndexTime.Hour}");
            //debug.AppendLine($"Program.Settings.IndexTime.Hour.Minute {Program.Settings.IndexTime.Minute}");
            //WriteLog(debug.ToString());

            if (Program.Settings.IsIndexSetTime &&
                DateTime.Now.Hour == Program.Settings.IndexTime.Hour &&
                DateTime.Now.Minute == Program.Settings.IndexTime.Minute &&
                DateTime.Now.Second == 0)
            {
                debug.AppendLine($"Index at set time initiated.");
                Index();
            }
        }

        private void UpdateTimerSettingsReader()
        {
            timerSettingsReader.Interval = Program.Settings.LoadSettingsHz * 3600 * 1000;

            debug.AppendLine($"Settings read scheduled for {DateTime.Now.AddHours(Program.Settings.LoadSettingsHz).ToString("yyyy-MM-dd HH:mm")}.");
            debug.AppendLine($"Indexing every {Program.Settings.IndexsHz} hours.");

            WriteLog(debug.ToString());
        }

        private void TimerSettingsReader_Elapsed(object sender, ElapsedEventArgs e)
        {
            Program.LoadSettings();
            debug.AppendLine($"Working directory: {Program.Settings.ConfigFolder}");

            UpdateTimerSettingsReader();
        }

        private void TimerIndexer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Index();
        }

        private void Index()
        {
            if (Directory.Exists(Program.Settings.ConfigFolder))
            {
                var configFiles = Directory.GetFiles(Program.Settings.ConfigFolder, "*.tgcj", SearchOption.AllDirectories);
                configFiles.ToList<string>().ForEach(tgcjFile =>
                {
                    try
                    {
                        debug.AppendLine($"Reading {tgcjFile}");
                        IndexerHelper.Index(Config.Load(tgcjFile));
                    }
                    catch (Exception ex)
                    {
                        WriteLog("Indexing error", ex);
                    }
                });

                WriteLog(debug.ToString());
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
            switch (command)
            {
                case (int)ServiceCommand.Index:
                    Index();
                    break;
            }
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

            debug.Clear();
        }
    }
}
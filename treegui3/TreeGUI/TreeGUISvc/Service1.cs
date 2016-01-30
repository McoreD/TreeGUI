using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TreeGUI
{
    public partial class TreeGUISvc : ServiceBase
    {
        private static Timer timerIndexer = new Timer() { Interval = 24 * 3600 * 1000 };
        private static Timer timerSettingsReader = new Timer() { Interval = 1 * 3600 * 1000 };

        public TreeGUISvc()
        {
            InitializeComponent();
            this.EventLog.Log = "Application";

            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;
        }

        protected override void OnStart(string[] args)
        {
            timerIndexer.Elapsed += TimerIndexer_Elapsed;
            timerSettingsReader.Elapsed += TimerSettingsReader_Elapsed;
            base.OnStart(args);
        }

        private void TimerSettingsReader_Elapsed(object sender, ElapsedEventArgs e)
        {
            EventLog.WriteEntry($"Settings reloaded. Working directory: {Program.Settings.ConfigFolder}");
            Program.LoadProgramSettings();
        }

        private void TimerIndexer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Directory.Exists(Program.Settings.ConfigFolder))
            {
                var configFiles = Directory.GetFiles(Program.Settings.ConfigFolder, "*.tgcj", SearchOption.AllDirectories);
                configFiles.ToList<string>().ForEach(tgcjFile =>
                {
                    EventLog.WriteEntry($"Reading {tgcjFile}");
                    IndexerHelper.Index(Config.Load(tgcjFile));
                });
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        protected override void OnContinue()
        {
            base.OnContinue();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
        }

        protected override void OnCustomCommand(int command)
        {
            //  A custom command can be sent to a service by using this method:
            //#  int command = 128; //Some Arbitrary number between 128 & 256
            //#  ServiceController sc = new ServiceController("NameOfService");
            //#  sc.ExecuteCommand(command);

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
    }
}
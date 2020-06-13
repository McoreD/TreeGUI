using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;

namespace TreeGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private JumpList JumpList = new JumpList() { ShowRecentCategory = true };
        public static List<string> LoadedAssemblies = new List<string>();

        protected override void OnStartup(StartupEventArgs e)
        {
            JumpList.SetJumpList(Application.Current, JumpList);

            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;

            base.OnStartup(e);
        }

        private void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            LoadedAssemblies.Add(Path.GetFileName(args.LoadedAssembly.Location));
        }

        public static void ApplyTheme(bool isDarkTheme)
        {
            // new PaletteHelper().SetLightDark(isDarkTheme);
        }

        public static void ApplyPrimaryColor()
        {
            // if (!string.IsNullOrEmpty(Program.Settings.PrimaryColor))
            //     new PaletteHelper().ReplacePrimaryColor(Program.Settings.PrimaryColor);
        }
    }
}
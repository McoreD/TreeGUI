using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            JumpList.SetJumpList(Application.Current, JumpList);

            base.OnStartup(e);
        }
    }
}
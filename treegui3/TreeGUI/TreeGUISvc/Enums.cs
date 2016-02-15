using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGUI
{
    public enum OutputMode
    {
        [Description("Create index file in the same directory")]
        SameDirectory,
        [Description("Create index file in a custom directory")]
        CustomDirectory
    }

    public enum ServiceCommand
    {
        Index = 128
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.OS
{
    public static class OSInfo
    {
        public static bool IsWindows10
        {
            get
            {
                return Environment.OSVersion.Version.Major == 10;
            }
        }
    }
}

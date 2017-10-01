using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Helper
{
    public static class SystemHelper
    {
        public static bool IsWin10
        {
            get
            {
                return Environment.OSVersion.Version.Major == 10;
            }
        }
    }
}

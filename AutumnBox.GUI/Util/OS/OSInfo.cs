using System;

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

using AutumnBox.Leafx.Container.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(IBuildInfo))]
    class BuildInfo : IBuildInfo
    {
        private const string FILE_NAME = "./build.ini";

        public string LatestCommit => Read("commit") ?? "unknown";

        public string DateTime => Read("datetime") ?? "unknown";

        public string LatestTag => Read("tag") ?? "unknown";

        public string Version => Read("version") ?? "unknown";

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def,
                    StringBuilder retVal, int size, string filePath);

        private static string? Read(string key)
        {
            const int BUFFER_SIZE = 255;
            StringBuilder sb = new StringBuilder(BUFFER_SIZE);
            _ = GetPrivateProfileString("Build", key, "", sb, BUFFER_SIZE, FILE_NAME);
            return sb.ToString();
        }

    }
}

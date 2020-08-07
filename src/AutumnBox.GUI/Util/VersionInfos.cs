using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Management.ExtLibrary.Impl;
using System;
using System.Linq;
using AutumnBox.Leafx.Container;
using System.Reflection;
using System.Text.RegularExpressions;
using AutumnBox.Basic;
using AutumnBox.Basic.Calling;

namespace AutumnBox.GUI.Util
{
    static class VersionInfos
    {
        public static Version Basic { get; }

        public static string BasicString => Basic.ToString();

        public static Version GUI { get; }
        public static string GUIString => GUI.ToString();

        public static Version OpenFx { get; }
        public static string OpenFxString => OpenFx.ToString();

        public static Version Logging { get; }
        public static string LoggingString => Logging.ToString();

        public static string Adb { get; }

        public static Version CoreLib { get; }
        public static string CoreLibString => CoreLib.ToString();

        public static Version DotNetFramework { get; }
        public static string DotNetFrameworkString => DotNetFramework.ToString();

        public static Version OS { get; }
        public static string OSString => OS.ToString();

        public static Version HandyControl { get; }
        public static string HandyControlString => HandyControl.ToString();

        public static Version SharpZipLib { get; }
        public static string SharpZipLibString => SharpZipLib.ToString();

        private static string GetAdbVersion()
        {
            using var cmd = BasicBooter.CommandProcedureManager.OpenADBCommand(null, "version");
            string versionOutput = cmd.Execute().Output;
            var match = Regex.Match(versionOutput, @"[\w|\s]*[version\s](?<name>[\d|\.]+)([\r\n|\n]*)Version\s(?<code>\d+)", RegexOptions.Multiline);
            if (match.Success)
            {
                return match.Result("${name}(${code})");
            }
            else
            {
                return "unknown";
            }
        }

        static VersionInfos()
        {
            Basic = AutumnBox.Basic.ModuleInfo.Version;
            GUI = Self.Version;
            OpenFx = AutumnBox.OpenFramework.BuildInfo.SDK_VERSION;
            Logging = typeof(Logging.SLogger).Assembly.GetName().Version;

            HandyControl = typeof(HandyControl.Controls.AnimationPath).Assembly.GetName().Version;
            SharpZipLib = typeof(ICSharpCode.SharpZipLib.SharpZipBaseException).Assembly.GetName().Version;

            DotNetFramework = Environment.Version;
            OS = Environment.OSVersion.Version;
            Adb = GetAdbVersion();
        }
    }
}
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.OpenFramework.ExtLibrary;
using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AutumnBox.GUI.Util
{
    static class VersionInformation
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

        public static Version JsonLib { get; }
        public static string JsonLibString => JsonLib.ToString();

        public static Version HandyControl { get; }
        public static string HandyControlString => HandyControl.ToString();

        public static Version SharpZipLib { get; }
        public static string SharpZipLibString => SharpZipLib.ToString();

        private static string GetAdbVersion()
        {
            string versionOutput = new AdbCommand("version").Execute().Output;
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
        private static Version GetCoreLibVersion()
        {
            var coreLibFilterResult = from lib in OpenFramework.Management.OpenFx.LibsManager.Librarians
                                      where lib.Name == "AutumnBox Core Modules"
                                      select lib;
            if (coreLibFilterResult.Count() == 0) return new Version(0, 0, 1);
            var assemblyLib = coreLibFilterResult.First() as AssemblyBasedLibrarian;
            Assembly assembly = assemblyLib.ManagedAssembly;
            return assembly.GetName().Version;
        }

        static VersionInformation()
        {
            Basic = typeof(Basic.ManagedAdb.LocalAdbServer).Assembly.GetName().Version;
            GUI = Self.Version;
            OpenFx = typeof(OpenFramework.BuildInfo).Assembly.GetName().Version;
            Logging = typeof(Logging.SLogger).Assembly.GetName().Version;

            JsonLib = typeof(Newtonsoft.Json.JsonException).Assembly.GetName().Version;
            HandyControl = typeof(HandyControl.Controls.AnimationPath).Assembly.GetName().Version;
            SharpZipLib = typeof(ICSharpCode.SharpZipLib.SharpZipBaseException).Assembly.GetName().Version;

            DotNetFramework = Environment.Version;
            OS = Environment.OSVersion.Version;
            Adb = GetAdbVersion();
            CoreLib = GetCoreLibVersion();
        }
    }
}
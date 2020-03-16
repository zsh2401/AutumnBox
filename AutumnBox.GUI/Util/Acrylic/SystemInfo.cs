/*

* ==============================================================================
*
* Filename: SystemInfo
* Description: 
*
* Version: 1.0
* Created: 2020/3/13 23:13:34
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.GUI.Model;
using System;

namespace AutumnBox.GUI.Util.OS.Acrylic
{
    class SystemInfo
    {
        public static Lazy<Version> Version { get; private set; } = new Lazy<Version>(() => GetVersionInfo());

        internal static Version GetVersionInfo()
        {
            var regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\", false);
            // キーが存在しないときはnullが返る
            if (regkey == null) return default(Version);

            // Windows10以降は、以下のレジストリ値でOSバージョンを判断する
            var majorValue = regkey.GetValue("CurrentMajorVersionNumber");
            var minorValue = regkey.GetValue("CurrentMinorVersionNumber");
            var buildValue = (string)regkey.GetValue("CurrentBuild", 7600);
            var canReadBuild = int.TryParse(buildValue, out var build);

            // Windows10用のレジストリ値が取れない場合は以下の値を使う
            // ※この方法だと、Windows8/8.1の区別がつかない。
            var defaultVersion = System.Environment.OSVersion.Version;

            if (majorValue is int major && minorValue is int minor && canReadBuild)
            {
                return new Version(major, minor, build);
            }
            else
            {
                return new Version(defaultVersion.Major, defaultVersion.Minor, defaultVersion.Revision);
            }
        }

        /// <summary>
        /// 実行環境のOSがWindows10か否かを判定
        /// </summary>
        /// <returns></returns>
        internal static bool IsWin10()
        {
            return Version.Value.Major == 10;
        }


        internal static bool IsWin7()
        {
            return Version.Value.Major == 6 && Version.Value.Minor == 1;
        }

        internal static bool IsWin8x()
        {
            return Version.Value.Major == 6 && (Version.Value.Minor == 2 || Version.Value.Minor == 3);
        }
    }
}

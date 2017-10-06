/* =============================================================================*\
*
* Filename: VersionInfo.cs
* Description: 
*
* Version: 1.0
* Created: 8/16/2017 00:27:56(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;

namespace AutumnBox.Util
{
    public struct VersionInfo
    {
        public string content;
        public int build;
        public string version;
        public DateTime time;
        public string githubReleaseDownloadUrl;
        public string baiduPanDownloadUrl;
    }
}

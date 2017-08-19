using AutumnBox.Util;
using System;

namespace AutumnBox
{
    internal static class StaticData
    {
        public static VersionInfo nowVersion
        {
            get
            {
                return new VersionInfo
                {
                    content = "前期测试版本,修复大量bug",
                    build = 4,//已弃用
                    version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                    time = new DateTime(2017, 8, 19),
                    baiduPanDownloadUrl =  "https://pan.baidu.com/s/1bFZBAI",
                    githubReleaseDownloadUrl = "https://github.com/zsh2401/AutumnBox/releases/tag/0.12.8"
                };
            }
        }
    }
}
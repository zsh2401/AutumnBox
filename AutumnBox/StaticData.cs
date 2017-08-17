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
                    content = "",
                    build = 3,
                    version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                    time = new DateTime(2017, 8, 18)
                };
            }
        }
    }
}
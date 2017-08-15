using AutumnBox.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                    build = 1,
                    version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                    time = new DateTime(2017, 8, 16)
                };
            }
        }
    }
}
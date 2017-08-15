using AutumnBox.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    content = "初号机",
                    build = 1,
                    version = "0.11.0",
                    time = new DateTime(2017, 8, 16)
                };
            }
        }
    }
}
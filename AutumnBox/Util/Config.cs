using AutumnBox.Debug;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Util
{
    internal static class Config
    {
        internal static bool isShowSideloadTur
        {
            set { new ConfigSql().Set("boolValues", "isShowSideloadTur", value); }
            get { return Convert.ToBoolean(new ConfigSql().Read("boolValues", "isShowSideloadTur")); }
        }
        internal static string language
        {
            set { new ConfigSql().Set("stringValues", "language", value); }
            get { return new ConfigSql().Read("stringValues", "language").ToString(); }
        }
        internal static int skipBuild
        {
            set { new ConfigSql().Set("intValues", "skipBuild", value); }
            get { return int.Parse(new ConfigSql().Read("intValues", "skipBuild").ToString()); }
        }
        internal static bool isFristLaunch
        {
            set { new ConfigSql().Set("boolValues", "isFristLaunch", value); }
            get { return bool.Parse(new ConfigSql().Read("boolValues", "isFristLaunch").ToString()); }
        }
    }
}

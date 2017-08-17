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
        internal static string skipVersion
        {
            set { new ConfigSql().Set("stringValues", "skipVersion", value); }
            get { return new ConfigSql().Read("stringValues", "skipVersion").ToString(); }
        }
        internal static bool isFristLaunch
        {
            set { new ConfigSql().Set("boolValues", "isFristLaunch", value); }
            get { return bool.Parse(new ConfigSql().Read("boolValues", "isFristLaunch").ToString()); }
        }
    }
}

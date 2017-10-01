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
    public static class Config
    {
        private static ConfigJson jConfig = new ConfigJson();
        public static bool IsFirstLaunch { get {
                return Convert.ToBoolean(jConfig.SourceData["IsFirstLaunch"]);
            } set {
                jConfig.SourceData["IsFirstLaunch"] = value;
                jConfig.Save();
            } }
        public static string SkipVersion { get {
                return jConfig["SkipVersion"].ToString();
            } set {
                jConfig.SourceData["SkipVersion"] = value;
                jConfig.Save();
            } }
        public static string Lang
        {
            get
            {
                return jConfig["Lang"].ToString();
            }
            set
            {
                jConfig.SourceData["Lang"] = value;
                jConfig.Save();
            }
        }
    }
}

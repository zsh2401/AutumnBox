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
        private static JConfig jConfig = new JConfig();
        public static bool IsFirstLaunch { get {
                return Convert.ToBoolean(jConfig["IsFirstLaunch"]);
            } set {
                jConfig["IsFirstLaunch"] = value;
            } }
        public static string SkipVersion { get {
                return jConfig["SkipVersion"].ToString();
            } set {
                jConfig["SkipVersion"] = value;
            } }
    }
}

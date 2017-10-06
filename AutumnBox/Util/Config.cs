/* =============================================================================*\
*
* Filename: Config.cs
* Description: 
*
* Version: 1.0
* Created: 8/4/2017 13:34:50(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;

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

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
using Newtonsoft.Json;
using System;

namespace AutumnBox.GUI.Util
{
    public static class Config
    {
        private static ConfigJson _JConfig = new ConfigJson();
        public static bool IsFirstLaunch
        {
            get
            {
                return _JConfig.IsFirstLaunch;
            }
            set
            {
                _JConfig.IsFirstLaunch = value;
                _JConfig.SaveToDisk();
            }
        }
        public static string SkipVersion
        {
            get
            {
                return _JConfig.SkipVersion;
            }
            set
            {
                _JConfig.SkipVersion = value;
                _JConfig.SaveToDisk();
            }
        }
        public static string Lang
        {
            get
            {
                return _JConfig.Lang;
            }
            set
            {
                _JConfig.Lang = value;
                _JConfig.SaveToDisk();
            }
        }
    }
}

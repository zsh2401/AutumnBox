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
using AutumnBox.Shared.CstmDebug;

namespace AutumnBox.GUI.Cfg
{
    public static class Config
    {
        private static IConfigOperator Operator = new ConfigOperator();
        public static bool IsFirstLaunch
        {
            get
            {
                Logger.D("Config", "Get Is firstLaunch value " + Operator.Data.IsFirstLaunch);
                return Operator.Data.IsFirstLaunch;
            }
            set
            {
                Operator.Data.IsFirstLaunch = value;
                Logger.D("Config", "ifl set, now value " + Operator.Data.IsFirstLaunch.ToString());
                Operator.SaveToDisk();
            }
        }
        public static string SkipVersion
        {
            get
            {
                return Operator.Data.SkipVersion;
            }
            set
            {
                Operator.Data.SkipVersion = value;
                Operator.SaveToDisk();
            }
        }
        public static string Lang
        {
            get
            {
                return Operator.Data.Lang;
            }
            set
            {
                Operator.Data.Lang = value;
                Operator.SaveToDisk();
            }
        }
    }
}

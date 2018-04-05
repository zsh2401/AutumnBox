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

namespace AutumnBox.GUI.Cfg
{
    [Obsolete("已使用Settings代替AutumnBox.GUI.Cfg,此处代码现在仅供参考")]
    internal static class Config
    {
        public static IConfigOperator Operator = new ConfigOperator();
        public static bool IsFirstLaunch
        {
            get
            {
                return Operator.Data.IsFirstLaunch;
            }
            set
            {
                Operator.Data.IsFirstLaunch = value;
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
            }
        }
        public static byte BackgroundA
        {
            get
            {
                return Operator.Data.BackgroundA;
            }
            set
            {
                Operator.Data.BackgroundA = value;
            }
        }
        public static bool ShowDebugWindowOnNextLaunch
        {
            get { return Operator.Data.ShowDebugWindowOnNextLaunch; }
            set { Operator.Data.ShowDebugWindowOnNextLaunch = value; }
        }
    }
}

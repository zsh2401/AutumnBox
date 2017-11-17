/* =============================================================================*\
*
* Filename: DeviceImagePathFinder
* Description: 
*
* Version: 1.0
* Created: 2017/11/16 21:58:39 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Devices
{
    public enum Images
    {
        Boot,
        Recovery
    }
    public static class DeviceImageHelper
    {
        private static bool CheckPath(AndroidShell shellAsSu, string path)
        {
            return shellAsSu.SafetyInput($"ls -l {path}").IsSuccess;
        }
        public static string Find(AndroidShell shellAsSu, Images img)
        {
            var output = shellAsSu.SafetyInput($"find /dev/ -name {img.ToString().ToLower()}");
            string path = $"/dev/block/platform/*/by-name/{img.ToString().ToLower()}";
            if (output.IsSuccess && output.LineAll.Count > 0)
            {
                path = output.LineAll[output.LineAll.Count - 1];
            }
            Logger.D($"the path is {path}");
            return (CheckPath(shellAsSu, path)) ? path : null;
        }
        public static string FindById(string id, Images img)
        {
            using (AndroidShell su = new AndroidShell(id))
            {
                su.Connect();
                su.Switch2Su();
                return Find(su, img);
            }
        }
    }
}

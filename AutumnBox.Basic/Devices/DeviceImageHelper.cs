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
        private static bool CheckPath(AndroidShell shellAsSu,string path) {
            throw new NotImplementedException();
            shellAsSu.SafetyInput($"ls -l {path}");
            return true; }
        public static string FindByShell(AndroidShell shell, Images img)
        {
            throw new NotImplementedException();
            if (!shell.IsConnect) shell.Connect();
            shell.Switch2Superuser();
            bool exeResult = shell.SafetyInput($"find /dev/ -name {img.ToString().ToLower()}");
            if (exeResult)
            {
                return shell.LatestLineOutput;
            }
            return "/dev/block/platform/*/by-name";
        }
        public static string FindById(string id, Images img)
        {
            throw new NotImplementedException();
        }
    }
}

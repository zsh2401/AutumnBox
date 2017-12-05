/* =============================================================================*\
*
* Filename: DeviceImageHelper
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Devices
{
    public enum Images
    {
        Boot,
        Recovery
    }
    /// <summary>
    /// 有关设备镜像的帮助类
    /// </summary>
    public static class DeviceImageHelper
    {
        private static bool CheckPath(AndroidShell shellAsSu, string path)
        {
            return shellAsSu.SafetyInput($"ls -l {path}").IsSuccess;
        }
        public static string Find(AndroidShell shellAsSu, Images img)
        {
            Logger.D("try to find image path use find-command");
            var output = shellAsSu.SafetyInput($"find /dev/ -name {img.ToString().ToLower()}");
            string path = $"/dev/block/platform/*/by-name/{img.ToString().ToLower()}";
            if (output.IsSuccess && output.LineAll.Count > 0)
            {
                Logger.D("image path was finded by find-command");
                path = output.LineAll[output.LineAll.Count - 1];
            }
            var _output = shellAsSu.SafetyInput($"ls -l {path}");
            if (_output.IsSuccess)
            {
                var match = Regex.Match(_output.LineAll.Last(), @"->.{1}(?<path>.+)");
                if (match.Success) {
                    return match.Result("${path}");
                }
            }
            return null;
        }
        public static string FindById(string id, Images img)
        {
            using (AndroidShell su = new AndroidShell(id))
            {
                su.Connect();
                if (!su.Switch2Su()) { throw new Exception("su not found,device have no root access maybe"); }
                return Find(su, img);
            }
        }
    }
}

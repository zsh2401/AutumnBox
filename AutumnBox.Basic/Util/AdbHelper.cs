/* =============================================================================*\
*
* Filename: AdbHelper
* Description: 
*
* Version: 1.0
* Created: 2017/11/21 0:03:28 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.Support.CstmDebug;
using AutumnBox.Support.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Util
{
    public static class AdbHelper
    {
        public static bool HaveOtherAdbProcess()
        {
            return Process.GetProcessesByName("adb").Length != 0;
        }
        public static void KillAllAdbProcess()
        {
            Process[] processs = Process.GetProcessesByName("adb");
            foreach (var p in processs)
            {
                SystemHelper.KillProcessAndChildrens(p.Id);
            }
        }
        public static bool Check()
        {
            if (!HaveOtherAdbProcess())
            {
                return StartServer();
            }
            return true;
        }
        public static bool StartServer()
        {
            OutputData o = new OutputData();
            using (ABProcess process = new ABProcess())
            {
                o = process.RunToExited("cmd.exe", "/c " + ConstData.ADB_PATH + " start-server & echo %errorlevel%");
            }
            Logger.D("the fuck output of adb start-server ->" + o.ToString());
            return !o.All.ToString().Contains("cannot connect to daemon");
        }
        public static bool StopServer()
        {
            OutputData o = new OutputData();
            using (ABProcess process = new ABProcess())
            {
                o = process.RunToExited("cmd.exe", "/c " + ConstData.ADB_PATH + " stop-server & echo %errorlevel%");
            }
            return o.LineAll.Last() == "0";
        }
        public static bool RestartServer()
        {
            StopServer();
            return StartServer();
        }
    }
}

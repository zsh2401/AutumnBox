/* =============================================================================*\
*
* Filename: Executer
* Description: 
*
* Version: 1.0
* Created: 2017/10/24 15:18:06 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public sealed class CExecuter : IDisposable, IOutSender
    {
        public event ProcessStartedEventHandler ProcessStarted { add { MainProcess.ProcessStarted += value; } remove { MainProcess.ProcessStarted -= value; } }
        public event OutputReceivedEventHandler OutputReceived { add { MainProcess.OutputReceived += value; } remove { MainProcess.OutputReceived -= value; } }
        public bool BlockNullOutput { get { return MainProcess.BlockNullOutput; } set { MainProcess.BlockNullOutput = value; } }
        public OutputData Execute(Command command)
            => Execute(command.FileName, command.FullCommand);
        public OutputData AdbExecute(string command)
         => Execute(Command.MakeForAdb(command));
        public OutputData AdbExecute(string devId, string command)
            => Execute(Command.MakeForAdb(devId, command));
        public OutputData FastbootExecute(string command)
            => Execute(Command.MakeForFastboot(command));
        public OutputData FastbootExecute(string devId, string command)
            => Execute(Command.MakeForFastboot(devId, command));
        public OutputData QuicklyShell(string id, string command, out bool isSuccessful)
        {
            var o = QuicklyShell(id, command, out int exitCode);
            isSuccessful = exitCode == 0;
            return o;
        }
        public ShellOutput QuicklyShell(string id, string command)
        {
            var o = QuicklyShell(id, command, out int retCode);
            var shell_output = new ShellOutput(o)
            {
                ReturnCode = retCode
            };
            return shell_output;
        }
        public OutputData QuicklyShell(string id, string command, out int returnCode)
        {
            var o = Execute(Command.MakeForAdb($"-s {id} shell \"{command}\" ; echo __ec$?"));
            try
            {
                string lastLine = o.LineAll[o.LineAll.Count - 1];
                string strExitCode = Regex.Match(lastLine, @"__ec(?<code>\d+)").Result("${code}");
                returnCode = Convert.ToInt32(strExitCode);
            }
            catch (NullReferenceException)
            {
                returnCode = 1;
            }
            return o;
        }

        private ABProcess MainProcess = new ABProcess();
        private Object Locker = new object();
        private OutputData Execute(string fileName, string args, bool needCheck = true)
        {
            if (needCheck)
            {
                Check();
            }
            lock (Locker)
            {
                return MainProcess.RunToExited(fileName, args);
            }
        }
        public static void Check()
        {
            if (Process.GetProcessesByName("adb").Length == 0)
            {
                using (CExecuter executer = new CExecuter())
                {
                    executer.Execute(ConstData.ADB_PATH, "start-server", false);
                }
            }
        }
        public void Dispose()
        {
            MainProcess.Dispose();
        }
    }
}

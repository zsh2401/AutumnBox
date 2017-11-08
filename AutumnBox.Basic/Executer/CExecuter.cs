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
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public sealed class CExecuter : IDisposable, IOutSender
    {
        public event ProcessStartedEventHandler ProcessStarted { add { MainProcess.ProcessStarted += value; } remove { MainProcess.ProcessStarted -= value; } }
        [Obsolete("please use OutputReceived to instead")]
        public event DataReceivedEventHandler OutputDataReceived { add { MainProcess.OutputDataReceived += value; } remove { MainProcess.OutputDataReceived -= value; } }
        [Obsolete("please use OutputReceived to instead")]
        public event DataReceivedEventHandler ErrorDataReceived { add { MainProcess.ErrorDataReceived += value; } remove { MainProcess.ErrorDataReceived -= value; } }
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

        private ABProcess MainProcess = new ABProcess();
        private Object Locker = new object();
        public CExecuter()
        {
        }
        private OutputData Execute(string fileName, string args, bool needCheck = true)
        {
            if (needCheck)
            {
                Check();
            }
            lock (Locker)
            {
                MainProcess.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                return MainProcess.RunToExited(fileName, args);
            }
        }
        private static void Check()
        {
            if (Process.GetProcessesByName("adb").Length == 0)
            {
                new CExecuter().Execute(ConstData.ADB_PATH, "start-server", false);
            }
        }

        public void Dispose()
        {
            MainProcess.Dispose();
        }
    }
}

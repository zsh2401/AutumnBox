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
    public delegate void OutputReceivedEventHandler(object sender, OutputReceivedEventArgs e);
    public class OutputReceivedEventArgs : EventArgs
    {
        public bool IsError { get; private set; }
        public string Text { get; private set; }
        public OutputReceivedEventArgs(string text, bool isError = false)
        {
            Text = text;
            IsError = isError;
        }
    }
    public sealed class CExecuter
    {
        private ABProcess mainProcess = new ABProcess();
        public event ProcessStartedEventHandler ProcessStart { add { mainProcess.ProcessStarted += value; } remove { mainProcess.ProcessStarted -= value; } }
        [Obsolete("please use OutputReceived to instead")]
        public event DataReceivedEventHandler OutDataReceived { add { mainProcess.OutputDataReceived += value; } remove { mainProcess.OutputDataReceived -= value; } }
        [Obsolete("please use OutputReceived to instead")]
        public event DataReceivedEventHandler ErrorDataReceived { add { mainProcess.ErrorDataReceived += value; } remove { mainProcess.ErrorDataReceived -= value; } }
        public bool BlockNullOutput { get; set; } = true;
        public event OutputReceivedEventHandler OutputReceived;
        public CExecuter()
        {
            mainProcess.OutputDataReceived += (s, e) =>
            {
                if (BlockNullOutput && e.Data == null) return;
                OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e.Data, false));
            };
            mainProcess.ErrorDataReceived += (s, e) =>
            {
                if (BlockNullOutput && e.Data == null) return;
                OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e.Data, true));
            };
        }
        private OutputData Execute(string fileName, string args, bool needCheck = true)
        {
            if (needCheck)
            {
                Check();
            }
            return mainProcess.RunToExited(fileName, args);
        }
        public OutputData Execute(Command command)
            => Execute(command.FileName, command.FullCommand);
        public OutputData AdbExecute(string devId, string command)
            => Execute(new Command(devId, command, ExeType.Adb));
        public OutputData AdbExecute(string command)
            => Execute(new Command(command, ExeType.Adb));
        public OutputData FastbootExecute(string devId, string command)
            => Execute(new Command(devId, command, ExeType.Fastboot));
        public OutputData FastbootExecute(string command)
            => Execute(new Command(command, ExeType.Fastboot));
        private static void Check()
        {
            if (Process.GetProcessesByName("adb").Length == 0)
            {
                new CExecuter().Execute(ConstData.ADB_PATH, "start-server", false);
            }
        }
    }
}

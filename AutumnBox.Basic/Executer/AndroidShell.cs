/* =============================================================================*\
*
* Filename: AndroidShell
* Description: 
*
* Version: 1.0
* Created: 2017/11/10 19:28:13 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
#define LOG_OUTPUT
#define LOG_FULL_OUTPUT
#define LOG_INPUT
using AutumnBox.Basic.Util;
using AutumnBox.Support.CstmDebug;
using AutumnBox.Support.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public delegate void InputReceivedEventHandler(object sender, InputReceivedEventArgs e);
    public class InputReceivedEventArgs : EventArgs
    {
        public string Command { get; set; }
        public readonly DateTime Time;
        public InputReceivedEventArgs()
        {
            Time = DateTime.Now;
        }
    }
    public sealed class AndroidShell : IOutSender, IDisposable
    {
        public event InputReceivedEventHandler InputReceived;
        public event OutputReceivedEventHandler OutputReceived;
        public bool IsConnect { get; private set; }
        public bool BlockNullOutput { get; set; } = true;
        public bool BlockEmptyOutput { get; set; } = true;
        public bool BlockLastCommand { get; set; } = true;
        public bool HasExited { get { return _mainProcess.HasExited; } }
        public string LatestLineOutput { get; private set; }
        public string LastCommand { get; private set; }
        public bool IsEnableSaftyWait { get; set; } = true;
        public OutputData Output { get; private set; }
        private static readonly string[] BlockedCommands = { "su" };
        private static readonly string AndroidShellWaitMark = "__androidshellfinishedmark__";
        private Process _mainProcess;
        private string _devID;
        private StreamWriter _cmdWriter;
        public AndroidShell(string id)
        {
            _devID = id;
            _mainProcess = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = "cmd.exe",
                }
            };
            _mainProcess.Exited += (s, e) => { };
            Connect();
        }
        private void OnOutputReceived(OutputReceivedEventArgs e)
        {
#if LOG_FULL_OUTPUT
            Logger.D(e.Text);
#endif
            if (e.Text == AndroidShellWaitMark) { _isLocking = false; return; }
            if (e.Text == "" && BlockEmptyOutput) return;
            if (e.Text == null && BlockNullOutput) return;
            if (e.Text.Contains("$ " + LastCommand) || e.Text.Contains("# " + LastCommand)) return;
#if !LOG_FULL_OUTPUT && LOG_OUTPUT
            Logger.D(e.Text);
#endif
            OutputReceived?.Invoke(this, e);
            LatestLineOutput = e.Text;
        }
        private void OnInputReceived(InputReceivedEventArgs e)
        {
            if (!BlockLastCommand)
                InputReceived?.Invoke(this, e);
        }
        private void Connect()
        {
            CExecuter.Check();
            if (IsConnect) throw new Exception("do not connect again");
            _mainProcess.Start();
            _mainProcess.BeginOutputReadLine();
            _mainProcess.BeginErrorReadLine();
            _mainProcess.OutputDataReceived += (s, e) => { if (e == null & BlockNullOutput) return; OnOutputReceived(new OutputReceivedEventArgs(e.Data, e, false)); };
            _mainProcess.ErrorDataReceived += (s, e) => { if (e == null & BlockNullOutput) return; OnOutputReceived(new OutputReceivedEventArgs(e.Data, e, true)); };
            _cmdWriter = _mainProcess.StandardInput;
            _cmdWriter.AutoFlush = true;
            IsConnect = true;
            Thread.Sleep(200);
            EnterToShell();
            Output = new OutputData
            {
                OutSender = this
            };
        }
        private bool _isLocking = false;
        private void EnterToShell()
        {
            _cmdWriter.WriteLine($"{ConstData.ADB_PATH.Replace('/', '\\')} -s {_devID} shell");
        }
        public bool IsSuperuser
        {
            get
            {
                DrangerousInputLine("echo $USER");
                Thread.Sleep(1000);
                return Output.LineAll[Output.LineAll.Count - 1] == "root";
            }
        }
        public bool Switch2Superuser()
        {
            DrangerousInputLine($"su");
            Thread.Sleep(2000);
            return IsSuperuser;
            //if (LatestLineOutput.Contains("__sunotfound__"))
            //{
            //    return false;
            //}
            //return true;
        }
        public bool Switch2Normaluser()
        {
            if (IsSuperuser)
            {
                DrangerousInputLine("exit");
                Thread.Sleep(2000);
            }
            return !IsSuperuser;
        }
        public void ForceStopLocking()
        {
            _isLocking = false;
        }
        public void InputLine(string command)
        {
            if (BlockedCommands.Contains(command)) return;
            if (command == "exit") { DrangerousInputLine("exit"); return; }
#if LOG_INPUT
            Logger.D(command);
#endif
            _isLocking = true;
            _cmdWriter.WriteLine(command + $";echo {AndroidShellWaitMark}");
            LastCommand = command;
            OnInputReceived(new InputReceivedEventArgs() { Command = command });
            while (_isLocking && IsEnableSaftyWait)
            {
                Thread.Sleep(500);
            }
        }
        public void DrangerousInputLine(string command)
        {
#if LOG_INPUT
            Logger.D(command);
#endif
            _cmdWriter.WriteLine(command);
            OnInputReceived(new InputReceivedEventArgs() { Command = command });
        }
        public void ExitShell()
        {
            while (!_mainProcess.HasExited)
            {
                try
                {
                    InputLine("exit");
                    Thread.Sleep(1000);
                }
                catch
                {

                }
            }
        }
        public void Dispose()
        {
            ExitShell();
            SystemHelper.KillProcessAndChildrens(_mainProcess.Id);
            _cmdWriter.Dispose();
            _mainProcess.Dispose();
            IsConnect = false;
        }
    }


}

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
using AutumnBox.Basic.Util;
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
        public int SafetyInterval { get { return _safetyInterval; } set { if (value > MinSafetyInterval) _safetyInterval = value; } }
        private int _safetyInterval = 2000;
        public static int MinSafetyInterval = 1000;
        public OutputData Output { get; private set; }
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
            if (e.Text == "" && BlockEmptyOutput) return;
            if (e.Text == null && BlockNullOutput) return;
            if (e.Text.Contains("$ " + LastCommand) || e.Text.Contains("# " + LastCommand)) return;
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
            if (IsConnect) throw new Exception("do not connect again");
            _mainProcess.Start();
            _mainProcess.BeginOutputReadLine();
            _mainProcess.BeginErrorReadLine();
            _mainProcess.OutputDataReceived += (s, e) => { if (e == null & BlockNullOutput) return; OnOutputReceived(new OutputReceivedEventArgs(e.Data, e, false)); };
            _mainProcess.ErrorDataReceived += (s, e) => { if (e == null & BlockNullOutput) return; OnOutputReceived(new OutputReceivedEventArgs(e.Data, e, true)); };
            _cmdWriter = _mainProcess.StandardInput;
            _cmdWriter.AutoFlush = true;
            IsConnect = true;
            InputLine($"{ConstData.ADB_PATH.Replace('/', '\\')} -s {_devID} shell");
            Output = new OutputData
            {
                OutSender = this
            };
        }

        public void InputLine(string command)
        {
            if (IsConnect)
            {
                _cmdWriter.WriteLine(command);
                LastCommand = command;
                OnInputReceived(new InputReceivedEventArgs() { Command = command });
                Thread.Sleep(_safetyInterval);
            }
            else
                throw new Exception("shell not connect");
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

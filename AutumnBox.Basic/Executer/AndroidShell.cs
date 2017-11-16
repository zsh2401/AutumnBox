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
        public event ProcessStartedEventHandler ProcessStarted;
        public event InputReceivedEventHandler InputReceived;
        public event OutputReceivedEventHandler OutputReceived;
        public bool IsConnect { get; private set; }
        public bool BlockNullOutput { get; set; } = true;
        public bool BlockEmptyOutput { get; set; } = true;
        public bool BlockLastCommand { get; set; } = true;
        public bool HasExited { get { return _mainProcess.HasExited; } }
        public string LatestLineOutput { get; private set; }
        public string LastCommand { get; private set; }
        public int? LastExitcode { get; private set; }
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
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    FileName = ConstData.ADB_PATH.Replace("/", "\\"),
                    Arguments = $" -s {_devID} shell"
                }
            };
            _mainProcess.OutputDataReceived += (s, e) => { OnOutputReceived(new OutputReceivedEventArgs(e.Data, e, false)); };
            _mainProcess.ErrorDataReceived += (s, e) => { OnOutputReceived(new OutputReceivedEventArgs(e.Data, e, true)); };
        }
        public void Connect()
        {
            Output = new OutputData();
            _mainProcess.Start();
            ProcessStarted?.Invoke(this, new ProcessStartedEventArgs() { Pid = _mainProcess.Id });
            Logger.D("process started...pid " + _mainProcess.Id);
            _cmdWriter = _mainProcess.StandardInput;
            _mainProcess.BeginOutputReadLine();
            _mainProcess.BeginErrorReadLine();
            IsConnect = true;
            Thread.Sleep(2000);
        }
        public void Disconnect()
        {
            while (!_mainProcess.HasExited)
            {
                try
                {
                    Input("exit");
                    Thread.Sleep(200);
                }
                catch { }
            }
            SystemHelper.KillProcessAndChildrens(_mainProcess.Id);
            IsConnect = false;
        }
        public bool IsRuningAsSuperuser
        {
            get
            {
                SafetyInput("echo $USER");
                Thread.Sleep(200);
                Logger.D("latest line" + LatestLineOutput);
                return LatestLineOutput == "root";
            }
        }
        public bool Switch2Superuser()
        {
            if (IsRuningAsSuperuser) return true;
            Input($"su");
            Thread.Sleep(500);
            var now = IsRuningAsSuperuser;
            Logger.D("return" + now);
            return now;
        }
        public bool Switch2Normaluser()
        {
            if (IsRuningAsSuperuser)
            {
                Input("exit");
            }
            return !IsRuningAsSuperuser;
        }
        public bool SafetyInput(string command)
        {
            LastExitcode = null;
            if (!IsConnect) return false;
            Input(command + $"; echo __ec$?", 0);
            while (LastExitcode == null) ;
            if (LastExitcode == 0) return true;
            else return false;
        }
        public int SafetyInputRetNum(string command)
        {
            LastExitcode = null;
            if (!IsConnect) return 24010;
            Input(command + $"; echo __ec$?", 0);
            while (LastExitcode == null) ;
            return LastExitcode??24010;
        }
        private static bool IsNum(string str)
        {
            try { Convert.ToInt32(str); return true; } catch { return false; }
        }
        public void Input(string command, int waitInterval = 2000)
        {
            if (!IsConnect) return;
            OnInputReceived(new InputReceivedEventArgs() { Command = command });
            _cmdWriter.WriteLine(command);
            Thread.Sleep(waitInterval);
        }
        public void Dispose()
        {
            new Thread(Disconnect).Start();
        }
        private void OnInputReceived(InputReceivedEventArgs e)
        {
            InputReceived?.Invoke(this, e);
        }
        private void OnOutputReceived(OutputReceivedEventArgs e)
        {
            Logger.D("untreated output ->" + e.Text);
            if (!e.IsError) Output.OutAdd(e.Text);
            else Output.ErrorAdd(e.Text);
            if (e.Text == null && BlockNullOutput) return;
            if (e.Text == "" && BlockEmptyOutput) return;
            if (e.Text.Contains("$ " + LastCommand)) return;
            try
            {
                if (e.Text.Contains("__ec") && !(e.Text.Contains("__ec$")))
                {
                    LastExitcode = Convert.ToInt32(e.Text.Remove(0, 4));
                    return;
                }
            }
            catch { }
            //_FULL_OUTPUT && LOG_
            Logger.D("treated output ->" + e.Text);
            OutputReceived?.Invoke(this, e);
            LatestLineOutput = e.Text;
        }
    }
}

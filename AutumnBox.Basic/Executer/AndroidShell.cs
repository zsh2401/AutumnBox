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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public class ShellOutput
    {
        public bool IsSuccess { get { return ReturnCode == 0; } }
        public int ReturnCode { get; set; } = 0;
        public void OutAdd(string text)
        {
            LineAll.Add(text);
            All.AppendLine(text);
        }
        public List<string> LineAll { get; private set; } = new List<string>();
        public StringBuilder All { get; private set; } = new StringBuilder();
        public string AllString
        {
            get
            {
                string tmp = "";
                LineAll.ForEach((s) =>
                {
                    tmp += s + "\n";
                });
                return tmp;
            }
        }
    }
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
        private static readonly string _finishMark = "___ec$?";
        public event ProcessStartedEventHandler ProcessStarted;
        public event InputReceivedEventHandler InputReceived;
        public event OutputReceivedEventHandler OutputReceived;
        public bool HasExited { get { return _mainProcess.HasExited; } }
        public bool IsConnect { get; private set; } = false;
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
        public bool IsRunningAsSuperuser
        {
            get
            {
                return SafetyInput("echo $USER").LineAll.Last() == "root";
            }
        }
        private bool _isSwitchedSu = false;
        public bool Switch2Su()
        {
            if (_isSwitchedSu) return true;
            Input("su");
            Thread.Sleep(200);
            if (IsRunningAsSuperuser)
            {
                _isSwitchedSu = true;
                return true;
            }
            return false;
        }
        public bool Switch2Normal()
        {
            if (!_isSwitchedSu) return true;
            Input("exit");
            Thread.Sleep(200);
            return IsRunningAsSuperuser;
        }
        public void Input(string command)
        {
            OnInputReceived(new InputReceivedEventArgs() { Command = command });
            _mainProcess.StandardInput.WriteLine(command);
            _mainProcess.StandardInput.Flush();
        }
        public ShellOutput SafetyInput(string command)
        {
            _latestReturnCode = null;
            BeginRead();
            Input(command + $"; echo {_finishMark}");
            while (_latestReturnCode == null) ;
            StopRead();
            _outTmp.ReturnCode = _latestReturnCode ?? 24010;
            Logger.D("command execute finished");
            return _outTmp;
        }
        public void Connect(bool asSuperuser = false)
        {
            CExecuter.Check();
            _mainProcess.Start();
            ProcessStarted?.Invoke(this, new ProcessStartedEventArgs() { Pid = _mainProcess.Id });
            _mainProcess.BeginOutputReadLine();
            _mainProcess.BeginErrorReadLine();
            IsConnect = true;
            Thread.Sleep(200);
        }
        public void Disconnect()
        {
            new Thread(() =>
            {
                while (!_mainProcess.HasExited)
                {
                    try
                    {
                        Input("exit");
                        Thread.Sleep(100);
                    }
                    catch { }
                }
                SystemHelper.KillProcessAndChildrens(_mainProcess.Id);
                IsConnect = false;
            }).Start();
        }
        public void Dispose()
        {
            Disconnect();
        }


        private void OnOutputReceived(OutputReceivedEventArgs e)
        {
            if (e.Text == null || e.Text == "") return;
            var m = Regex.Match(e.Text, @"^___ec(?<code>-?\d+)$");
            if (Regex.IsMatch(e.Text, @"^.+;\u0020echo\u0020___ec\$\?$"))
            {
                return;
            }
            if (m.Success)
            {
                _latestReturnCode = Convert.ToInt32(m.Result("${code}"));
                return;
            }
            if (_isEnableRead)
            {
                _outTmp.OutAdd(e.Text);
            }
            OutputReceived?.Invoke(this, e);
        }
        private void OnInputReceived(InputReceivedEventArgs e)
        {
            _latestCommand = e.Command;
            InputReceived?.Invoke(this, e);
        }
        private Process _mainProcess;
        private int? _latestReturnCode = 0;
        private string _latestCommand = "";
        private string _devID;
        private string _latestOutput { get { return _outTmp.LineAll[_outTmp.LineAll.Count - 1]; } }
        private ShellOutput _outTmp = null;
        private bool _isEnableRead = false;
        private void BeginRead()
        {
            _outTmp = new ShellOutput();
            _isEnableRead = true;
        }
        private void StopRead()
        {
            _isEnableRead = false;
        }
        //public event ProcessStartedEventHandler ProcessStarted;
        //public event InputReceivedEventHandler InputReceived;
        //public event OutputReceivedEventHandler OutputReceived;
        //public bool IsConnect { get; private set; }
        //public bool BlockNullOutput { get; set; } = true;
        //public bool BlockEmptyOutput { get; set; } = true;
        //public bool BlockLastCommand { get; set; } = true;
        //public bool HasExited { get { return _mainProcess.HasExited; } }
        //public string LatestLineOutput { get; private set; }
        //public string LastCommand { get; private set; } = "";
        //public int? LastExitcode { get; private set; }
        //public OutputData Output { get; private set; }
        //private Process _mainProcess;
        //private string _devID;
        //private StreamWriter _cmdWriter;
        //public AndroidShell(string id)
        //{
        //    _devID = id;
        //    _mainProcess = new Process
        //    {
        //        StartInfo = new ProcessStartInfo()
        //        {
        //            RedirectStandardError = true,
        //            RedirectStandardInput = true,
        //            RedirectStandardOutput = true,
        //            CreateNoWindow = true,
        //            UseShellExecute = false,
        //            FileName = ConstData.ADB_PATH.Replace("/", "\\"),
        //            Arguments = $" -s {_devID} shell"
        //        }
        //    };
        //    _mainProcess.OutputDataReceived += (s, e) => { OnOutputReceived(new OutputReceivedEventArgs(e.Data, e, false)); };
        //    _mainProcess.ErrorDataReceived += (s, e) => { OnOutputReceived(new OutputReceivedEventArgs(e.Data, e, true)); };
        //}
        //public void Connect()
        //{
        //    CExecuter.Check();
        //    Output = new OutputData();
        //    _mainProcess.Start();
        //    ProcessStarted?.Invoke(this, new ProcessStartedEventArgs() { Pid = _mainProcess.Id });
        //    Logger.D("process started...pid " + _mainProcess.Id);
        //    _cmdWriter = _mainProcess.StandardInput;
        //    _mainProcess.BeginOutputReadLine();
        //    _mainProcess.BeginErrorReadLine();
        //    IsConnect = true;
        //    Thread.Sleep(1000);
        //}
        //public void Disconnect()
        //{
        //    while (!_mainProcess.HasExited)
        //    {
        //        try
        //        {
        //            Input("exit");
        //            Thread.Sleep(200);
        //        }
        //        catch { }
        //    }
        //    SystemHelper.KillProcessAndChildrens(_mainProcess.Id);
        //    IsConnect = false;
        //}
        //public bool IsRuningAsSuperuser
        //{
        //    get
        //    {
        //        SafetyInput("echo $USER");
        //        Thread.Sleep(200);
        //        Logger.D("latest line" + LatestLineOutput);
        //        return LatestLineOutput == "root";
        //    }
        //}
        //public bool Switch2Superuser()
        //{
        //    if (IsRuningAsSuperuser) return true;
        //    Input($"su");
        //    Thread.Sleep(200);
        //    var now = IsRuningAsSuperuser;
        //    Logger.D("return" + now);
        //    return now;
        //}
        //public bool Switch2Normaluser()
        //{
        //    if (IsRuningAsSuperuser)
        //    {
        //        Input("exit");
        //    }
        //    return !IsRuningAsSuperuser;
        //}
        //public int NSafetyInput(string command)
        //{
        //    LastExitcode = null;
        //    if (!IsConnect) return 24010;
        //    Input(command + $"; echo __ec$?", 0);
        //    while (LastExitcode == null) ;
        //    return LastExitcode ?? 24010;
        //}
        //public ShellOutput SafetyInput(string command)
        //{
        //    StartOutputGet();
        //    LastExitcode = null;
        //    if (!IsConnect)
        //    {
        //        throw new Exception("please shell.Connect()");
        //    }
        //    Input(command + $"; echo __ec$?", 0);
        //    while (LastExitcode == null) ;
        //    StopOutputGet();
        //    _tmpOut.ReturnCode = LastExitcode ?? 24010;
        //    return _tmpOut;
        //}
        //private static bool IsNum(string str)
        //{
        //    try { Convert.ToInt32(str); return true; } catch { return false; }
        //}
        //public void Input(string command, int waitInterval = 2000)
        //{
        //    if (!IsConnect) return;
        //    LastCommand = command;
        //    OnInputReceived(new InputReceivedEventArgs() { Command = command });
        //    _cmdWriter.WriteLine(command);
        //    Thread.Sleep(waitInterval);
        //}
        //public void Dispose()
        //{
        //    new Thread(Disconnect).Start();
        //}
        //private bool _isEnableOutputGetting = false;
        //private ShellOutput _tmpOut;
        //private void StartOutputGet()
        //{
        //    _tmpOut = new ShellOutput();
        //    _isEnableOutputGetting = true;
        //}
        //private void StopOutputGet()
        //{
        //    _isEnableOutputGetting = false;
        //}
        //private void OnInputReceived(InputReceivedEventArgs e)
        //{
        //    InputReceived?.Invoke(this, e);
        //}
        //private void OnOutputReceived(OutputReceivedEventArgs e)
        //{
        //    Logger.D("untreated output ->" + e.Text);
        //    /*一系列的无用输出屏蔽*/
        //    if (!e.IsError) Output.OutAdd(e.Text);
        //    else Output.ErrorAdd(e.Text);
        //    //if (e.Text == null || e.Text == "") return;
        //    if (e.Text == null && BlockNullOutput) return;
        //    if (e.Text == "" && BlockEmptyOutput) return;
        //    if (e.Text.Contains(LastCommand)) return;
        //    try
        //    {
        //        if (e.Text.Contains("__ec") && !(e.Text.Contains("__ec$")))
        //        {
        //            LastExitcode = Convert.ToInt32(e.Text.Remove(0, 4));
        //            return;
        //        }
        //    }
        //    catch { }
        //    /*过滤出来的有用信息*/
        //    Logger.D("treated output ->" + e.Text);
        //    LatestLineOutput = e.Text;
        //    OutputReceived?.Invoke(this, e);
        //    if (_isEnableOutputGetting)
        //    {
        //        _tmpOut.OutAdd(e.Text);
        //    }
        //}
    }
}

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
using AutumnBox.Basic.Adb;
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
    /// <summary>
    /// 简单封装的,用于执行基础命令的AndroidShell类
    /// </summary>
    public sealed class AndroidShell : IOutSender, IDisposable
    {
        /// <summary>
        /// 每条常规命令后都会有这个命令,用来获取返回值
        /// </summary>
        private static readonly string _finishMark = "___ec$?";
        /// <summary>
        /// 进程开始时发生
        /// </summary>
        public event ProcessStartedEventHandler ProcessStarted;
        /// <summary>
        /// 接收到输入时发生
        /// </summary>
        public event InputReceivedEventHandler InputReceived;
        /// <summary>
        /// 接收到输出时发生
        /// </summary>
        public event OutputReceivedEventHandler OutputReceived;
        /// <summary>
        /// 是否退出
        /// </summary>
        public bool HasExited { get { return _mainProcess.HasExited; } }
        /// <summary>
        /// 是否连接
        /// </summary>
        public bool IsConnect { get; private set; } = false;
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="id"></param>
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
        /// <summary>
        /// 是否运行在超级用户(root)下
        /// </summary>
        public bool IsRunningAsSuperuser
        {
            get
            {
                return SafetyInput("echo $USER").LineAll.Last() == "root";
            }
        }
        /// <summary>
        /// 是否已经切换到超级用户
        /// </summary>
        private bool _isSwitchedSu = false;
        /// <summary>
        /// 切换到超级用户
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 切换到普通用户
        /// </summary>
        /// <returns></returns>
        public bool Switch2Normal()
        {
            if (!_isSwitchedSu) return true;
            Input("exit");
            Thread.Sleep(200);
            return IsRunningAsSuperuser;
        }
        /// <summary>
        /// 输入一条指令,不会进行自动等待
        /// </summary>
        /// <param name="command"></param>
        public void Input(string command)
        {
            OnInputReceived(new InputReceivedEventArgs() { Command = command });
            _mainProcess.StandardInput.WriteLine(command);
            _mainProcess.StandardInput.Flush();
        }
        /// <summary>
        /// 输入一条指令,会进行线程等待并且可以获取返回值
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 连接到设备上
        /// </summary>
        /// <param name="asSuperuser"></param>
        public void Connect(bool asSuperuser = false)
        {
            AdbHelper.Check();
            _mainProcess.Start();
            ProcessStarted?.Invoke(this, new ProcessStartedEventArgs() { Pid = _mainProcess.Id });
            _mainProcess.BeginOutputReadLine();
            _mainProcess.BeginErrorReadLine();
            IsConnect = true;
            if (asSuperuser)
            {
                Switch2Su();
            }
            Thread.Sleep(200);
        }
        /// <summary>
        /// 断开连接
        /// </summary>
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
        /// <summary>
        /// 析构
        /// </summary>
        public void Dispose()
        {
            Disconnect();
        }

        /// <summary>
        /// 触发OutputReceived事件
        /// </summary>
        /// <param name="e"></param>
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
        /// <summary>
        /// 触发InputReceived事件
        /// </summary>
        /// <param name="e"></param>
        private void OnInputReceived(InputReceivedEventArgs e)
        {
            _latestCommand = e.Command;
            InputReceived?.Invoke(this, e);
        }
        /// <summary>
        /// 主进程
        /// </summary>
        private Process _mainProcess;
        /// <summary>
        /// 最近一次命令执行返回值
        /// </summary>
        private int? _latestReturnCode = 0;
        /// <summary>
        /// 最近一次的命令
        /// </summary>
        private string _latestCommand = "";
        /// <summary>
        /// 连接的设备ID
        /// </summary>
        private string _devID;
        /// <summary>
        /// 最近的一行输出
        /// </summary>
        private string _latestOutput { get { return _outTmp.LineAll[_outTmp.LineAll.Count - 1]; } }
        /// <summary>
        /// 用来存储输出的缓冲类
        /// </summary>
        private ShellOutput _outTmp = null;
        /// <summary>
        /// 是否存储输出
        /// </summary>
        private bool _isEnableRead = false;
        /// <summary>
        /// 开始获取输出
        /// </summary>
        private void BeginRead()
        {
            _outTmp = new ShellOutput();
            _isEnableRead = true;
        }
        /// <summary>
        /// 停止获取输出
        /// </summary>
        private void StopRead()
        {
            _isEnableRead = false;
        }
    }
}

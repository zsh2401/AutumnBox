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
using AutumnBox.Basic.Adb;
using AutumnBox.Basic.Util;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    /// <summary>
    /// 命令执行器/新版
    /// </summary>
    public sealed class CExecuter : IDisposable, IOutSender
    {
        /// <summary>
        /// 当核心进程开始时将引发这个事件
        /// </summary>
        public event ProcessStartedEventHandler ProcessStarted { add { MainProcess.ProcessStarted += value; } remove { MainProcess.ProcessStarted -= value; } }
        /// <summary>
        /// 当执行时接收到输出时引发此事件
        /// </summary>
        public event OutputReceivedEventHandler OutputReceived { add { MainProcess.OutputReceived += value; } remove { MainProcess.OutputReceived -= value; } }
        /// <summary>
        /// 是否屏蔽NULL输出
        /// </summary>
        public bool BlockNullOutput { get { return MainProcess.BlockNullOutput; } set { MainProcess.BlockNullOutput = value; } }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public OutputData Execute(Command command)
            => Execute(command.FileName, command.FullCommand);
        /// <summary>
        /// 执行ADB命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public OutputData AdbExecute(string command)
         => Execute(Command.MakeForAdb(command));
        /// <summary>
        /// 执行ADB命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public OutputData AdbExecute(string devId, string command)
            => Execute(Command.MakeForAdb(devId, command));
        /// <summary>
        /// 执行Fastboot命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public OutputData FastbootExecute(string command)
            => Execute(Command.MakeForFastboot(command));
        /// <summary>
        /// 执行Fastboot命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public OutputData FastbootExecute(string devId, string command)
            => Execute(Command.MakeForFastboot(devId, command));
        /// <summary>
        /// 执行Shell命令(没有ROOT权限)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public OutputData QuicklyShell(string id, string command, out bool isSuccessful)
        {
            var o = QuicklyShell(id, command, out int exitCode);
            isSuccessful = exitCode == 0;
            return o;
        }
        /// <summary>
        /// 执行Shell命令(没有ROOT权限)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ShellOutput QuicklyShell(string id, string command)
        {
            Logger.D("quickly shell" + command);
            var o = QuicklyShell(id, command, out int retCode);
            var shell_output = new ShellOutput(o)
            {
                ReturnCode = retCode
            };
            return shell_output;
        }
        /// <summary>
        /// 执行Shell命令(没有ROOT权限)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public OutputData QuicklyShell(string id, string command, out int returnCode)
        {
            Logger.T($"execute shell command ->{command} for dev {id}");
            var o = Execute(Command.MakeForAdb($"-s {id} shell \"{command}\" ; echo __ec$?"));
            try
            {
                string lastLine = o.LineAll[o.LineAll.Count - 1];
                string strExitCode = Regex.Match(lastLine, @"__ec(?<code>\d+)").Result("${code}");
                returnCode = Convert.ToInt32(strExitCode);
            }
            catch (Exception e)
            {
                Logger.T("quickly shell failed...", e);
                returnCode = 1;
                if (e is NotSupportedException) returnCode = 24010;
                else if (e is IndexOutOfRangeException) returnCode = 24011;
            }
            Logger.D("return code ->" + returnCode);
            return o;
        }
        /// <summary>
        /// 执行器主进程
        /// </summary>
        private ABProcess MainProcess = new ABProcess();
        /// <summary>
        /// 执行锁.以免一次执行多个命令
        /// </summary>
        private Object Locker = new object();
        /// <summary>
        /// 更加详细的执行命令
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="args">启动参数</param>
        /// <param name="needCheck">是否需要检查ADB的状态</param>
        /// <returns></returns>
        internal OutputData Execute(string fileName, string args, bool needCheck = true)
        {
            lock (Locker)
            {
                if (needCheck && !AdbHelper.Check())
                {
                    Logger.T("adb server check failed.......cannot start adb-server?");
                    return new OutputData();
                }
                var o = MainProcess.RunToExited(fileName, args);
                if (o.All.ToString().Contains("cannot connect to daemon"))
                {
                    Logger.T("cannot cannect to daemon! fullout ->" + o.All.ToString());
                    return new OutputData();
                }
                return o;
            }
        }
        /// <summary>
        /// 析构
        /// </summary>
        public void Dispose()
        {
            MainProcess.Dispose();
        }
    }
}

/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/26 2:05:41 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Calling.Cmd;
using AutumnBox.Basic.Calling.Fastboot;
using AutumnBox.Basic.Device;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using AutumnBox.Basic.Util.Debugging;

namespace AutumnBox.Basic.Calling
{
    /// <summary>
    /// 提供高效的,自动化的命令管理
    /// </summary>
    public sealed class CommandStation
    {
        private Logger<CommandStation> logger = new Logger<CommandStation>();
        private readonly List<ProcessBasedCommand> commands = new List<ProcessBasedCommand>();
        /// <summary>
        /// 注册一个命令,并交由CommandStation管理
        /// </summary>
        /// <param name="command"></param>
        public TCommand Register<TCommand>(TCommand command) where TCommand : ProcessBasedCommand
        {
            commands.Add(command);
            return command;
        }
        /// <summary>
        /// 获取一个被管理的ADB命令
        /// </summary>
        /// <param name="device"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public AdbCommand GetAdbCommand(IDevice device, string cmd)
        {
            return Register(new AdbCommand(device, cmd));
        }
        /// <summary>
        /// 获取一个被管理的ADB命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public AdbCommand GetAdbCommand(string cmd)
        {
            return Register(new AdbCommand(cmd));
        }
        /// <summary>
        /// 获取一个被管理的ADB命令
        /// </summary>
        /// <param name="device"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public FastbootCommand GetFastbootCommand(IDevice device, string cmd)
        {
            return Register(new FastbootCommand(device, cmd));
        }
        /// <summary>
        /// 获取一个被管理的ADB命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public FastbootCommand GetFastbootCommand(string cmd)
        {
            return Register(new FastbootCommand(cmd));
        }
        /// <summary>
        /// 获取一个被管理的Windows cmd命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public WindowsCmdCommand GetCmdCommand(string cmd)
        {
            return Register(new WindowsCmdCommand(cmd));
        }
        /// <summary>
        /// 杀死所有被管理的命令的进程
        /// </summary>
        public void Free()
        {
            var runningCommand = from cmd in commands
                                 where cmd.process.IsRunning()
                                 select cmd;
            foreach (var cmd in runningCommand)
            {
                try
                {
                    cmd.Kill();
                }
                catch (Exception ex)
                {
                    logger.Warn($"can't stop command:{cmd.ToString()}", ex);
                }
            }
        }
    }
}

using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device.ManagementV2.OS
{
    /// <summary>
    /// 用于获取/proc/uptime的类
    /// </summary>
    public sealed class Uptime
    {
        readonly IDevice device;
        readonly ICommandExecutor executor;
        static readonly Regex regex = new Regex(@"(?<uptime>\d+[\.|\d+]?)[^\d]+(?<idle>\d+[\.|\d+]?)");

        /// <summary>
        /// 构造Uptime类
        /// </summary>
        /// <param name="device"></param>
        /// <param name="executor"></param>
        public Uptime(IDevice device, ICommandExecutor executor)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        /// <summary>
        /// 获取运行时间
        /// </summary>
        /// <exception cref="CommandErrorException">无法正确执行命令</exception>
        /// <returns></returns>
        public double GetRunningSeconds()
        {
            var output = executor.AdbShell(device, "cat /proc/uptime").ThrowIfError().Output.All;
            var match = regex.Match(output);
            if (match.Success)
            {
                return double.Parse(match.Result("${uptime}"));
            }
            else
            {
                throw new Exception("Could not parse the output");
            }
        }

        /// <summary>
        /// 获取空闲时间
        /// </summary>
        /// <exception cref="CommandErrorException">命令无法被执行</exception>
        /// <returns></returns>
        public double GetIdleSeconds()
        {
            var output = executor.AdbShell(device, "cat /proc/uptime").ThrowIfError().Output.All;
            var match = regex.Match(output);
            if (match.Success)
            {
                return double.Parse(match.Result("${idle}"));
            }
            else
            {
                throw new Exception("Could not parse the output");
            }
        }
    }
}

using AutumnBox.Basic.Calling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device.ManagementV2.OS
{
    /// <summary>
    /// 用于获取/proc/uptime的类
    /// </summary>
    public class Uptime
    {
        private readonly IDevice device;
        private readonly ICommandExecutor executor;
        private static readonly Regex regex = new Regex(@"(?<uptime>\d+[\.|\d+]?)[^\d]+(?<idle>\d+[\.|\d+]?)");
        public Uptime(IDevice device, ICommandExecutor executor)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }
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

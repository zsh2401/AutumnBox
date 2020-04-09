using AutumnBox.Basic.Calling;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device.ManagementV2.OS
{
    /// <summary>
    /// 内存信息获取器
    /// </summary>
    public class MemInfoGetter
    {
        private readonly IDevice device;
        private readonly ICommandExecutor executor;
        private const string INFO_READ_COMMAND = "cat /proc/meminfo";
        private const string LINE_PATTERN = @"^(?<key>.+):[^\d]+(?<num>[\d]+).+$";
        private static readonly Regex infoRegex = new Regex(LINE_PATTERN, RegexOptions.Multiline | RegexOptions.Compiled);
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        /// <param name="executor"></param>
        public MemInfoGetter(IDevice device, ICommandExecutor executor)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public MemoryInfo Get()
        {
            var output = executor.AdbShell(device, INFO_READ_COMMAND).ThrowIfError().Output.All;
            Dictionary<string, int> info = new Dictionary<string, int>();
            foreach (Match match in infoRegex.Matches(output))
            {
                //SLogger<MemInfoGetter>.Info($"{match.Result("${key}")}-{int.Parse(match.Result("${num}"))}");
                info.Add(match.Result("${key}"), int.Parse(match.Result("${num}")));
            }
            return new  MemoryInfo()
            {
                Total = info["MemTotal"],
                Free = info["MemFree"],
                Available= info["MemAvailable"],
                Buffers = info["Buffers"]
            };
        }
    }
}

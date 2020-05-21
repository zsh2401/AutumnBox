using AutumnBox.Basic.Calling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device.ManagementV2.OS
{
    /// <summary>
    /// CPU活动信息
    /// </summary>
    public struct Stat
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public int User { get; set; }
        public int Nice { get; set; }
        public int System { get; set; }
        public int Idle { get; set; }
        public int Iowait { get; set; }
        public int Irq { get; set; }
        public int SoftIrq { get; set; }
        public int StealStolen { get; set; }
        public int Guset { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
    /// <summary>
    /// Cpu信息获取器
    /// </summary>
    public class StatGetter
    {
        private const string PATTERN_TOTAL = @"cpu[^\d]+(?<user>\d+)[^\d+](?<nice>\d+)[^\d+](?<system>\d+)[^\d+](?<idle>\d+)[^\d+](?<iowait>\d+)[^\d+](?<irq>\d+)[^\d+](?<softirq>\d+)[^\d+](?<stealstolen>\d+)[^\d+](?<guest>\d+)[^\d+]";
        private static readonly Regex _regexTotal = new Regex(PATTERN_TOTAL, RegexOptions.Multiline | RegexOptions.Compiled);
        private const string PATTERN = @"cpu\d[^\d]+(?<user>\d+)[^\d+](?<nice>\d+)[^\d+](?<system>\d+)[^\d+](?<idle>\d+)[^\d+](?<iowait>\d+)[^\d+](?<irq>\d+)[^\d+](?<softirq>\d+)[^\d+](?<stealstolen>\d+)[^\d+](?<guest>\d+)[^\d+]";
        private static readonly Regex _regex = new Regex(PATTERN, RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly IDevice device;
        private readonly ICommandExecutor executor;
        /// <summary>
        /// 构造Cpu信息获取器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="executor"></param>
        public StatGetter(IDevice device, ICommandExecutor executor)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public Stat[] GetCpuStats()
        {
            List<Stat> stats = new List<Stat>();
            var output = executor.AdbShell(device, "cat /proc/stat").ThrowIfError().Output.All;
            foreach (Match match in _regex.Matches(output))
            {
                stats.Add(new Stat()
                {
                    User = int.Parse(match.Result("${user}")),
                    Nice = int.Parse(match.Result("${nice}")),
                    System = int.Parse(match.Result("${system}")),
                    SoftIrq = int.Parse(match.Result("${softirq}")),
                    StealStolen = int.Parse(match.Result("${stealstolen}")),
                    Guset = int.Parse(match.Result("${guest}")),
                    Idle = int.Parse(match.Result("${idle}")),
                    Iowait = int.Parse(match.Result("${iowait}")),
                    Irq = int.Parse(match.Result("${irq}")),
                });
            }
            return stats.ToArray();
        }
        /// <summary>
        /// 获取总值
        /// </summary>
        /// <returns></returns>
        public Stat GetTotal()
        {
            var output = executor.AdbShell(device, "cat /proc/stat").ThrowIfError().Output.All;
            var match = _regexTotal.Match(output);
            if (match.Success)
            {
                return new Stat()
                {
                    User = int.Parse(match.Result("${user}")),
                    Nice = int.Parse(match.Result("${nice}")),
                    System = int.Parse(match.Result("${system}")),
                    SoftIrq = int.Parse(match.Result("${softirq}")),
                    StealStolen = int.Parse(match.Result("${stealstolen}")),
                    Guset = int.Parse(match.Result("${guest}")),
                    Idle = int.Parse(match.Result("${idle}")),
                    Iowait = int.Parse(match.Result("${iowait}")),
                    Irq = int.Parse(match.Result("${irq}")),
                };
            }
            else
            {
                throw new Exception("can not parse");
            }
        }
    }
}

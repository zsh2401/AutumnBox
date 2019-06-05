using AutumnBox.Basic.Calling;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device.ManagementV2.OS
{
    /// <summary>
    /// 电池信息
    /// </summary>
    public struct BatteryInfo
    {
        /// <summary>
        /// 最大充电电流
        /// </summary>
        public int? MaxChargingCurrent { get; set; }
        /// <summary>
        /// 最大充电电压
        /// </summary>
        public int? MaxCharingVoltage { get; set; }
        /// <summary>
        /// 是否为AC充电
        /// </summary>
        public bool? ACPowered { get; set; }
        /// <summary>
        /// 是否为USB充电
        /// </summary>
        public bool? USBPowered { get; set; }
        /// <summary>
        /// 是否为无线充电
        /// </summary>
        public bool? WirelessPowered { get; set; }
        /// <summary>
        /// 电池状态,2为充电,其它为未充电
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 电池健康状态：只有数字2表示good
        /// </summary>
        public int? Health { get; set; }
        /// <summary>
        /// 电池是否安装在机身
        /// </summary>
        public bool? Present { get; set; }
        /// <summary>
        /// 电量: 百分比
        /// </summary>
        public int? Level { get; set; }
        /// <summary>
        /// 电池总容量?
        /// </summary>
        public int? Scale { get; set; }
        /// <summary>
        /// 电压
        /// </summary>
        public int? Voltage { get; set; }
        /// <summary>
        /// 电流,负数为正在充电
        /// </summary>
        public int? CurrentNow { get; set; }
        /// <summary>
        /// 电池温度,单位是0.1摄氏度
        /// </summary>
        public int? Temperature { get; set; }
        /// <summary>
        /// 电池种类/技术
        /// </summary>
        public string Technology { get; set; }
    }
    /// <summary>
    /// 电量管理器
    /// </summary>
    public sealed class BatteryManager
    {
        private static readonly Regex regex = new Regex(@"(?<key>.+):[^\n|^\d|^\w]+(?<value>[\w|\d]+)", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly IDevice device;
        private readonly ICommandExecutor executor;

        /// <summary>
        /// 构造电量管理器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="executor"></param>
        public BatteryManager(IDevice device, ICommandExecutor executor)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }
        /// <summary>
        /// 获取完整的电池信息
        /// </summary>
        /// <returns></returns>
        public BatteryInfo GetBatteryInfo()
        {
            var output = executor.AdbShell(device, "dumpsys battery").ThrowIfError().Output.All;
            var matches = regex.Matches(output);
            var dict = new Dictionary<string, string>();
            foreach (Match match in matches)
            {
                string key = match.Result("${key}").TrimStart();
                string value = match.Result("${value}").TrimEnd();
                dict.Add(key, value);
            }
            return new BatteryInfo
            {
                Status = TryGetInt(dict, "status"),
                Technology = dict["technology"],
                Level = TryGetInt(dict, "level"),
                CurrentNow = TryGetInt(dict, "current now"),
                Health = TryGetInt(dict, "health"),
                WirelessPowered = TryGetBool(dict, "Wireless powered"),
                USBPowered = TryGetBool(dict, "USB powered"),
                ACPowered = TryGetBool(dict, "AC powered"),
                Scale = TryGetInt(dict, "scale"),
                Present = TryGetBool(dict, "present"),
                Temperature = TryGetInt(dict, "temperature"),
                Voltage = TryGetInt(dict, "voltage"),
                MaxChargingCurrent = TryGetInt(dict, "Max charging current"),
                MaxCharingVoltage = TryGetInt(dict, "Max charging voltage")
            };
        }
        private static int? TryGetInt(Dictionary<string, string> dict, string key)
        {
            if (dict.TryGetValue(key, out string value) && int.TryParse(value, out int result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        private static bool? TryGetBool(Dictionary<string, string> dict, string key)
        {
            if (dict.TryGetValue(key, out string value) && bool.TryParse(value, out bool result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 伪造电池充电状态
        /// </summary>
        /// <param name="isCharging"></param>
        public ICommandResult SetChargeState(bool isCharging = true)
        {
            int status = isCharging ? 2 : 1;
            return executor.AdbShell(device, $"dumpsys battery set status {status}");
        }
        /// <summary>
        /// 伪造电池电量
        /// </summary>
        /// <param name="level"></param>
        public ICommandResult SetBatteryLevel(int level)
        {
            if (level <= 0) level = 0;
            else if (level > 100) level = 100;
            return executor.AdbShell(device, $"dumpsys battery set level {level}");
        }
        /// <summary>
        /// 伪造拔出状态
        /// </summary>
        public ICommandResult Unplug()
        {
            return executor.AdbShell(device, $"dumpsys battery unplug");
        }
        /// <summary>
        /// 伪造插入状态
        /// </summary>
        public ICommandResult Plug()
        {
            return executor.AdbShell(device, $"dumpsys battery plug");
        }
    }
}

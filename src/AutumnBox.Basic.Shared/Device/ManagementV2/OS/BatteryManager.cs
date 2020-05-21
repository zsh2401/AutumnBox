using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Exceptions;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device.ManagementV2.OS
{

    /// <summary>
    /// 电量管理器
    /// </summary>
    public sealed class BatteryManager
    {
        static readonly Regex regex = new Regex(@"(?<key>.+):[^\n|^\d|^\w]+(?<value>[\w|\d]+)", RegexOptions.Multiline | RegexOptions.Compiled);
        readonly IDevice device;
        readonly ICommandExecutor executor;

        /// <summary>
        /// 构造电量管理器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="executor"></param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        public BatteryManager(IDevice device, ICommandExecutor executor)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        /// <summary>
        /// 获取完整的电池信息
        /// </summary>
        /// <exception cref="CommandErrorException">命令无法正常执行</exception>
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

        /// <summary>
        /// 尝试获取int值
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 尝试获取布尔值
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
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
        /// <returns>命令执行结果</returns>
        public CommandResult SetChargeState(bool isCharging = true)
        {
            int status = isCharging ? 2 : 1;
            return executor.AdbShell(device, $"dumpsys battery set status {status}");
        }

        /// <summary>
        /// 伪造电池电量
        /// </summary>
        /// <param name="level"></param>
        /// <returns>命令执行结果</returns>
        public CommandResult SetBatteryLevel(int level)
        {
            if (level <= 0) level = 0;
            else if (level > 100) level = 100;
            return executor.AdbShell(device, $"dumpsys battery set level {level}");
        }

        /// <summary>
        /// 伪造拔出状态
        /// </summary>
        /// <returns>命令执行结果</returns>
        public CommandResult Unplug()
        {
            return executor.AdbShell(device, $"dumpsys battery unplug");
        }

        /// <summary>
        /// 伪造插入状态
        /// </summary>
        /// <returns>命令执行结果</returns>
        public CommandResult Plug()
        {
            return executor.AdbShell(device, $"dumpsys battery plug");
        }
    }
}

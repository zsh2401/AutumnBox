/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/25 22:23:30
** filename: DeviceHardwareInfoGetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Logging;
using System;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device.ManagementV2.OS
{
    /// <summary>
    /// 设备硬件信息获取器
    /// </summary>
    public class HardwareInfoGetter
    {
        private readonly ILogger logger;
        private readonly IDevice device;
        private readonly ICommandExecutor executor;

        /// <summary>
        /// 构造
        /// </summary>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        public HardwareInfoGetter(IDevice device, ICommandExecutor executor)
        {
            logger = LoggerFactory.Auto<HardwareInfoGetter>();
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        public HardwareInfo Get()
        {
            HardwareInfo result = new HardwareInfo
            {
                Serial = device.SerialNumber,
                BatteryLevel = GetBatteryLevel(),
                SizeofRam = SizeofRam(),
                SizeofRom = SizeofRom(),
                ScreenInfo = GetScreenInfo(),
                SOCInfo = GetSocInfo(),
                FlashMemoryType = GetFlashMemoryType(),
            };
            return result;
        }
        /// <summary>
        /// 获取剩余电量
        /// </summary>
        /// <returns></returns>
        public int? GetBatteryLevel()
        {
            try
            {
                string output = executor.AdbShell(device, "dumpsys battery | grep level").Output.LineAll[0];
                return Convert.ToInt32(output.Split(':')[1].TrimStart());
            }
            catch (Exception e)
            {
                logger.Debug($"Get Battery info fail {e}");
                return null;
            }
        }
        /// <summary>
        /// 获取设备默认Dpi
        /// </summary>
        /// <returns></returns>
        public int? GetDpi()
        {
            var displayInfo = executor.AdbShell(device, "dumpsys display | grep mBaseDisplayInfo").Output.LineAll[0];
            var match = Regex.Match(displayInfo, @"^.+\bdensity\b.(?<dpi>[\d]{3}).\(.+$");
            try
            {
                return Convert.ToInt32(match.Result("${dpi}"));
            }
            catch (Exception e)
            {
                logger.Debug($"Get Battery info fail {e}");
                return null;
            }
        }
        /// <summary>
        /// 获取设备RAM大小
        /// </summary>
        /// <returns></returns>
        public double? SizeofRam()
        {
            try
            {
                string output = executor.AdbShell(device, "cat /proc/meminfo | grep MemTotal").Output.LineOut[0];
                var m = Regex.Match(output, @"(?i)^\w+:[\t|\u0020]+(?<size>[\d]+).+$");
                if (m.Success)
                {
                    double _gbMem = (Convert.ToDouble(m.Result("${size}")) / 1000.0 / 1000.0);
                    return Math.Round(_gbMem, MidpointRounding.AwayFromZero);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                logger.Debug($"Get size of ram info fail {e}");
                return null;
            }
        }
        /// <summary>
        /// 获取设备ROM大小
        /// </summary>
        /// <returns></returns>
        public double? SizeofRom()
        {
            try
            {
                var o = executor.AdbShell(device, "df /sdcard/").Output;
                var match = Regex.Match(o.All.ToString(), @"^.[\w|/]+[\t|\u0020]+(?<size>\d+\.?\d+G?) .+$", RegexOptions.Multiline);
                string result = match.Result("${size}");
                if (match.Success)
                {
                    if (result.ToLower().Contains("g"))
                    {
                        return Convert.ToDouble(result.Remove(result.Length - 1, 1));
                    }
                    else
                    {
                        return Math.Round(Convert.ToInt32(result) / 1024.0 / 1024.0, 2);
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e) { logger.Debug("get storage fail " + e); return null; }
        }
        /// <summary>
        /// 获取设备SOC信息
        /// </summary>
        /// <returns></returns>
        public string GetSocInfo()
        {
            try
            {
                string output = executor.AdbShell(device, "getprop ro.product.board").Output.LineAll[0];
                var hehe = output.Split(' ');
                return hehe[hehe.Length - 1];
            }
            catch (Exception e) { logger.Debug("Get cpuinfo fail" + e); return null; }
        }
        /// <summary>
        /// 获取设备屏幕信息
        /// </summary>
        /// <returns></returns>
        public string GetScreenInfo()
        {
            try
            {
                string output = executor.AdbShell(device, "cat /proc/hwinfo | grep LCD").Output.LineOut[0];
                return output.Split(':')[1].TrimStart();
            }
            catch (Exception e) { logger.Debug("Get LCD info fail" + e); return null; }
        }
        /// <summary>
        /// 获取设备闪存信息
        /// </summary>
        /// <returns></returns>
        public string GetFlashMemoryType()
        {
            try
            {
                string output = executor.AdbShell(device, "cat /proc/hwinfo | grep EMMC").Output.LineOut[0];
                return output.Split(':')[1].TrimStart() + " EMMC";
            }
            catch (Exception e) { logger.Debug("Get EMMC info fail" + e); }
            try
            {
                string output = executor.AdbShell(device, "cat /proc/hwinfo | grep UFS").Output.LineOut[0];
                return output.Split(':')[1].TrimStart() + " UFS";
            }
            catch (Exception e) { logger.Debug("Get UFS info fail" + e); return null; }
        }
    }
}

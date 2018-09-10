/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/25 22:23:30
** filename: DeviceHardwareInfoGetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Support.Log;
using System;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device.Management.Hardware
{
    /// <summary>
    /// 设备硬件信息获取器
    /// </summary>
    public class DeviceHardwareInfoGetter : DependOnDeviceObject, IHardwareInfoGetter
    {
        public DeviceHardwareInfoGetter(IDevice device) : base(device)
        {
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        public DeviceHardwareInfo Get()
        {
            DeviceHardwareInfo result = new DeviceHardwareInfo
            {
                Serial = Device.SerialNumber,
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
                string output = Device.Shell("dumpsys battery | grep level").Item1.LineAll[0];
                return Convert.ToInt32(output.Split(':')[1].TrimStart());
            }
            catch (Exception e)
            {
                Logger.Warn(this, "Get Battery info fail", e);
                return null;
            }
        }
        /// <summary>
        /// 获取设备默认Dpi
        /// </summary>
        /// <returns></returns>
        public int? GetDpi()
        {
            var displayInfo = Device.Shell("dumpsys display | grep mBaseDisplayInfo").Item1.LineAll[0];
            var match = Regex.Match(displayInfo, @"^.+\bdensity\b.(?<dpi>[\d]{3}).\(.+$");
            try
            {
                return Convert.ToInt32(match.Result("${dpi}"));
            }
            catch
            {
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
                string output = Device.Shell("cat /proc/meminfo | grep MemTotal").Item1.LineOut[0];
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
                Logger.Warn(this, "Get MemTotal fail", e);
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
                var o = Device.Shell("df /sdcard/").Item1;
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
            catch (Exception e) { Logger.Warn(this, "get storage fail ", e); return null; }
        }
        /// <summary>
        /// 获取设备SOC信息
        /// </summary>
        /// <returns></returns>
        public string GetSocInfo()
        {
            try
            {
                string output = Device.Shell("getprop ro.product.board").Item1.LineAll[0];
                var hehe = output.Split(' ');
                return hehe[hehe.Length - 1];
            }
            catch (Exception e) { Logger.Warn(this, "Get cpuinfo fail", e); return null; }
        }
        /// <summary>
        /// 获取设备屏幕信息
        /// </summary>
        /// <returns></returns>
        public string GetScreenInfo()
        {
            try
            {
                string output = Device.Shell("cat /proc/hwinfo | grep LCD").Item1.LineOut[0];
                return output.Split(':')[1].TrimStart();
            }
            catch (Exception e) { Logger.Warn(this, "Get LCD info fail", e); return null; }
        }
        /// <summary>
        /// 获取设备闪存信息
        /// </summary>
        /// <returns></returns>
        public string GetFlashMemoryType()
        {
            try
            {
                string output = Device.Shell("cat /proc/hwinfo | grep EMMC").Item1.LineOut[0];
                return output.Split(':')[1].TrimStart() + " EMMC";
            }
            catch (Exception e) { Logger.Warn(this, "Get EMMC info fail", e); }
            try
            {
                string output = Device.Shell("cat /proc/hwinfo | grep UFS").Item1.LineOut[0];
                return output.Split(':')[1].TrimStart() + " UFS";
            }
            catch (Exception e) { Logger.Warn(this, "Get UFS info fail", e); return null; }
        }
    }
}

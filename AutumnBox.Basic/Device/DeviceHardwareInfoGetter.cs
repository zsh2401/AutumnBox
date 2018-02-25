/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/25 22:23:30
** filename: DeviceHardwareInfoGetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Util;
using AutumnBox.Support.Log;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device
{
    public class DeviceHardwareInfoGetter
    {
        private static readonly CommandExecuter executer;
        static DeviceHardwareInfoGetter()
        {
            executer = new CommandExecuter();
        }
        private readonly DeviceSerial serial;
        public DeviceHardwareInfoGetter(DeviceSerial deviceSerial)
        {
            this.serial = deviceSerial;
        }
        public DeviceHardwareInfo Get()
        {
            DeviceHardwareInfo result = new DeviceHardwareInfo
            {
                Serial = this.serial,
                BatteryLevel = GetBatteryLevel(),
                SizeofRam = SizeofRam(),
                SizeofRom = SizeofRom(),
                ScreenInfo = GetScreenInfo(),
                SOCInfo = GetSocInfo(),
                FlashMemoryType = GetFlashMemoryType(),
            };
            return result;
        }
        public int? GetBatteryLevel()
        {
            try
            {
                string output = (executer.QuicklyShell(serial, "dumpsys battery | grep level").LineOut[0]);
                return Convert.ToInt32(output.Split(':')[1].TrimStart());
            }
            catch (Exception e)
            {
                Logger.Warn(this,"Get Battery info fail", e);
                return null;
            }
        }
        public int? GetDpi()
        {
            var displayInfo = executer.QuicklyShell(serial, "dumpsys display | grep mBaseDisplayInfo").LineAll[0];
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
        public double? SizeofRam()
        {
            try
            {
                string output = (executer.QuicklyShell(serial, "cat /proc/meminfo | grep MemTotal").LineOut[0]);
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
                Logger.Warn(this,"Get MemTotal fail", e);
                return null;
            }
        }
        public double? SizeofRom()
        {
            try
            {
                var o = executer.QuicklyShell(serial, "df /sdcard/");
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
            catch (Exception e) { Logger.Warn(this,"get storage fail ", e); return null; }
        }
        public string GetSocInfo()
        {
            try
            {
                string output = (executer.QuicklyShell(serial, "getprop ro.product.board").LineAll[0]);
                var hehe = output.Split(' ');
                return hehe[hehe.Length - 1];
            }
            catch (Exception e) { Logger.Warn(this,"Get cpuinfo fail", e); return null; }
        }
        public string GetScreenInfo()
        {
            try
            {
                string output = executer.QuicklyShell(serial, "cat /proc/hwinfo | grep LCD").LineOut[0];
                return output.Split(':')[1].TrimStart();
            }
            catch (Exception e) { Logger.Warn(this,"Get LCD info fail", e); return null; }
        }
        public string GetFlashMemoryType()
        {
            try
            {
                string output = (executer.QuicklyShell(serial, "cat /proc/hwinfo | grep EMMC")).LineOut[0];
                return output.Split(':')[1].TrimStart() + " EMMC";
            }
            catch (Exception e) { Logger.Warn(this,"Get EMMC info fail", e); }
            try
            {
                string output = executer.QuicklyShell(serial, "cat /proc/hwinfo | grep UFS").LineOut[0];
                return output.Split(':')[1].TrimStart() + " UFS";
            }
            catch (Exception e) { Logger.Warn(this,"Get UFS info fail", e); return null; }
        }
    }
}

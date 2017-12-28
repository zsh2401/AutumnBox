/* =============================================================================*\
*
* Filename: DeviceeInfoHelper
* Description: 
*
* Version: 1.0
* Created: 2017/10/24 17:29:58 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Connection;
using AutumnBox.Basic.Executer;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Devices
{
    [LogProperty(TAG = "DeviceInfoHelper")]
    public static class DeviceInfoHelper
    {
        private static Executer.CommandExecuter Executer = new Executer.CommandExecuter();
        public static bool CheckRoot(Serial id)
        {
            using (AndroidShell shell = new AndroidShell(id))
            {
                shell.Connect();
                return shell.Switch2Su();
            }
        }
        public static DeviceHardwareInfo GetHwInfo(Serial serial)
        {
            return new DeviceHardwareInfo()
            {
                Serial = serial,
                BatteryLevel = GetBatteryLevel(serial),
                MemTotal = GetDevMemTotal(serial),
                StorageTotal = GetStorageTotal(serial),
                SOCInfo = GetSocInfo(serial),
                ScreenInfo = GetScreenInfo(serial),
                FlashMemoryType = GetFlashMemoryType(serial),
            };
        }
        public static DeviceState GetState(Serial serial)
        {
            return Executer.Execute(Command.MakeForAdb(serial, "get-state")).Output.All.ToString().ToDeviceState();
        }
        public static int? GetDpi(Serial serial)
        {
            var displayInfo = Executer.Execute(Command.MakeForAdb(serial, "shell \"dumpsys display | grep mBaseDisplayInfo\"")).Output.LineAll[0];
            Logger.D(displayInfo);
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
        public static int? GetBatteryLevel(Serial serial)
        {
            try
            {
                string output = (Executer.Execute(Command.MakeForAdb(serial, "shell \"dumpsys battery | grep level\"")).Output.LineOut[0]);
                Logger.T("BatteryLevel info  " + output);
                return Convert.ToInt32(output.Split(':')[1].TrimStart());
            }
            catch (Exception e) { Logger.T("Get Battery info fail", e); return null; }
        }
        public static double? GetDevMemTotal(Serial serial)
        {
            try
            {
                string output = (Executer.Execute(Command.MakeForAdb(serial, "shell \"cat /proc/meminfo | grep MemTotal\"")).Output.LineOut[0]);
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
                Logger.T("Get MemTotal fail", e);
                return null;
            }
        }
        public static double? GetStorageTotal(Serial serial)
        {
            try
            {
                var o = Executer.Execute(Command.MakeForAdb(serial, "shell df /sdcard/")).Output;
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
            catch (Exception e) { Logger.T("get storage fail ", e); return null; }
        }
        public static string GetSocInfo(Serial serial)
        {
            try
            {
                string output = (Executer.Execute(Command.MakeForAdb(serial, "shell \"cat /proc/cpuinfo | grep Hardware\"")).Output.LineOut[0]);
                Logger.T("cpuinfo " + output);
                var hehe = output.Split(' ');
                return hehe[hehe.Length - 1];
            }
            catch (Exception e) { Logger.T("Get cpuinfo fail", e); return null; }
        }
        public static bool? IsInstalled(Serial serial, string packageName)
        {
            using (AndroidShell shell = new AndroidShell(serial))
            {
                shell.Connect();
                var result = shell.SafetyInput($"pm path {packageName}");
                if (result.ReturnCode == 127) return null;
                return result.ReturnCode == 0;
            }
        }
        public static string GetScreenInfo(Serial serial)
        {
            try
            {
                string output = (Executer.Execute(Command.MakeForAdb(serial, "shell \"cat /proc/hwinfo | grep LCD\"")).Output.LineOut[0]);
                Logger.T("hwinfo LCD " + output);
                return output.Split(':')[1].TrimStart();
            }
            catch (Exception e) { Logger.T("Get LCD info fail", e); return null; }
        }
        public static string GetFlashMemoryType(Serial serial)
        {
            try
            {
                string output = (Executer.Execute(Command.MakeForAdb(serial, "shell \"cat /proc/hwinfo | grep EMMC\"")).Output.LineOut[0]);
                Logger.T("EMMC info  " + output);
                return output.Split(':')[1].TrimStart() + " EMMC";
            }
            catch (Exception e) { Logger.T("Get EMMC info fail", e); }
            try
            {
                string output = (Executer.Execute(Command.MakeForAdb(serial, "shell \"cat /proc/hwinfo | grep UFS\"")).Output.LineOut[0]);
                Logger.T("UFS info  " + output);
                return output.Split(':')[1].TrimStart() + " UFS";
            }
            catch (Exception e) { Logger.T("Get UFS info fail", e); return null; }
        }
    }
}

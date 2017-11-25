/* =============================================================================*\
*
* Filename: DeviceInfoGetter
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
using AutumnBox.Basic.Executer;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Devices
{
    [LogProperty(TAG = "DeviceInfoHelper")]
    public static class DeviceInfoHelper
    {
        private static CExecuter Executer = new CExecuter();
        public static bool CheckRoot(string id)
        {
            using (AndroidShell shell = new AndroidShell(id)) {
                shell.Connect();
                return shell.Switch2Su();
            }
        }
        public static DeviceHardwareInfo GetHwInfo(string id)
        {
            return new DeviceHardwareInfo()
            {
                ID = id,
                BatteryLevel = GetBatteryLevel(id),
                MemTotal = GetDevMemTotal(id),
                StorageTotal = GetStorageTotal(id),
                SOCInfo = GetSocInfo(id),
                ScreenInfo = GetScreenInfo(id),
                FlashMemoryType = GetFlashMemoryType(id),
            };
        }
        public static DeviceBuildInfo GetBuildInfo(string id)
        {
            Hashtable ht = new Hashtable();
            DeviceBuildInfo buildInfo = new DeviceBuildInfo
            {
                Id = id
            };
            var executer = new CExecuter();
            var o = executer.Execute(Command.MakeForAdb(id, "\"cat /system/build.prop\"")).All;

            try
            {
                var output = executer.Execute(Command.MakeForAdb(id, "shell \"cat /system/build.prop | grep \"product.name\"\""));
                Logger.D(output.All.ToString());
                buildInfo.Code = output.LineOut[0].Split('=')[1];
            }
            catch { }

            try { buildInfo.Brand = executer.Execute(Command.MakeForAdb(id, "shell \"cat /system/build.prop | grep \"product.brand\"\"")).LineOut[0].Split('=')[1]; }
            catch { }

            try { buildInfo.AndroidVersion = executer.Execute(Command.MakeForAdb(id, "shell \"cat /system/build.prop | grep \"build.version.release\"\"")).LineOut[0].Split('=')[1]; }
            catch { }

            try { buildInfo.Model = executer.Execute(Command.MakeForAdb(id, "shell \"cat /system/build.prop | grep \"product.model\"\"")).LineOut[0].Split('=')[1]; }
            catch { }

            return buildInfo;
        }
        public static DeviceBasicInfo GetBasicInfo(string id)
        {
            return new DeviceBasicInfo() { Id = id, Status = GetStatus(id) };
        }
        public static DeviceStatus GetStatus(string id)
        {
            DeviceStatus status = DeviceStatus.None;
            DevicesHelper.GetDevices().ForEach((i) =>
            {
                if (i.Id == id)
                {
                    status = i.Status;
                }
            });
            return status;
        }
        [LogProperty(TAG = "displayinfo get")]
        public static int? GetDpi(string id)
        {
            var displayInfo = Executer.Execute(Command.MakeForAdb(id, "shell \"dumpsys display | grep mBaseDisplayInfo\"")).LineAll[0];
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
        public static int? GetBatteryLevel(string id)
        {
            try
            {
                string output = (Executer.Execute(Command.MakeForAdb(id, "shell \"dumpsys battery | grep level\"")).LineOut[0]);
                Logger.T("BatteryLevel info  " + output);
                return Convert.ToInt32(output.Split(':')[1].TrimStart());
            }
            catch (Exception e) { Logger.T("Get Battery info fail", e); return null; }
        }
        public static double? GetDevMemTotal(string id)
        {
            try
            {
                string output = (Executer.Execute(Command.MakeForAdb(id, "shell \"cat /proc/meminfo | grep MemTotal\"")).LineOut[0]);
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
        public static double? GetStorageTotal(string id)
        {
            try
            {
                var o = Executer.AdbExecute(id, "shell df /sdcard/");
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
        public static string GetSocInfo(string id)
        {
            try
            {
                string output = (Executer.Execute(Command.MakeForAdb(id, "shell \"cat /proc/cpuinfo | grep Hardware\"")).LineOut[0]);
                Logger.T("cpuinfo " + output);
                var hehe = output.Split(' ');
                return hehe[hehe.Length - 1];
            }
            catch (Exception e) { Logger.T("Get cpuinfo fail", e); return null; }
        }
        public static string GetScreenInfo(string id)
        {
            try
            {
                string output = (Executer.Execute(Command.MakeForAdb(id, "shell \"cat /proc/hwinfo | grep LCD\"")).LineOut[0]);
                Logger.T("hwinfo LCD " + output);
                return output.Split(':')[1].TrimStart();
            }
            catch (Exception e) { Logger.T("Get LCD info fail", e); return null; }
        }
        public static string GetFlashMemoryType(string id)
        {
            try
            {
                string output = (Executer.Execute(Command.MakeForAdb(id, "shell \"cat /proc/hwinfo | grep EMMC\"")).LineOut[0]);
                Logger.T("EMMC info  " + output);
                return output.Split(':')[1].TrimStart() + " EMMC";
            }
            catch (Exception e) { Logger.T("Get EMMC info fail", e); }
            try
            {
                string output = (Executer.Execute(Command.MakeForAdb(id, "shell \"cat /proc/hwinfo | grep UFS\"")).LineOut[0]);
                Logger.T("UFS info  " + output);
                return output.Split(':')[1].TrimStart() + " UFS";
            }
            catch (Exception e) { Logger.T("Get UFS info fail", e); return null; }
        }
        /// <summary>
        /// 将string的状态转为DevicesStatus枚举
        /// </summary>
        /// <param name="statusString"></param>
        /// <returns></returns>
        public static DeviceStatus StringStatusToEnumStatus(string statusString)
        {
            switch (statusString)
            {
                case "device":
                    return DeviceStatus.Poweron;
                case "recovery":
                    return DeviceStatus.Recovery;
                case "fastboot":
                    return DeviceStatus.Fastboot;
                case "sideload":
                    return DeviceStatus.Sideload;
                case "unauthorized":
                    return DeviceStatus.Unauthorized;
                default:
                    return DeviceStatus.None;
            }
        }
    }
}

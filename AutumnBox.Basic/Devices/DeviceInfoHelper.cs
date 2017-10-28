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
using AutumnBox.Shared;
using AutumnBox.Shared.CstmDebug;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutumnBox.Basic.Debug;
namespace AutumnBox.Basic.Devices
{
    public static class DeviceInfoHelper
    {
        [LogSenderPropAttribute(TAG = "DeviceInfoHelper")]
        public class DeviceInfoSender { }
        private static readonly object sender = new DeviceInfoSender();
        private static CommandExecuter Executer = new CommandExecuter();
        static DeviceInfoHelper()
        {
            CommandExecuter.Start();
        }
        public static bool CheckRoot(string id)
        {
            lock (Executer)
            {
                var o = Executer.AdbExecute(id, "shell su ls");
                Logger.D(sender, o.All.ToString());
                if (o.All.ToString().Contains("not found"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
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
            var executer = new CommandExecuter();

            try { buildInfo.Code = executer.Execute(new Command(id, "shell \"cat /system/build.prop | grep \"product.name\"\"")).LineOut[0].Split('=')[1]; }
            catch { }

            try { buildInfo.Brand = executer.Execute(new Command(id, "shell \"cat /system/build.prop | grep \"product.brand\"\"")).LineOut[0].Split('=')[1]; }
            catch { }

            try { buildInfo.AndroidVersion = executer.Execute(new Command(id, "shell \"cat /system/build.prop | grep \"build.version.release\"\"")).LineOut[0].Split('=')[1]; }
            catch { }

            try { buildInfo.Model = executer.Execute(new Command(id, "shell \"cat /system/build.prop | grep \"product.model\"\"")).LineOut[0].Split('=')[1]; }
            catch { }

            return buildInfo;
        }
        public static DeviceBasicInfo GetBasicInfo(string id)
        {
            return new DeviceBasicInfo() { Id = id, Status = GetStatus(id) };
        }
        public static DeviceStatus GetStatus(string id)
        {
            DeviceStatus status = DeviceStatus.NO_DEVICE;
            DevicesHelper.GetDevices().ForEach((i) =>
            {
                if (i.Id == id)
                {
                    status = i.Status;
                }
            });
            return status;
        }
        public static int? GetDpi(string id)
        {
            return null;
        }
        public static int? GetBatteryLevel(string id)
        {
            try
            {
                string output = (Executer.Execute(new Command(id, "shell \"dumpsys battery | grep level\"")).LineOut[0]);
                Logger.T(sender, "BatteryLevel info  " + output);
                return Convert.ToInt32(output.Split(':')[1].TrimStart());
            }
            catch (Exception e) { Logger.T(sender, "Get Battery info fail", e); return null; }
        }
        public static double? GetDevMemTotal(string id)
        {
            try
            {
                string output = (Executer.Execute(new Command(id, "shell \"cat /proc/meminfo | grep MemTotal\"")).LineOut[0]);
                Logger.T(sender, "MemTotal " + output);
                string result = System.Text.RegularExpressions.Regex.Replace(output, @"[^0-9]+", "");
                Logger.T(sender, "MemTotal kb " + result);
                double gbMem = Math.Round((Convert.ToDouble(result) / 1024.0 / 1024.0), MidpointRounding.AwayFromZero);
                Logger.T(sender, "MemTotal gb " + gbMem);
                return gbMem;
            }
            catch (Exception e)
            {
                Logger.T(sender, "Get MemTotal fail", e);
                return null;
            }
        }
        public static int? GetStorageTotal(string id)
        {
            return null;
        }
        public static string GetSocInfo(string id)
        {
            try
            {
                string output = (Executer.Execute(new Command(id, "shell \"cat /proc/cpuinfo | grep Hardware\"")).LineOut[0]);
                Logger.T(sender, "cpuinfo " + output);
                var hehe = output.Split(' ');
                return hehe[hehe.Length - 1];
            }
            catch (Exception e) { Logger.T(sender, "Get cpuinfo fail", e); return null; }
        }
        public static string GetScreenInfo(string id)
        {
            try
            {
                string output = (Executer.Execute(new Command(id, "shell \"cat /proc/hwinfo | grep LCD\"")).LineOut[0]);
                Logger.T(sender, "hwinfo LCD " + output);
                return output.Split(':')[1].TrimStart();
            }
            catch (Exception e) { Logger.T(sender, "Get LCD info fail", e); return null; }
        }
        public static string GetFlashMemoryType(string id)
        {
            try
            {
                string output = (Executer.Execute(new Command(id, "shell \"cat /proc/hwinfo | grep EMMC\"")).LineOut[0]);
                Logger.T(sender, "EMMC info  " + output);
                return output.Split(':')[1].TrimStart() + " EMMC";
            }
            catch (Exception e) { Logger.T(sender, "Get EMMC info fail", e); }
            try
            {
                string output = (Executer.Execute(new Command(id, "shell \"cat /proc/hwinfo | grep UFS\"")).LineOut[0]);
                Logger.T(sender, "UFS info  " + output);
                return output.Split(':')[1].TrimStart() + " UFS";
            }
            catch (Exception e) { Logger.T(sender, "Get UFS info fail", e); return null; }
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
                    return DeviceStatus.RUNNING;
                case "recovery":
                    return DeviceStatus.RECOVERY;
                case "fastboot":
                    return DeviceStatus.FASTBOOT;
                case "sideload":
                    return DeviceStatus.SIDELOAD;
                default:
                    return DeviceStatus.NO_DEVICE;
            }
        }
    }
}

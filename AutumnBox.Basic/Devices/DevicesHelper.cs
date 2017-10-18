/* =============================================================================*\
*
* Filename: DevicesHelper.cs
* Description:  Static Functions About Device(s)
*
* Version: 1.0
* Created: 8/19/2017 03:42:33(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Devices
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Util;
    using System;
    using System.Collections;
    using System.Threading;
    using static Basic.Debug;
    /// <summary>
    /// 关于设备的一些静态函数
    /// </summary>
    public static class DevicesHelper
    {
        private static readonly string TAG = "DevicesHelper";
        private static CommandExecuter executer = new CommandExecuter();
        private static object _ExecuterLocker = new object();
        private static IDevicesGetter _DeviceGetter = new DevicesGetter();
        /// <summary>
        /// 检查指定设备是否有root权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CheckRoot(string id)
        {
            lock (executer)
            {
                var o = executer.AdbExecute(id, "shell su ls");
                Logger.D(TAG, o.All.ToString());
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
        /// <summary>
        /// 获取一个设备的进阶性质信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DeviceHardwareInfo GetDeviceAdvanceInfo(string id)
        {
            DeviceHardwareInfo info = new DeviceHardwareInfo();
            info.ID = id;
            try
            {
                string output = (executer.Execute(new Command(id, "shell \"cat /proc/meminfo | grep MemTotal\"")).LineOut[0]);
                Debug.Logger.T(TAG, "MemTotal " + output);
                string result = System.Text.RegularExpressions.Regex.Replace(output, @"[^0-9]+", "");
                info.MemTotal = Math.Round((Convert.ToDouble(result) / 1024.0 / 1024.0), MidpointRounding.AwayFromZero);
            }
            catch (Exception e) { Logger.T(TAG, "Get MemTotal fail", e); }
            try
            {
                string output = (executer.Execute(new Command(id, "shell \"cat /proc/hwinfo | grep LCD\"")).LineOut[0]);
                Logger.T(TAG, "hwinfo LCD " + output);
                info.ScreenInfo = output.Split(':')[1].TrimStart();
            }
            catch (Exception e) { Logger.T(TAG, "Get LCD info fail", e); }
            try
            {
                string output = (executer.Execute(new Command(id, "shell \"cat /proc/cpuinfo | grep Hardware\"")).LineOut[0]);
                Logger.T(TAG, "cpuinfo " + output);
                var hehe = output.Split(' ');

                info.SOCInfo = hehe[hehe.Length - 1];
            }
            catch (Exception e) { Logger.T(TAG, "Get cpuinfo fail", e); }
            bool GetFlashMemoryInfoSucessful = false;
            try
            {
                string output = (executer.Execute(new Command(id, "shell \"cat /proc/hwinfo | grep EMMC\"")).LineOut[0]);
                Logger.T(TAG, "EMMC info  " + output);
                info.FlashMemoryType = output.Split(':')[1].TrimStart() + " EMMC";
                GetFlashMemoryInfoSucessful = true;
            }
            catch (Exception e) { Logger.T(TAG, "Get EMMC info fail", e); }
            try
            {
                if (GetFlashMemoryInfoSucessful == false)
                {
                    string output = (executer.Execute(new Command(id, "shell \"cat /proc/hwinfo | grep UFS\"")).LineOut[0]);
                    Logger.T(TAG, "UFS info  " + output);
                    info.FlashMemoryType = output.Split(':')[1].TrimStart() + " UFS";
                    GetFlashMemoryInfoSucessful = true;
                }
            }
            catch (Exception e) { Logger.T(TAG, "Get UFS info fail", e); }
            try
            {
                string output = (executer.Execute(new Command(id, "shell \"dumpsys battery | grep level\"")).LineOut[0]);
                Logger.T(TAG, "BatteryLevel info  " + output);
                info.BatteryLevel = Convert.ToInt32(output.Split(':')[1].TrimStart());
            }
            catch (Exception e) { Logger.T(TAG, "Get Battery info fail", e); }
            return info;
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
        /// <summary>
        /// 获取设备状态
        /// </summary>
        /// <param name="id">设备id</param>
        /// <returns>设备状态</returns>
        public static DeviceStatus GetDeviceStatus(string id)
        {
            DeviceStatus status = DeviceStatus.NO_DEVICE;
            GetDevices().ForEach((i) =>
            {
                if (i.Id == id)
                {
                    status = i.Status;
                }
            });
            return status;
        }
        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <returns>设备列表</returns>
        public static DevicesList GetDevices()
        {
            using (DevicesGetter getter = new DevicesGetter())
            {
                return getter.GetDevices();
            }
        }
        /// <summary>
        /// 获取一个设备的信息
        /// </summary>
        /// <param name="id">设备的id</param>
        /// <returns>设备信息</returns>
        public static DeviceBuildInfo GetDeviceInfo(string id)
        {
            Hashtable ht = GetBuildInfo(id);
            return new DeviceBuildInfo
            {
                Brand = ht["brand"].ToString(),
                Code = ht["name"].ToString(),
                AndroidVersion = ht["androidVersion"].ToString(),
                Model = ht["model"].ToString(),
                DeviceStatus = GetDeviceStatus(id),
                Id = id
            };
        }
        /// <summary>
        /// 获取一个设备的信息
        /// </summary>
        /// <param name="id">设备的id</param>
        /// <returns>设备信息</returns>
        public static DeviceBuildInfo GetDeviceInfo(DeviceBasicInfo info)
        {
            var buildInfo = GetBuildInfo(info.Id);
            return new DeviceBuildInfo
            {
                Brand = buildInfo["brand"].ToString(),
                Code = buildInfo["name"].ToString(),
                AndroidVersion = buildInfo["androidVersion"].ToString(),
                Model = buildInfo["model"].ToString(),
                DeviceStatus = info.Status,
                Id = info.Id,
            };
        }
        /// <summary>
        /// 获取设备的build信息
        /// </summary>
        /// <param name="id">设备id</param>
        /// <returns>设备build信息</returns>
        public static Hashtable GetBuildInfo(string id)
        {
            Hashtable ht = new Hashtable();
            var executer = new CommandExecuter();

            try { ht.Add("name", executer.Execute(new Command(id, "shell \"cat /system/build.prop | grep \"product.name\"\"")).LineOut[0].Split('=')[1]); }
            catch { ht.Add("name", ".."); }

            try { ht.Add("brand", executer.Execute(new Command(id, "shell \"cat /system/build.prop | grep \"product.brand\"\"")).LineOut[0].Split('=')[1]); }
            catch { ht.Add("brand", ".."); }

            try { ht.Add("androidVersion", executer.Execute(new Command(id, "shell \"cat /system/build.prop | grep \"build.version.release\"\"")).LineOut[0].Split('=')[1]); }
            catch { ht.Add("androidVersion", ".."); }
            try { ht.Add("model", executer.Execute(new Command(id, "shell \"cat /system/build.prop | grep \"product.model\"\"")).LineOut[0].Split('=')[1]); }
            catch { ht.Add("model", ".."); }

            return ht;
        }
    }
}

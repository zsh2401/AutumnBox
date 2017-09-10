/*
 关于设备的各种工具类
 @zsh2401
 2017/9/8
 */
using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Devices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Devices
{
    /// <summary>
    /// 关于设备的一些静态函数
    /// </summary>
    public static class DevicesHelper
    {
        public static DeviceStatus StringStatusToEnumStatus(string statusString) {
            switch (statusString) {
                case "running":
                    return DeviceStatus.RUNNING;
                case "recovery":
                    return DeviceStatus.RECOVERY;
                case "fastboot":
                    return DeviceStatus.FASTBOOT;
                case "sideload":
                    return DeviceStatus.SIDELOAD;
                case "debugging_device":
                    return DeviceStatus.DEBUGGING_DEVICE;
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
            switch (GetDevices()[id].ToString())
            {
                case "device":
                    return DeviceStatus.RUNNING;
                case "recovery":
                    return DeviceStatus.RECOVERY;
                case "fastboot":
                    return DeviceStatus.FASTBOOT;
                case "sideload":
                    return DeviceStatus.SIDELOAD;
                case "debugging_device":
                    return DeviceStatus.DEBUGGING_DEVICE;
                default:
                    return DeviceStatus.NO_DEVICE;
            }
        }
        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <returns>设备列表</returns>
        public static DevicesHashtable GetDevices()
        {
#if DEBUG
            var ls = new Adb().GetDevices() + new Fastboot().GetDevices();
            ls.Add("zsh2401TestDebug", "debugging_device");
            return ls;
#else 
            return new Adb().GetDevices() + new Fastboot().GetDevices();
#endif

        }
        /// <summary>
        /// 获取一个设备的信息
        /// </summary>
        /// <param name="id">设备的id</param>
        /// <returns>设备信息</returns>
        public static DeviceInfo GetDeviceInfo(string id)
        {
            Hashtable ht = GetBuildInfo(id);
            return new DeviceInfo
            {
                brand = ht["brand"].ToString(),
                code = ht["name"].ToString(),
                androidVersion = ht["androidVersion"].ToString(),
                model = ht["model"].ToString(),
                deviceStatus = GetDeviceStatus(id),
                id = id
            };
        }
        /// <summary>
        /// 获取一个设备的信息
        /// </summary>
        /// <param name="id">设备的id</param>
        /// <returns>设备信息</returns>
        public static DeviceInfo GetDeviceInfo(string id,Hashtable buildInfo,DeviceStatus status)
        {
            return new DeviceInfo
            {
                brand = buildInfo["brand"].ToString(),
                code = buildInfo["name"].ToString(),
                androidVersion = buildInfo["androidVersion"].ToString(),
                model = buildInfo["model"].ToString(),
                deviceStatus = status,
                id = id
            };
        }
        /// <summary>
        /// 获取设备的build信息
        /// </summary>
        /// <param name="id">设备id</param>
        /// <returns>设备build信息</returns>
        public static Hashtable GetBuildInfo(string id)
        {
            Adb adb = new Adb();
            Fastboot fastboot = new Fastboot();
            Hashtable ht = new Hashtable();
            try { ht.Add("name", adb.Execute(id, "shell \"cat /system/build.prop | grep \"product.name\"\"").output[0].Split('=')[1]); }
            catch { ht.Add("name", ".."); }

            try { ht.Add("brand", adb.Execute(id, "shell \"cat /system/build.prop | grep \"product.brand\"\"").output[0].Split('=')[1]); }
            catch { ht.Add("brand", ".."); }

            try { ht.Add("androidVersion", adb.Execute(id, "shell \"cat /system/build.prop | grep \"build.version.release\"\"").output[0].Split('=')[1]); }
            catch { ht.Add("androidVersion", ".."); }

            try { ht.Add("model", adb.Execute(id, "shell \"cat /system/build.prop | grep \"product.model\"\"").output[0].Split('=')[1]); }
            catch { ht.Add("model", ".."); }

#if DEBUG
            try { ht.Add("model", adb.Execute(id, "shell \"cat /system/build.prop").output); }
            catch { ht.Add("all", ".."); }
#endif
            return ht;
        }
        /// <summary>
        /// 获取当前连接的所有设备的信息
        /// </summary>
        /// <returns>存储所有设备信息的list</returns>
        public static List<DeviceInfo> GetDevicesInfo()
        {
            return GetDevicesInfo(GetDevices());
        }
        /// <summary>
        /// 获取指定的多个设备的信息
        /// </summary>
        /// <param name="devices">需要获取的设备的列表</param>
        /// <returns>存储所有设备信息的list</returns> 
        public static List<DeviceInfo> GetDevicesInfo(DevicesHashtable devices)
        {
            List<DeviceInfo> result = new List<DeviceInfo>();
            foreach (DictionaryEntry i in devices)
            {
                result.Add(GetDeviceInfo(i.Key.ToString()));
            }
            return result;
        }
    }
}

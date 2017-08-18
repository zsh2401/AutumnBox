using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Devices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic
{
    public static class DevicesTools
    {
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
                default:
                    return DeviceStatus.NO_DEVICE;
            }
        }
        public static DevicesHashtable GetDevices()
        {
            return new Adb().GetDevices() + new Fastboot().GetDevices();
        }
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
        public static List<DeviceInfo> GetDevicesInfo()
        {
            return GetDevicesInfo(GetDevices());
        }
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

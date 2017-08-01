using AutumnBox.Basic.Devices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic
{

    public partial class Core {
        public string GET_FAIL = "...";
        private Hashtable GetBuildInfo(string id)
        {
            Hashtable ht = new Hashtable();
            try
            {
                ht.Add("name",ae($" -s {id} shell \"cat /system/build.prop | grep \"product.name\"\"").output[0].Split('=')[1]);
                ht.Add("brand", ae($" -s {id} shell \"cat /system/build.prop | grep \"product.brand\"\"").output[0].Split('=')[1]);
                ht.Add("androidVersion", ae($" -s {id} shell \"cat /system/build.prop | grep \"build.version.release\"\"").output[0].Split('=')[1]);
                ht.Add("model", ae($" -s {id} shell \"cat /system/build.prop | grep \"product.model\"\"").output[0].Split('=')[1]);
            }
            catch
            {
                ht.Add("name",GET_FAIL);
                ht.Add("brand", GET_FAIL);
                ht.Add("androidVersion", GET_FAIL);
                ht.Add("model",GET_FAIL);
            }
            return ht;
        }
        private DeviceStatus GetDeviceStatus(string id) {
            switch ((at.GetDevices() + ft.GetDevices())[id])
            {
                case "device":
                    return DeviceStatus.RUNNING;
                case "recovery":
                    return DeviceStatus.RECOVERY;
                case "fastboot":
                    return DeviceStatus.FASTBOOT;
                default:
                    return DeviceStatus.NO_DEVICE;
            }
        }
    }
}

using AutumnBox.Basic.Functions;
using AutumnBox.Basic.Util;
using System;
using System.Collections;
using System.Threading;

namespace AutumnBox.Basic.Devices
{
    /// <summary>
    /// 设备连接对象
    /// </summary>
    public sealed class DeviceLink : BaseObject
    {
        public string DeviceID { get; private set; }
        public DeviceInfo DeviceInfo { get; private set; }
        private DeviceLink(string id, DeviceStatus status)
        {
            DeviceID = id;
            if (status != DeviceStatus.FASTBOOT || status != DeviceStatus.DEBUGGING_DEVICE)
                DeviceInfo = DevicesHelper.GetDeviceInfo(id);
        }

        /// <summary>
        /// 获取一个与本连接相关的功能模块托管器
        /// </summary>
        /// <param name="func">功能模块</param>
        /// <returns>托管器</returns>
        public RunningManager InitRM(FunctionModule func) {
            func.DeviceID = this.DeviceID;
            var rm = new RunningManager(func);
            return rm;
        }

        /// <summary>
        /// 获取设备连接实例,如果不传入id,将自动获取第一个设备连接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DeviceLink Create(string id)
        {
            //string _id = id ?? DevicesHelper.GetDevices().GetFristDevice();
            //DeviceStatus _status = status != null ?
            //    DevicesHelper.StringStatusToEnumStatus(status) :
            //    DevicesHelper.GetDeviceStatus(_id);
            DeviceStatus status = DevicesHelper.GetDeviceStatus(id);
            return Create(id, status);
        }
        public static DeviceLink Create(string id, DeviceStatus status)
        {
            return new DeviceLink(id, status);
        }
        public static DeviceLink Create(DictionaryEntry e)
        {
            return Create(e.Key.ToString(), DevicesHelper.StringStatusToEnumStatus(e.Value.ToString()));
        }
        public static DeviceLink Create(DevicesHashtable devices,string id) {
            return Create(id, DevicesHelper.StringStatusToEnumStatus(devices[id].ToString()));
        }
    }
}

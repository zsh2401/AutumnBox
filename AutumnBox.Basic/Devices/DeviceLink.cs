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
        public string DeviceID { get { return _info.Id; } set { _info.Id = value; } }

        public DeviceSimpleInfo Info { get { return _info; } }
        public DeviceSimpleInfo _info = new DeviceSimpleInfo();

        public DeviceInfo DeviceInfo { get { return _deviceInfo; } }
        public DeviceInfo _deviceInfo = new DeviceInfo();

        private DeviceLink(string id, DeviceStatus status)
        {
            Reset(new DeviceSimpleInfo { Id = id, Status = status });
        }

        /// <summary>
        /// 获取一个与本连接相关的功能模块托管器
        /// </summary>
        /// <param name="func">功能模块</param>
        /// <returns>托管器</returns>
        public RunningManager InitRM(FunctionModule func)
        {
            LogD("Init FunctionModule " + func.GetType().Name);
            func.DeviceID = this.DeviceID;
            var rm = new RunningManager(func);
            LogD("Init FunctionModule Finish");
            return rm;
        }
        /// <summary>
        /// 根据新的设备信息重设连接
        /// </summary>
        /// <param name="info"></param>
        public void Reset(DeviceSimpleInfo info)
        {
            _info.Id = info.Id;
            _info.Status = info.Status;
            if (info.Status != DeviceStatus.FASTBOOT)
                _deviceInfo = DevicesHelper.GetDeviceInfo(info.Id, DevicesHelper.GetBuildInfo(info.Id), info.Status);
        }
        /// <summary>
        /// 刷新设备build信息
        /// </summary>
        public void RefreshInfo()
        {
            if (Info.Status != DeviceStatus.FASTBOOT)
                _deviceInfo = DevicesHelper.GetDeviceInfo(Info.Id, DevicesHelper.GetBuildInfo(Info.Id), Info.Status);
        }

        /// <summary>
        /// 获取设备连接实例,如果不传入id,将自动获取第一个设备连接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DeviceLink Create()
        {
            var info = DevicesHelper.GetDevices()[0];
            return Create(info);
        }
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
        public static DeviceLink Create(DeviceSimpleInfo info)
        {
            return Create(info.Id, info.Status);
        }
        public static DeviceLink Create(DictionaryEntry e)
        {
            return Create(e.Key.ToString(), DevicesHelper.StringStatusToEnumStatus(e.Value.ToString()));
        }
        public static DeviceLink Create(DevicesHashtable devices, string id)
        {
            return Create(id, DevicesHelper.StringStatusToEnumStatus(devices[id].ToString()));
        }
    }
}

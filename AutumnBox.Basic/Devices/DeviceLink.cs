namespace AutumnBox.Basic.Devices
{
    using AutumnBox.Basic.Functions;
    using AutumnBox.Basic.Util;
    using System;
    using System.Collections;
    using System.Threading;
    /// <summary>
    /// 设备连接对象
    /// </summary>
    public sealed class DeviceLink : BaseObject
    {
        public enum LinkType {
            USB = 0,
            LOCAL_NET,
        }
        public bool IsOK { get {
                return (DeviceID == null) ? true : false;
            } }
        public string DeviceID { get { return _info.Id; } set { _info.Id = value; } }

        public DeviceSimpleInfo Info { get { return _info; } }
        private DeviceSimpleInfo _info = new DeviceSimpleInfo();

        public DeviceInfo DeviceInfo { get { return _deviceInfo; } }
        private DeviceInfo _deviceInfo = new DeviceInfo();

        private DeviceLink(string id, DeviceStatus status)
        {
            Reset(new DeviceSimpleInfo { Id = id, Status = status });
        }
        private DeviceLink() { }
        [Obsolete("You can try use : RunningManager.Create(AnyLink.Info);")]
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
        [Obsolete("You can try use : RunningManager.Create(AnyLink.Info);")]
        /// <summary>
        /// 获取一个与本连接相关的功能模块托管器
        /// </summary>
        /// <param name="func">功能模块</param>
        /// <returns>托管器</returns>
        public RunningManager GetRunningManager(FunctionModule func)
        {
            LogD("Init FunctionModule " + func.GetType().Name);
            func.DeviceID = this.DeviceID;
            func.DevSimpleInfo = this.Info;
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
            RefreshDeviceInfo();
        }
        /// <summary>
        /// 刷新设备build信息
        /// </summary>
        public void RefreshDeviceInfo()
        {
            if (Info.Status != DeviceStatus.FASTBOOT)
                _deviceInfo = DevicesHelper.GetDeviceInfo(Info.Id, DevicesHelper.GetBuildInfo(Info.Id), Info.Status);
        }

        /// <summary>
        /// 创建一个空的设备连接
        /// </summary>
        /// <returns></returns>
        public static DeviceLink CreateNone() {
            return new DeviceLink();
        }
        /// <summary>
        /// 使用连接列表的第一个设备创建连接实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DeviceLink Create()
        {
            var info = DevicesHelper.GetDevices()[0];
            return Create(info);
        }
        /// <summary>
        /// 使用指定设备创建连接实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DeviceLink Create(string id)
        {
            DeviceStatus status = DevicesHelper.GetDeviceStatus(id);
            return Create(new DeviceSimpleInfo { Id =  id,  Status = status });
        }
        /// <summary>
        /// 通过设备简单信息创建连接,能节约时间
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static DeviceLink Create(DeviceSimpleInfo info)
        {
            return new DeviceLink(info.Id,info.Status);
        }
    }
}

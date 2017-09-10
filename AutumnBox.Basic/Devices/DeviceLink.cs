using AutumnBox.Basic.Functions;
using AutumnBox.Basic.Util;
using System;
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
            if (status != DeviceStatus.DEBUGGING_DEVICE)
                DeviceInfo = DevicesHelper.GetDeviceInfo(id);
        }

        ///// <summary>
        ///// 执行一个需要特殊参数的功能模块
        ///// </summary>
        ///// <param name="functionModule">功能函数</param>
        ///// <returns></returns>
        //public Thread Execute(IArgFunctionModule functionModule)
        //{
        //    return functionModule.Run();
        //}
        ///// <summary>
        ///// 执行一个无需特殊参数的功能模块
        ///// </summary>
        ///// <param name="functionModule"></param>
        ///// <returns></returns>
        //public Thread Execute(INoArgFunctionModule functionModule)
        //{
        //    functionModule.DeviceID = this.DeviceID;
        //    return functionModule.Run();
        //}
        public RunningManager Execute(FunctionModule func)
        {
            //设置设备id
            func.DeviceID = this.DeviceID;
            if (!func.IsFinishEventBound) throw new EventNotBoundException();
            //运行功能模块
            return func.Run();
        }

        /// <summary>
        /// 获取设备连接实例,如果不传入id,将自动获取第一个设备连接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DeviceLink Create(string id = null, string status = null)
        {
            string _id = id ?? DevicesHelper.GetDevices().GetFristDevice();
            DeviceStatus _status = status == null ?
                DevicesHelper.StringStatusToEnumStatus(status) :
                DevicesHelper.GetDeviceStatus(_id);
            //return new DeviceLink(_id)

            return Create(_id, status);
        }
        public static DeviceLink Create(string id, DeviceStatus status)
        {
            return new DeviceLink(id, status);
        }
    }
}

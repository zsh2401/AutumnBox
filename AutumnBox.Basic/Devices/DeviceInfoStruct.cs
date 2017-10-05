using AutumnBox.Basic.Functions;
using AutumnBox.Basic.Functions.RunningManager;
using System;

namespace AutumnBox.Basic.Devices
{
    /// <summary>
    /// 完整的设备信息结构体,主要用于获取设备的详细信息
    /// </summary>
    public struct DeviceInfo
    {
        public string model { internal set; get; }//型号
        public string brand { internal set; get; }//厂商
        public string code { internal set; get; }//代号
        public string id { internal set; get; }//id
        public string m { get { return brand + " " + model; } }
        public string androidVersion { internal set; get; }//安卓版本
        public DeviceStatus deviceStatus { internal set; get; }//设备状态
    }
    /// <summary>
    /// 简单的仅包含设备id和设备状态的结构体,主要用于设备列表 DevicesList
    /// </summary>
    public struct DeviceSimpleInfo{
        public string Id { get; set; }
        public DeviceStatus Status { get; set; }
        public override string ToString()
        {
            return Id;
        }
        public static bool operator ==(DeviceSimpleInfo left, DeviceSimpleInfo right) {
            return (left.Id == right.Id);
        }
        public static bool operator !=(DeviceSimpleInfo left, DeviceSimpleInfo right)
        {
            return !(left.Id == right.Id);
        }
        /// <summary>
        /// 要不是为了消除警告...
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        /// <summary>
        /// 要不是为了消除警告...
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public RunningManager GetRunningManger(FunctionModule fm) {
            return RunningManager.Create(this,fm);
        }
    }
}

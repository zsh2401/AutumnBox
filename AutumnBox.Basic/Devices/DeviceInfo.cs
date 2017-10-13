/* =============================================================================*\
*
* Filename: DeviceInfoStruct.cs
* Description: 
*
* Version: 1.0
* Created: 8/18/2017 22:09:36(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.RunningManager;
using System;

namespace AutumnBox.Basic.Devices
{
    /// <summary>
    /// 完整的设备信息结构体,主要用于获取设备的详细信息
    /// </summary>
    public struct DeviceInfo
    {
        public string Model { internal set; get; }//型号
        public string Brand { internal set; get; }//厂商
        public string Code { internal set; get; }//代号
        public string Id { internal set; get; }//id
        public string M { get { return Brand + " " + Model; } }
        public string AndroidVersion { internal set; get; }//安卓版本
        public DeviceStatus DeviceStatus { internal set; get; }//设备状态
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

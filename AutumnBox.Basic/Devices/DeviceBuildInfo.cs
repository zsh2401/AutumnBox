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
namespace AutumnBox.Basic.Devices
{
    /// <summary>
    /// 完整的设备信息结构体,主要用于获取设备的详细信息
    /// </summary>
    public struct DeviceBuildInfo
    {
        public string Id { internal set; get; }//id
        public string Model { internal set; get; }//型号
        public string Brand { internal set; get; }//厂商
        public string Code { internal set; get; }//代号
        public string M { get { return Brand + " " + Model; } }
        public string AndroidVersion { internal set; get; }//安卓版本
    }
}

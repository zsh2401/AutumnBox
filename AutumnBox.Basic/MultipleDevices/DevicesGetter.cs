/* =============================================================================*\
*
* Filename: DevicesGetter.cs
* Description: 
*
* Version: 1.0
* Created: 9/27/2017 02:12:37(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.MultipleDevices
{
    using AutumnBox.Basic.Device;
    using AutumnBox.Basic.Executer;
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 当前已连接设备获取器
    /// </summary>
    public sealed class DevicesGetter : IDevicesGetter
    {
        private CommandExecuter executer = new CommandExecuter();
        private static readonly Command adbDevicesCmd = Command.MakeForAdb("devices");
        private static readonly Command fbDevicesCmd = Command.MakeForFastboot("devices");
        /// <summary>
        /// 获取已连接的设备
        /// </summary>
        /// <returns></returns>
        public DevicesList GetDevices()
        {
            lock (executer)
            {
                DevicesList devList = new DevicesList();
                var adbDevicesOutput = executer.Execute(adbDevicesCmd).Output;
                var fastbootDevicesOutput = executer.Execute(fbDevicesCmd).Output;
                AdbParse(adbDevicesOutput ,ref devList);
                FastbootParse(fastbootDevicesOutput, ref devList);
                return devList;
            }
        }
        private const string devicePattern = @"(?i)^(?<serial>[^\u0020|^\t]+)[^\w]+(?<status>\w+)[^?!.]$";
        private static readonly Regex _deviceRegex = new Regex(devicePattern, RegexOptions.Multiline);
        private static void AdbParse(OutputData o, ref DevicesList devList)
        {
            var matches = _deviceRegex.Matches(o.ToString());
            foreach (Match match in matches)
            {
                devList.Add(DeviceBasicInfo.Make(
                    match.Result("${serial}"),
                    match.Result("${status}").ToDeviceState()));
            }
        }
        private static void FastbootParse(OutputData o, ref DevicesList devList)
        {
            var matches = _deviceRegex.Matches(o.ToString());
            foreach (Match match in matches)
            {
                devList.Add(DeviceBasicInfo.Make(
                    match.Result("${serial}"),
                    match.Result("${status}").ToDeviceState()));
            }
        }
    }
}

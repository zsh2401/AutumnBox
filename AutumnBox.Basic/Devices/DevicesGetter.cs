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
namespace AutumnBox.Basic.Devices
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Util;
    using AutumnBox.Support.CstmDebug;
    using System;
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    public sealed class DevicesGetter : IDevicesGetter, IDisposable
    {
        private CExecuter executer = new CExecuter();
        private static readonly Command adbDevicesCmd = Command.MakeForAdb("devices");
        private static readonly Command fbDevicesCmd = Command.MakeForFastboot("devices");
        public void Dispose()
        {
            executer.Dispose();
        }
        public DevicesList GetDevices()
        {
            lock (executer)
            {
                
                DevicesList devList = new DevicesList();
                var adbDevicesOutput = executer.Execute(adbDevicesCmd);
                var fastbootDevicesOutput = executer.Execute(fbDevicesCmd);
                AdbParse(adbDevicesOutput ,ref devList);
                FastbootParse(fastbootDevicesOutput, ref devList);
                return devList;
            }
        }
        private static readonly string devicePattern = @"(?i)^(?<serial>[^\u0020|^\t]+)[^\w]+(?<status>\w+)[^?!.]$";
        private static readonly Regex _deviceRegex = new Regex(devicePattern, RegexOptions.Multiline);
        private static void AdbParse(OutputData o, ref DevicesList devList)
        {
            var matches = _deviceRegex.Matches(o.ToString());
            foreach (Match match in matches)
            {
                devList.Add(DeviceBasicInfo.Make(
                    match.Result("${serial}"),
                    match.Result("${status}").ToDeviceStatus()));
            }
        }
        private static void FastbootParse(OutputData o, ref DevicesList devList)
        {
            var matches = _deviceRegex.Matches(o.ToString());
            foreach (Match match in matches)
            {
                devList.Add(DeviceBasicInfo.Make(
                    match.Result("${serial}"),
                    match.Result("${status}").ToDeviceStatus()));
            }
        }
    }
}

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
    using System.Text.RegularExpressions;

    public sealed class DevicesGetter : IDevicesGetter
    {
        private CommandExecuter executer = new CommandExecuter();
        private static readonly Command adbDevicesCmd = Command.MakeForAdb("devices");
        private static readonly Command fbDevicesCmd = Command.MakeForFastboot("devices");
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
        private static readonly string devicePattern = @"(?i)^(?<serial>[^\u0020|^\t]+)[^\w]+(?<status>\w+)[^?!.]$";
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

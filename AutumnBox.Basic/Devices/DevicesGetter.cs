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
    public sealed class DevicesGetter : IDevicesGetter, IDisposable
    {
        private CExecuter executer = new CExecuter();
        public void Dispose()
        {
            executer.Dispose();
        }
        public DevicesList GetDevices()
        {
            lock (executer)
            {
                DevicesList devList = new DevicesList();
                var adbDevicesOutput = executer.Execute(Command.MakeForAdb("devices"));
                AdbPrase(adbDevicesOutput, ref devList);
                var fastbootDevicesOutput = executer.Execute(Command.MakeForFastboot("devices"));
                FastbootParse(fastbootDevicesOutput, ref devList);
                return devList;
            }
        }
        private void AdbPrase(OutputData o, ref DevicesList devList)
        {
            var l = o.LineOut;
            for (int i = 1; i < l.Count - 1; i++)
            {
                try
                {
                    devList.Add(
                        new DeviceBasicInfo
                        {
                            Id = l[i].Split('\t')[0],
                            Status = DeviceInfoHelper.StringStatusToEnumStatus(l[i].Split('\t')[1])
                        });
                }
                catch { }
            }
        }
        private void FastbootParse(OutputData o, ref DevicesList devList)
        {
            var l = o.LineOut;
            for (int i = 0; i < l.Count; i++)
            {
                try
                {
                    devList.Add(
                    new DeviceBasicInfo
                    {
                        Id = l[i].Split('\t')[0],
                        Status = DeviceInfoHelper.StringStatusToEnumStatus(l[i].Split('\t')[1])
                    });
                }
                catch { }
            }
        }
    }
}

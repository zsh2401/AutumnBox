namespace AutumnBox.Basic.Devices
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Util;
    using System.Diagnostics;
    public class DevicesGetter : BaseObject, IDevicesGetter
    {
        private CommandExecuter executer = new CommandExecuter();
        public DevicesList GetDevices()
        {
            if (Process.GetProcessesByName("adb").Length == 0) CommandExecuter.Start();

            DevicesList devList = new DevicesList();
            var adbDevicesOutput = executer.Execute(new Command("devices"));
            AdbPrase(adbDevicesOutput, ref devList);
            var fastbootDevicesOutput = executer.Execute(new Command("devices", ExeType.Fastboot));
            FastbootParse(fastbootDevicesOutput, ref devList);
            return devList;
        }

        private void AdbPrase(OutputData o, ref DevicesList devList)
        {
            var l = o.LineOut;
            for (int i = 1; i < l.Count - 1; i++)
            {
                devList.Add(
                    new DeviceSimpleInfo
                    {
                        Id = l[i].Split('\t')[0],
                        Status = DevicesHelper.StringStatusToEnumStatus(l[i].Split('\t')[1])
                    });
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
                    new DeviceSimpleInfo
                    {
                        Id = l[i].Split('\t')[0],
                        Status = DevicesHelper.StringStatusToEnumStatus(l[i].Split('\t')[1])
                    });
                }
                catch { }
            }
        }
    }
}

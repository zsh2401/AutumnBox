using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Util;
using System.Collections.Generic;
using System.Diagnostics;

namespace AutumnBox.Basic.AdbEnc
{
    /// <summary>
    /// 封装Adb工具
    /// </summary>
    internal class Adb : Cmd, IDevicesGetter, IAdbCommandExecuter
    {
        public Adb() : base()
        {
        }
        public override OutputData Execute(string command)
        {
            return base.Execute(Paths.ADB_TOOLS + " " + command);
        }
        public OutputData Execute(string id, string command)
        {
            return base.Execute(Paths.ADB_TOOLS + $" -s {id} " + command);
        }
        public DevicesHashtable GetDevices()
        {
            if (Process.GetProcessesByName("adb").Length == 0) Execute("start-server");
            List<string> output = Execute(" devices").output;
            DevicesHashtable hs = new DevicesHashtable();
            for (int i = 1; i < output.Count - 2; i++)
            {
                //Logger.D("Adb Device Get", output[i].Split('\t')[0] + output[i].Split('\t')[1]);
                hs.Add(output[i].Split('\t')[0], output[i].Split('\t')[1]);
            }
            return hs;
        }
        public static void KillAdb()
        {
            new Adb().Execute("kill-server");
        }
        public static void RestartAdb()
        {
            KillAdb();
            new Adb().Execute("start-server");
        }
    }
}

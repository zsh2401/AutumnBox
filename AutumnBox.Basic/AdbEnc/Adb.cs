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
        /// <summary>
        /// 使用adb.exe执行命令
        /// </summary>
        /// <param name="command">完整命令</param>
        /// <returns>完整输出</returns>
        public override OutputData Execute(string command)
        {
            return base.Execute(Paths.ADB_TOOLS + " " + command);
        }
        /// <summary>
        /// 使用adb执行命令,但可以更好的指定设备id
        /// </summary>
        /// <param name="id">设备id</param>
        /// <param name="command">未指定设备id的部分命令</param>
        /// <returns></returns>
        public OutputData Execute(string id, string command)
        {
            return Execute($" -s {id} " + command);
        }
        /// <summary>
        /// 获取处于非Fastboot状态下的设备
        /// </summary>
        /// <returns>处于非fastboot状态下的设备列表</returns>
        public DevicesHashtable GetDevices()
        {
            if (Process.GetProcessesByName("adb").Length == 0) Execute("start-server");
            List<string> output = Execute(" devices").output;
            DevicesHashtable hs = new DevicesHashtable();
            for (int i = 1; i < output.Count - 2; i++)
            {
                hs.Add(output[i].Split('\t')[0], output[i].Split('\t')[1]);
            }
            return hs;
        }
        /// <summary>
        /// 杀死系统中的adb进程
        /// </summary>
        public static void KillAdb()
        {
            new Adb().Execute("kill-server");
        }
        /// <summary>
        /// 重启系统中的ADB进程
        /// </summary>
        public static void RestartAdb()
        {
            KillAdb();
            new Adb().Execute("start-server");
        }
    }
}

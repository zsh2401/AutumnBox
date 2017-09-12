using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Util;
using System.Collections.Generic;

namespace AutumnBox.Basic.AdbEnc
{
    /// <summary>
    /// 封装fastboot工具
    /// </summary>
    internal class Fastboot:Cmd,IDevicesGetter, IAdbCommandExecuter
    {
        /// <summary>
        /// 使用fastboot执行命令
        /// </summary>
        /// <param name="command">完整命令</param>
        /// <returns></returns>
        public override OutputData Execute(string command)
        {
            return base.Execute(Paths.FASTBOOT_TOOLS + " " + command);
        }
        /// <summary>
        /// 使用fastboot执行命令,但可以更好的指定设备id
        /// </summary>
        /// <param name="id">设备id</param>
        /// <param name="command">未指定设备id的部分命令</param>
        /// <returns></returns>
        public OutputData Execute(string id,string command)
        {
            return base.Execute(Paths.FASTBOOT_TOOLS + $" -s {id} " + command);
        }
        /// <summary>
        /// 获取处于Fastboot下的设备
        /// </summary>
        /// <returns></returns>
        public DevicesHashtable GetDevices()
        {
            List<string> x = Execute(" devices").output;
            DevicesHashtable hs = new DevicesHashtable();
            for (int i = 0; i < x.Count - 1; i++)
            {
                try
                {
                    hs.Add(x[i].Split('\t')[0], x[i].Split('\t')[1]);
                }
                catch { }
            }
            return hs;
        }
    }
}

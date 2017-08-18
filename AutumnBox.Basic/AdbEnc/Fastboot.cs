using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Other;
using System.Collections.Generic;

namespace AutumnBox.Basic.AdbEnc
{
    /// <summary>
    /// 封装fastboot工具
    /// </summary>
    internal class Fastboot:Cmd,ITools, ICommandExecuter
    {
        public Fastboot():base()
        {
        }
        public new OutputData Execute(string command)
        {
            return base.Execute(Paths.FASTBOOT_TOOLS + " " + command);
        }
        public new OutputData Execute(string id,string command)
        {
            return base.Execute(Paths.FASTBOOT_TOOLS + $" -s {id} " + command);
        }
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

using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Arg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 小米系统解锁器(非BL锁解锁器),操作完成后可以获得完整root权限,前提是,必须是开发版并且已经开启开发版的root权限
    /// </summary>
    class XiaomiSystemUnlocker : Function, IThreadFunctionRunner
    {
        public event EventsHandlers.FinishEventHandler unlockFinish;
        private string TAG = "Xiaomi Bootloader Relock";
        public XiaomiSystemUnlocker():base(FunctionInitType.Adb) {
            mainThread = new Thread(new ParameterizedThreadStart(_Run));
            mainThread.Name = TAG + "thread";
        } 
        public void Run(IArgs args) {
            mainThread.Start(args);
        }
        private void _Run(object args) {
            Args _a = (Args)args;
            adb.Execute(_a.deviceID, "root");
            Thread.Sleep(300);
            OutputData o = adb.Execute(_a.deviceID, "disable - verity");
            Thread.Sleep(300);
            new RebootOperator().Run(new RebootArgs { deviceID = _a.deviceID,rebootOption = RebootOptions.System});
            unlockFinish(o);
        }
    }
}

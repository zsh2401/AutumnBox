using AutumnBox.Basic.Arg;
using AutumnBox.Basic.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 黑域服务激活器
    /// </summary>
    internal sealed class BreventServiceActivator : Function, ICanReturnThreadFunction
    {
        public event EventsHandlers.FinishEventHandler ActivatedFinish;
        public BreventServiceActivator() : base(FunctionInitType.Adb) { }
        /// <summary>
        /// 供外部调用的方法
        /// </summary>
        /// <param name="arg"></param>
        public Thread Run(IArgs arg)
        {
            if (ActivatedFinish == null)
            {
                throw new EventNotBoundException();
            }
            mainThread = new Thread(new ParameterizedThreadStart(_Run));
            mainThread.Name = "Brvent Activator Thread";
            mainThread.Start(arg);
            return mainThread;
        }
        private void _Run(object arg)
        {
            Args _arg = (Args)arg;
            ActivatedFinish(base.adb.Execute(_arg.deviceID, "shell sh /data/data/me.piebridge.brevent/brevent.sh"));
        }
    }
}

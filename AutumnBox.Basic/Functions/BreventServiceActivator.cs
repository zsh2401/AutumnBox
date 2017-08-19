using AutumnBox.Basic.Arg;
using AutumnBox.Basic.DebugTools;
using AutumnBox.Basic.Other;
using Newtonsoft.Json.Linq;
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
        private const string DEFAULT_COMMAND = "shell \"sh /data/data/me.piebridge.brevent/brevent.sh\"";
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
            string c;
            try
            {
                JObject extData = JObject.Parse(Tools.GetHtmlCode(new Guider()["ext"].ToString()));
                c = extData["breventCommand"].ToString();
                ActivatedFinish(base.adb.Execute(_arg.deviceID, c));
            }
            catch(Exception e) {
                Log.d(this.ToString(),"get server brevent command fail");
                Log.d(this.ToString(),e.Message);
                ActivatedFinish(base.adb.Execute(_arg.deviceID, DEFAULT_COMMAND));
            }
        }
    }
}

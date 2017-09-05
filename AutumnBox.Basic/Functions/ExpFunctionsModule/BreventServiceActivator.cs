using AutumnBox.Basic.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Diagnostics;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 黑域服务激活器
    /// </summary>
    public sealed class BreventServiceActivator : FunctionModule, ICanGetRealTimeOut
    {
        private const string DEFAULT_COMMAND = "shell \"sh /data/data/me.piebridge.brevent/brevent.sh\"";

        private static new FunctionRequiredDeviceStatus RequiredDeviceStatus = FunctionRequiredDeviceStatus.Running;

        public event DataReceivedEventHandler OutputDataReceived;

        public BreventServiceActivator() : base(RequiredDeviceStatus)
        {
            var d = this.ToString().Split('.');
            TAG = d[d.Length - 1];
            adb.OutputDataReceived += OnOutputDataReceived;
        }
        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            OutputDataReceived?.Invoke(sender, e);
        }
        protected override void MainMethod()
        {
            string c;
            try
            {
                JObject extData = JObject.Parse(Tools.GetHtmlCode(new Guider()["ext"].ToString()));
                c = extData["breventCommand"].ToString();
                var o = base.adb.Execute(this.DeviceID, c);
                OnFinish(this, new FinishEventArgs() { OutputData = o });
            }
            catch (Exception e)
            {
                Log.d(this.ToString(), "get server brevent command fail");
                Log.d(this.ToString(), e.Message);
                Log.d(this.ToString(), this.DeviceID);
                var o = adb.Execute(DeviceID, DEFAULT_COMMAND);
                OnFinish(this, new FinishEventArgs() { OutputData = o });
            }
        }
    }
}

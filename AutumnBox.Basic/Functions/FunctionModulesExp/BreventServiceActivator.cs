/*
 黑域激活器
 @zsh2401
 2017/9/8
 */
using AutumnBox.Basic.Functions.Event;
using AutumnBox.Basic.Util;
using System.Diagnostics;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 黑域服务激活器
    /// </summary>
    public sealed class BreventServiceActivator : FunctionModule, ICanGetRealTimeOut, IFunctionCanStop
    {
        private const string DEFAULT_COMMAND = "shell \"sh /data/data/me.piebridge.brevent/brevent.sh\"";

        private static new FunctionRequiredDeviceStatus RequiredDeviceStatus = FunctionRequiredDeviceStatus.Running;

        public int CmdProcessPID { get { return adb.Pid; } }

        public event DataReceivedEventHandler OutputDataReceived;
        public event DataReceivedEventHandler ErrorDataReceived;

        public BreventServiceActivator() : base(RequiredDeviceStatus)
        {
            adb.ErrorDataReceived += ErrorDataReceived;
            adb.OutputDataReceived += OnOutputDataReceived;
        }
        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Logger.D(TAG, "Received Data");
            OutputDataReceived?.Invoke(sender, e);
        }
        protected override void MainMethod()
        {
#if !DEBUG
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
#endif
            var o = adb.Execute(DeviceID, DEFAULT_COMMAND);
            OnFinish(this, new FinishEventArgs() { OutputData = o });
        }
    }
}

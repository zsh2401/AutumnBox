/*
 黑域激活器
 @zsh2401
 2017/9/8
 */
using AutumnBox.Basic.Functions.Event;
using AutumnBox.Basic.Functions.Interface;
using AutumnBox.Basic.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using AutumnBox.Basic.Functions.ExecutedResultHandler;
using AutumnBox.Basic.Util.ExecutedResultHandler;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 黑域服务激活器
    /// </summary>
    public sealed class BreventServiceActivator : FunctionModule, IOutAnalysable
    {
        private const string DEFAULT_COMMAND = "shell \"sh /data/data/me.piebridge.brevent/brevent.sh\"";

        private static new FunctionRequiredDeviceStatus RequiredDeviceStatus = FunctionRequiredDeviceStatus.Running;

        //public int CmdProcessPID { internal get { return adb.Pid; } }

        public Handler OutHandler { get; private set; }

        //public event DataReceivedEventHandler OutputDataReceived;
        //public event DataReceivedEventHandler ErrorDataReceived;

        //private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        //{
        //    Logger.D(TAG, "Received Data");
        //    OutputDataReceived?.Invoke(sender, e);
        //}
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
                LogD("get server brevent command fail");
                LogD(e.Message);
                LogD(this.DeviceID);
                var o = adb.Execute(DeviceID, DEFAULT_COMMAND);
                OnFinish(this, new FinishEventArgs() { OutputData = o });
            }
#else
            var o = MainExecuter.Execute(DeviceID, DEFAULT_COMMAND);
            OutHandler = new BreventShOutputHandler(o);
            OnFinish(this, new FinishEventArgs() { OutputData = o });
#endif
        }
    }
}

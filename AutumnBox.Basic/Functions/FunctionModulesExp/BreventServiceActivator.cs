/*
 黑域激活器
 @zsh2401
 2017/9/8
 */
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Functions.Interface;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 黑域服务激活器
    /// </summary>
    public sealed class BreventServiceActivator : FunctionModule, IOutAnalysable
    {
        private const string DEFAULT_COMMAND = "shell \"sh /data/data/me.piebridge.brevent/brevent.sh\"";
        protected override OutputData MainMethod()
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
            var o = executer.Execute(DeviceID, DEFAULT_COMMAND);
            return o;
#endif
        }
    }
}

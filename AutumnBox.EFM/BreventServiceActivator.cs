/*
 黑域激活器
 @zsh2401
 2017/9/8
 */
namespace AutumnBox.Basic.Functions
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Functions.Interface;
    /// <summary>
    /// 黑域服务激活器
    /// </summary>
    public sealed class BreventServiceActivator : FunctionModule, IOutAnalysable
    {
        private const string DEFAULT_COMMAND = "shell \"sh /data/data/me.piebridge.brevent/brevent.sh\"";
        protected override OutputData MainMethod()
        {
             executer.ExecuteWithDevice(DeviceID, DEFAULT_COMMAND,out OutputData o);
            return o;
        }
    }
}

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
    public sealed class BreventServiceActivator : FunctionModule
    {
        private const string DEFAULT_COMMAND = "shell \"sh /data/data/me.piebridge.brevent/brevent.sh\"";
        protected override OutputData MainMethod()
        {
             var o = Ae(DEFAULT_COMMAND);
            return o;
        }
        protected override void HandingOutput(OutputData output, ref ExecuteResult result)
        {
            result = new ExecuteResult(output);
            if (output.Error != null) result.IsSuccessful = false;
            if(output.Out.ToString().ToLower().Contains("warning")) result.IsSuccessful = false;
            base.HandingOutput(output, ref result);
        }
    }
}

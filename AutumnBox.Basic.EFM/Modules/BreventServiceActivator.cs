/* =============================================================================*\
*
* Filename: BreventServiceActivator.cs
* Description: 
*
* Version: 1.0
* Created: 9/22/2017 03:13:12(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
/*
 黑域激活器
 @zsh2401
 2017/9/8
 */
namespace AutumnBox.Basic.Function.Modules
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Function.Args;
    /// <summary>
    /// 黑域服务激活器
    /// </summary>
    public sealed class BreventServiceActivator : FunctionModule
    {
        private const string DEFAULT_COMMAND = "shell \"sh /data/data/me.piebridge.brevent/brevent.sh\"";
        protected override OutputData MainMethod()
        {
            FunctionModuleProxy.Create<ActivityLauncher>(new ActivityLaunchArgs(Args.DeviceBasicInfo)
            { PackageName = "me.piebridge.brevent", ActivityName = ".ui.BreventActivity" }).FastRun();
            var o = Ae(DEFAULT_COMMAND);
            return o;
        }
        protected override void AnalyzeOutput(ref ExecuteResult result)
        {
            base.AnalyzeOutput(ref result);
            if (result.OutputData.Error != null) result.Level = ResultLevel.Unsuccessful;
            if (result.OutputData.All.ToString().ToLower().Contains("warning")) result.Level = ResultLevel.Unsuccessful;
            if (result.OutputData.All.ToString().ToLower().Contains("started")) result.Level = ResultLevel.Successful;
        }
    }
}

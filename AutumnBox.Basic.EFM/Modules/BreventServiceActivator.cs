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
namespace AutumnBox.Basic.Function.Modules
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Function.Args;
    using AutumnBox.Basic.Function.Bundles;
    using AutumnBox.Support.CstmDebug;
    using System;
    using System.Threading;

    [Obsolete("Please use Basic.Flows.BreventServiceActivator",true)]
    /// <summary>
    /// 黑域服务激活器
    /// </summary>
    public sealed class BreventServiceActivator : FunctionModule
    {
        private static readonly string DEFAULT_SHELL_COMMAND = "sh /data/data/me.piebridge.brevent/brevent.sh";
        private bool _exitResult;
        protected override OutputData MainMethod(BundleForTools toolsBundle)
        {
            OutputData o = new OutputData
            {
                OutSender = toolsBundle.Executer
            };
            Logger.D("start brevent activity");
            FunctionModuleProxy.Create<ActivityLauncher>(new ActivityLaunchArgs(toolsBundle.Args.DeviceBasicInfo)
            { PackageName = "me.piebridge.brevent", ActivityName = "ui.BreventActivity" }).SyncRun();
            Logger.D("try to execute command with quicklyshell ");
            Thread.Sleep(2000);
            toolsBundle.Executer.QuicklyShell(toolsBundle.DeviceID, DEFAULT_SHELL_COMMAND, out _exitResult);
            return o;
        }
        protected override void AnalyzeOutput(BundleForAnalyzingResult bundle)
        {
            base.AnalyzeOutput(bundle);
            Logger.D($"shell exitResult?? {_exitResult}");
            if (bundle.Result.OutputData.Error != null) bundle.Result.Level = ResultLevel.Unsuccessful;
            if (bundle.Result.OutputData.All.ToString().ToLower().Contains("warning")) bundle.Result.Level = ResultLevel.Unsuccessful;
            if (bundle.Result.OutputData.All.ToString().ToLower().Contains("started")) bundle.Result.Level = ResultLevel.Successful;
        }
    }
}

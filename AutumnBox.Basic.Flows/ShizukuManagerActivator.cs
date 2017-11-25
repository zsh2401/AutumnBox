/* =============================================================================*\
*
* Filename: ShizukuManagerActivator
* Description: 
*
* Version: 1.0
* Created: 2017/11/25 0:49:03 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.FlowFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework.Args;
using AutumnBox.Basic.FlowFramework.Container;
using AutumnBox.Basic.Flows.Result;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.Basic.Function.Args;
using System.Threading;

namespace AutumnBox.Basic.Flows
{
    public class ShizukuManagerActivator : FunctionFlow<FlowArgs, AdvanceResult>
    {
        private ShellOutput _shellOutput;
        private static readonly string _defaultShell =
            "sh /sdcard/Android/data/moe.shizuku.privileged.api/files/start.sh";
        protected override OutputData MainMethod(ToolKit<FlowArgs> toolKit)
        {
            FunctionModuleProxy.Create<ActivityLauncher>(new ActivityLaunchArgs(toolKit.Args.DevBasicInfo)
            { PackageName = "moe.shizuku.privileged.api", ActivityName = "moe.shizuku.manager.MainActivity" }).SyncRun();
            Thread.Sleep(2000);
            _shellOutput = toolKit.Executer.QuicklyShell(toolKit.Args.DevBasicInfo, _defaultShell);
            return _shellOutput.OutputData;
        }
        protected override void AnalyzeResult(AdvanceResult result)
        {
            base.AnalyzeResult(result);
            result.ShellOutput = _shellOutput;
            result.ResultType =
                _shellOutput.ReturnCode == 0 ?
                FlowFramework.States.ResultType.Successful :
                FlowFramework.States.ResultType.Unsuccessful;
        }
    }
}

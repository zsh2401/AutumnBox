/* =============================================================================*\
*
* Filename: BreventServiceActivator
* Description: 
*
* Version: 1.0
* Created: 2017/11/24 21:15:11 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.FlowFramework.Args;
using AutumnBox.Basic.FlowFramework.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.Basic.Function.Args;
using System.Threading;
using AutumnBox.Basic.Flows.Result;
using AutumnBox.Basic.FlowFramework.States;

namespace AutumnBox.Basic.Flows
{
    public class BreventServiceActivator : FunctionFlow<FlowArgs, BreventServiceActivatorResult>
    {
        private int retCode = 0;
        private ShellOutput _shellOutput;
        private static readonly string _defaultShellCommand = "sh /data/data/me.piebridge.brevent/brevent.sh";
        protected override OutputData MainMethod(ToolKit<FlowArgs> toolKit)
        {
            FunctionModuleProxy.Create<ActivityLauncher>(new ActivityLaunchArgs(toolKit.Args.DevBasicInfo)
            { PackageName = "me.piebridge.brevent", ActivityName = ".ui.BreventActivity" }).SyncRun();
            Thread.Sleep(2000);
            _shellOutput = toolKit.Executer.QuicklyShell(toolKit.Args.DevBasicInfo.Id, _defaultShellCommand);
            retCode = _shellOutput.ReturnCode;
            return _shellOutput.OutputData;
        }
        protected override void AnalyzeResult(BreventServiceActivatorResult result)
        {
            base.AnalyzeResult(result);
            switch (_shellOutput.ReturnCode)
            {
                case 0://No error
                    result.ResultType = ResultType.Successful;
                    result.ErrorType = States.BreventServiceActivatorErrorType.None;
                    break;
                case 1://Error
                    result.ResultType = ResultType.Unsuccessful;
                    result.ErrorType = States.BreventServiceActivatorErrorType.Error;
                    break;
                case 127://Shell not found
                    result.ErrorType = States.BreventServiceActivatorErrorType.ShNotFound | States.BreventServiceActivatorErrorType.ShNotFound;
                    result.ResultType = ResultType.Unsuccessful;
                    break;
                default://Unknow error..
                    result.ErrorType = States.BreventServiceActivatorErrorType.ShNotFound | States.BreventServiceActivatorErrorType.Unknow;
                    result.ResultType = ResultType.MaybeUnsuccessful;
                    break;
            }
        }
    }
}

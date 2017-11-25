/* =============================================================================*\
*
* Filename: IceActivator
* Description: 
*
* Version: 1.0
* Created: 2017/11/25 23:30:07 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.FlowFramework.Args;
using AutumnBox.Basic.Flows.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework.Container;
using AutumnBox.Basic.Flows.States;
using AutumnBox.Basic.FlowFramework.Events;
using AutumnBox.Support.CstmDebug;

namespace AutumnBox.Basic.Flows
{
    public abstract class IceActivator : FunctionFlow<FlowArgs, IceSoftwareResult>
    {
        protected bool NeedReboot { get; } = true;
        protected abstract string _defaultShellCommand { get; }
        protected ShellOutput _shellOutput;
        private ToolKit<FlowArgs> _tooKit;
        protected override OutputData MainMethod(ToolKit<FlowArgs> toolKit)
        {
            _tooKit = toolKit;
            _shellOutput = toolKit.Executer.QuicklyShell(toolKit.Args.DevBasicInfo, _defaultShellCommand);
            return _shellOutput.OutputData;
        }
        [LogProperty(TAG = "IceActivator Analyzing Result",Show = true)]
        protected override void AnalyzeResult(IceSoftwareResult result)
        {
            base.AnalyzeResult(result);
            result.ShellOutput = _shellOutput;
            Logger.T("the return code ->" + result.ShellOutput.ReturnCode);
            switch (result.ShellOutput.ReturnCode)
            {
                case (int)LinuxReturnCodes.NoError:
                    Logger.T("No error");
                    result.ErrorType = IceActivatorErrType.None;
                    result.ResultType = FlowFramework.States.ResultType.Successful;
                    break;
                default:
                    result.ErrorType = IceActivatorErrType.Unknow;
                    result.ResultType = FlowFramework.States.ResultType.Unsuccessful;
                    break;
            }
            if (result.ErrorType == IceActivatorErrType.None) return;
            if (result.OutputData.Contains("already set"))
            {
                result.ResultType = FlowFramework.States.ResultType.MaybeUnsuccessful;
                result.ErrorType = IceActivatorErrType.DeviceOwnerIsAlreadySet;
            }
            else if (result.OutputData.Contains("device is already provisioned"))
            {
                result.ErrorType = IceActivatorErrType.DeviceAlreadyProvisioned;
            }
            else if (result.OutputData.Contains("several user on the device"))
            {
                result.ErrorType = IceActivatorErrType.HaveOtherUser;
            }
            else if (result.OutputData.Contains("exception") && result.OutputData.Contains("java"))
            {
                result.ErrorType = IceActivatorErrType.UnknowJavaException;
            }
        }
        protected override void OnFinished(FinishedEventArgs<IceSoftwareResult> e)
        {
            base.OnFinished(e);
            if (e.Result.ErrorType == IceActivatorErrType.None && NeedReboot)
            {
                _tooKit.Ae("reboot");
            }
        }
    }
}

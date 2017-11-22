/* =============================================================================*\
*
* Filename: IceBoxActivator
* Description: 
*
* Version: 1.0
* Created: 2017/11/15 15:39:59 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Text;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Event;
using AutumnBox.Support.CstmDebug;
using AutumnBox.Basic.Function.Bundles;

namespace AutumnBox.Basic.Function.Modules
{
    public enum ErrorType
    {
        DeviceOwnerIsAlreadySet,
        IceBoxHaveNoInstall
    }
    public sealed class IceBoxActivator : FunctionModule
    {
        private bool _exeResult;
        BundleForTools _bundle;
        private static readonly string _defaultCommand = "dpm set-device-owner com.catchingnow.icebox/.receiver.DPMReceiver";
        protected override OutputData MainMethod(BundleForTools bundle)
        {
            _bundle = bundle;
            var o =
                bundle.Executer.QuicklyShell(bundle.DeviceID, _defaultCommand);
            _exeResult = o.IsSuccess;
            return o.OutputData;
        }
        protected override void AnalyzeOutput(BundleForAnalyzeOutput bundle)
        {
            base.AnalyzeOutput(bundle);
            if (bundle.OutputData.All.ToString().ToLower().Contains("error"))
            {
                bundle.Result.Level = ResultLevel.Unsuccessful;
                bundle.Other = ErrorType.IceBoxHaveNoInstall;
            }
            if (bundle.OutputData.All.ToString().ToLower().Contains("device owner is already set"))
            {
                bundle.Result.Level = ResultLevel.Unsuccessful;
                bundle.Other = ErrorType.DeviceOwnerIsAlreadySet;
            }
            if (_exeResult == false) bundle.Result.Level = ResultLevel.Unsuccessful;
            if (bundle.Result.Level == ResultLevel.Successful) _bundle.Ae("reboot");
        }
    }
}

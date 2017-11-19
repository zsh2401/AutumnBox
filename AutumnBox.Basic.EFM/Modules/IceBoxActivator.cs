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

namespace AutumnBox.Basic.Function.Modules
{
    public sealed class IceBoxActivator : FunctionModule
    {
        private bool _exeResult;
        private static readonly string _defaultCommand = "dpm set-device-owner com.catchingnow.icebox/.receiver.DPMReceiver";
        protected override OutputData MainMethod(ToolsBundle bundle)
        {
            var o =
                bundle.Executer.QuicklyShell(bundle.DeviceID, _defaultCommand, out _exeResult);
            if (_exeResult == true)
            {
                bundle.Ae("reboot");
            }
            return o;
        }
        protected override void AnalyzeOutput(BundleForAnalyzeOutput bundleForAnalyzeOutput)
        {
            base.AnalyzeOutput(bundleForAnalyzeOutput);
            if (_exeResult == false) bundleForAnalyzeOutput.Result.Level = ResultLevel.Unsuccessful;
        }
    }
}

/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/28 1:42:44
** filename: NetDeviceRemover.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    public class NetDeviceRemover : FunctionFlow
    {
        protected override OutputData MainMethod(ToolKit<FlowArgs> toolKit)
        {
            return toolKit.Executer.Execute(Command.MakeForCmd($"{ConstData.FullAdbFileName} ${toolKit.Args.DevBasicInfo.Serial.ToFullSerial()} usb && echo ___errorcode%errorlevel%"));
        }
        protected override void AnalyzeResult(FlowResult result)
        {
            if (!(result.OutputData.Contains("___errorcode0")))
            {
                result.ResultType = ResultType.MaybeUnsuccessful;
            }
            base.AnalyzeResult(result);
        }
    }
}

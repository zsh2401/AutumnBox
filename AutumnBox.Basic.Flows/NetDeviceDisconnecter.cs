/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/28 1:42:44
** filename: NetDeviceRemover.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows.Result;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    public class NetDeviceDisconnecter : FunctionFlow<FlowArgs, AdvanceResult>
    {
        private AdvanceOutput _result;
        protected override Output MainMethod(ToolKit<FlowArgs> toolKit)
        {
            if (!toolKit.Args.DevBasicInfo.Serial.IsIpAdress)
                throw new Exception($"{toolKit.Args.DevBasicInfo.Serial} is not a net debugging device");
            _result = toolKit.Executer.Execute(Command.MakeForAdb($"disconnect {toolKit.Args.DevBasicInfo.Serial.ToString()}"));
            return _result;
        }
        protected override void AnalyzeResult(AdvanceResult result)
        {
            base.AnalyzeResult(result);
            result.ExitCode = _result.ExitCode;
            result.ResultType = _result.IsSuccessful ? ResultType.Successful : ResultType.Unsuccessful;
        }
    }
}

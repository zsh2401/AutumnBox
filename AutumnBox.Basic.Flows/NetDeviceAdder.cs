/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/28 1:34:05
** filename: NetDeviceAdder.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Flows
{
    public class NetDeviceAdderArgs : FlowArgs
    {
        public uint Port { get; set; } = 5555;
    }
    public class NetDeviceAdder : FunctionFlow<NetDeviceAdderArgs, FlowResult>
    {
        protected override OutputData MainMethod(ToolKit<NetDeviceAdderArgs> toolKit)
        {
            return toolKit.Executer.Execute(Command.MakeForCmd($"{ConstData.FullAdbFileName} ${toolKit.Args.DevBasicInfo.Serial.ToFullSerial()} tcpip 5555 && echo ___errorcode%errorlevel%"));
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

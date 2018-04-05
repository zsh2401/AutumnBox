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
using System;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// 网络调试设备连接断开器
    /// </summary>
    public class NetDeviceDisconnecter : FunctionFlow<FlowArgs, AdvanceResult>
    {
        private AdvanceOutput _result;
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
        protected override Output MainMethod(ToolKit<FlowArgs> toolKit)
        {
            if (!toolKit.Args.DevBasicInfo.Serial.IsIpAdress)
                throw new Exception($"{toolKit.Args.DevBasicInfo.Serial} is not a net debugging device");
            _result = toolKit.Executer.Execute(Command.MakeForAdb($"disconnect {toolKit.Args.DevBasicInfo.Serial.ToString()}"));
            return _result;
        }
        /// <summary>
        /// 处理结果
        /// </summary>
        /// <param name="result"></param>
        protected override void AnalyzeResult(AdvanceResult result)
        {
            base.AnalyzeResult(result);
            result.ExitCode = _result.GetExitCode();
            result.ResultType = _result.IsSuccessful ? ResultType.Successful : ResultType.Unsuccessful;
        }
    }
}

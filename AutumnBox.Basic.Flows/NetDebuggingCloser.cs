/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/28 17:52:05
** filename: NetDebuggingCloser.cs
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
    /// 设备网络调试关闭器
    /// </summary>
    public class NetDebuggingCloser : FunctionFlow<FlowArgs, AdvanceResult>
    {
        /// <summary>
        /// 结果
        /// </summary>
        public AdvanceOutput _result;
        /// <summary>
        /// 主函数
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
        protected override Output MainMethod(ToolKit<FlowArgs> toolKit)
        {
            if (!toolKit.Args.DevBasicInfo.Serial.IsIpAdress)
                throw new Exception($"{toolKit.Args.DevBasicInfo.Serial} is not a net debugging device");
            _result = toolKit.Ae("usb");
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

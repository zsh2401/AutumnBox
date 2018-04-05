/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/28 1:34:05
** filename: NetDeviceAdder.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows.Result;
using AutumnBox.Basic.Util;
using System.Net;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// 网络调试设备连接器参数
    /// </summary>
    public sealed class NetDeviceConnecterArgs : FlowArgs
    {
        /// <summary>
        /// 目标IP地址以及端口
        /// </summary>
        public IPEndPoint IPEndPoint { get; set; }
    }
    /// <summary>
    /// 网络调试设备连接器
    /// </summary>
    public sealed class NetDeviceConnecter : FunctionFlow<NetDeviceConnecterArgs, AdvanceResult>
    {
        private AdvanceOutput _result;
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
        protected override Output MainMethod(ToolKit<NetDeviceConnecterArgs> toolKit)
        {
            _result = toolKit.Executer.Execute(Command.MakeForAdb($"connect {toolKit.Args.IPEndPoint.ToString()}"));
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
            if (result.OutputData.Contains("unable")) {
                result.ResultType = ResultType.Unsuccessful;
            }
        }
    }
}

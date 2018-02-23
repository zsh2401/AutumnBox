/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/23 17:29:56 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    public class DpiChangerArgs :FlowArgs{
        public int Dpi { get; set; }
    }
    public class DpiChanger : FunctionFlow<DpiChangerArgs,AdvanceResult>
    {
        int exitCode = 1;
        protected override Output MainMethod(ToolKit<DpiChangerArgs> toolKit)
        {
            var outputBuilder = new OutputBuilder();
            outputBuilder.Register(toolKit.Executer);
            exitCode =  toolKit.Executer.QuicklyShell(toolKit.Args.DevBasicInfo.Serial, $"wm density {toolKit.Args.Dpi}").ExitCode;
            if (exitCode == 0) {
                toolKit.Ae("reboot");
            }
            return outputBuilder.Result;
        }
        protected override void AnalyzeResult(AdvanceResult result)
        {
            base.AnalyzeResult(result);
            result.ExitCode = exitCode;
            result.ResultType = (exitCode == 0)? ResultType.Successful:ResultType.MaybeUnsuccessful;
        }
    }
}

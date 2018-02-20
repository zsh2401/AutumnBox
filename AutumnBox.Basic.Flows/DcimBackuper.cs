/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/17 18:51:58 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    public class DcimBackuperArgs : FlowArgs
    {
        public string TargetPath { get; set; }
    }
    public class DcimBackuper : FunctionFlow<DcimBackuperArgs>
    {
        private AdvanceOutput exeResult;
        protected override CheckResult Check(DcimBackuperArgs args)
        {
            var result =
                (args.DevBasicInfo.State == Device.DeviceState.Poweron ||
                args.DevBasicInfo.State == Device.DeviceState.Recovery) ? CheckResult.OK : CheckResult.DeviceStateError;
            Logger.D(result.ToString());
            return result;
        }
        protected override Output MainMethod(ToolKit<DcimBackuperArgs> toolKit)
        {
            Logger.D(toolKit.Args.TargetPath);
            if (toolKit.Args.DevBasicInfo.State == Device.DeviceState.Poweron)
            {
                exeResult = toolKit.Ae($"pull /sdcard/DCIM/. \"{toolKit.Args.TargetPath}\"");
            }
            else
            {
                exeResult = toolKit.Ae($"pull /data/media/0/DCIM/. \"{toolKit.Args.TargetPath}\"");
            }
            return exeResult;
        }
        protected override void AnalyzeResult(FlowResult result)
        {
            base.AnalyzeResult(result);
            result.ResultType = exeResult.IsSuccessful ? ResultType.Successful : ResultType.Unsuccessful;
        }
    }
}

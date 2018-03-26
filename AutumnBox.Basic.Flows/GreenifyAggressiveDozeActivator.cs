/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/26 23:24:28 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    public class GreenifyAggressiveDozeActivator : FunctionFlow
    {
        public const string AppPackageName = "com.oasisfeng.greenify";
        bool allSuccessful = true;
        protected override Output MainMethod(ToolKit<FlowArgs> toolKit)
        {
            AndroidShellV2 shell =
                new AndroidShellV2(toolKit.Args.DevBasicInfo.Serial);
            AdvanceOutputBuilder builder =
                new AdvanceOutputBuilder();
            Version crtVersion =
                new DeviceBuildPropGetter(toolKit.Args.DevBasicInfo.Serial).GetAndroidVersion();
            builder.Register(shell);
            allSuccessful =
                shell.Execute("pm grant com.oasisfeng.greenify android.permission.WRITE_SECURE_SETTINGS").IsSuccessful;
            allSuccessful =
                shell.Execute("pm grant com.oasisfeng.greenify android.permission.DUMP").IsSuccessful;
            allSuccessful =
                shell.Execute("pm grant com.oasisfeng.greenify android.permission.READ_LOGS").IsSuccessful;
            if (crtVersion?.Major >= 8)
            {
                allSuccessful =
                    shell.Execute("pm grant com.oasisfeng.greenify android.permission.READ_APP_OPS_STATS").IsSuccessful;
            }
            allSuccessful = shell.Execute("am force-stop com.oasisfeng.greenify").IsSuccessful;
            return builder.Result;
        }
        protected override void AnalyzeResult(FlowResult result)
        {
            base.AnalyzeResult(result);
            if (allSuccessful == false)
            {
                result.ResultType = ResultType.MaybeSuccessful;
            }
            if (result.OutputData.Contains("not found"))
            {
                result.ResultType = ResultType.MaybeSuccessful;
            }
        }
    }
}

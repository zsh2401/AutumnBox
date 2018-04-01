/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/26 23:24:28 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using System;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// 绿色守护嗜睡模式设置器
    /// </summary>
    public class GreenifyAggressiveDozeActivator : FunctionFlow
    {
        /// <summary>
        /// 应用包名
        /// </summary>
        public const string AppPackageName = "com.oasisfeng.greenify";
        bool allSuccessful = true;
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 处理结果
        /// </summary>
        /// <param name="result"></param>
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

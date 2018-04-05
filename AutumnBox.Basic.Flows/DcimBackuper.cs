/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/17 18:51:58 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// 相册备份器参数
    /// </summary>
    public class DcimBackuperArgs : FlowArgs
    {
        /// <summary>
        /// 备份到的路径
        /// </summary>
        public string TargetPath { get; set; }
    }
    /// <summary>
    /// 相册备份器
    /// </summary>
    public class DcimBackuper : FunctionFlow<DcimBackuperArgs>
    {
        private AdvanceOutput exeResult;
        /// <summary>
        /// 检查
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override CheckResult Check(DcimBackuperArgs args)
        {
            var result =
                (args.DevBasicInfo.State == Device.DeviceState.Poweron ||
                args.DevBasicInfo.State == Device.DeviceState.Recovery) ? CheckResult.OK : CheckResult.DeviceStateError;
            return result;
        }
        /// <summary>
        /// 主函数
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
        protected override Output MainMethod(ToolKit<DcimBackuperArgs> toolKit)
        {
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
        /// <summary>
        /// 处理结果
        /// </summary>
        /// <param name="result"></param>
        protected override void AnalyzeResult(FlowResult result)
        {
            base.AnalyzeResult(result);
            result.ResultType = exeResult.IsSuccessful ? ResultType.Successful : ResultType.Unsuccessful;
        }
    }
}

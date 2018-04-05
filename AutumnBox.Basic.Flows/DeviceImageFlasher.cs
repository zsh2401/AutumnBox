/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/9 17:08:30
** filename: DeviceImageFlasher.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// 设备镜像刷入器参数
    /// </summary>
    public sealed class DeviceImageFlasherArgs : FlowArgs
    {
        /// <summary>
        /// 刷入的镜像类型
        /// </summary>
        public DeviceImage ImageType { get; set; } = DeviceImage.Recovery;
        /// <summary>
        /// 源镜像路径
        /// </summary>
        public string SourceFile { get; set; }
    }
    /// <summary>
    /// 设备镜像刷入器
    /// </summary>
    public sealed class DeviceImageFlasher : FunctionFlow<DeviceImageFlasherArgs>
    {
        private const string _savePath = "/sdcard/autumnbox.img.tmp";
        private AdvanceOutput moveResult;
        /// <summary>
        /// 主函数
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
        protected override Output MainMethod(ToolKit<DeviceImageFlasherArgs> toolKit)
        {
            var outputBuilder = new AdvanceOutputBuilder();
            /*push image file to sdcard*/
            var pushArgs = new FilePusherArgs()
            {
                DevBasicInfo = toolKit.Args.DevBasicInfo,
                SavePath = _savePath,
                SourceFile = toolKit.Args.SourceFile,
            };
            var pushResult = new FilePusher().Run(pushArgs);
            outputBuilder.Append(pushResult.OutputData);
            /*move file to img path*/
            string path = DeviceImageFinder.PathOf(toolKit.Args.DevBasicInfo.Serial, toolKit.Args.ImageType);
            using (AndroidShell shell = new AndroidShell(toolKit.Args.DevBasicInfo.Serial))
            {
                shell.Connect();
                shell.Switch2Su();
                moveResult = shell.SafetyInput($"mv {_savePath} {path}");
                outputBuilder.Append(moveResult);
            }
            return outputBuilder.Result;
        }
        /// <summary>
        /// 处理结果
        /// </summary>
        /// <param name="result"></param>
        protected override void AnalyzeResult(FlowResult result)
        {
            base.AnalyzeResult(result);
            result.ResultType = moveResult.IsSuccessful ? ResultType.MaybeSuccessful : ResultType.MaybeUnsuccessful;
        }
    }
}

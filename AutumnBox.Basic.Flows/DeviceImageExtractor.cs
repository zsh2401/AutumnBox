/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/9 12:42:49
** filename: DeviceImageExtractor.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Support.CstmDebug;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// 设备镜像提取器参数
    /// </summary>
    public sealed class DeviceImageExtractorArgs : FlowArgs
    {
        /// <summary>
        /// 要提取的镜像类型
        /// </summary>
        public DeviceImage ImageType { get; set; } = DeviceImage.Recovery;
        /// <summary>
        /// 保存到的路径
        /// </summary>
        public string SavePath { get; set; } = ".";
    }
    /// <summary>
    /// 设备镜像提取器
    /// </summary>
    public sealed class DeviceImageExtractor : FunctionFlow<DeviceImageExtractorArgs>
    {
        private const string tempFileName = "iamwaitingforyou_caona.img";
        private bool _getPathSuccessful = false;
        private bool _copySuccessful = false;
        private bool _pullSuccessful = false;
        /// <summary>
        /// 主函数
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
        protected override Output MainMethod(ToolKit<DeviceImageExtractorArgs> toolKit)
        {
            var outBuilder = new AdvanceOutputBuilder();
            string path = DeviceImageFinder.PathOf(toolKit.Args.DevBasicInfo.Serial, toolKit.Args.ImageType);
            if (path == null) { return null; }
            _getPathSuccessful = true;
            using (AndroidShell shell = new AndroidShell(toolKit.Args.DevBasicInfo.Serial))
            {
                shell.Connect();
                shell.Switch2Su();
                //复制到程序根目录
                string copyPath = $"/sdcard/{tempFileName}";
                var copyResult = shell.SafetyInput($"cp {path} {copyPath}");
                outBuilder.Append(copyResult);
                if (copyResult.IsSuccessful)
                {
                    _copySuccessful = copyResult.IsSuccessful;
                    var filePullerArgs = new FilePullerArgs()
                    {
                        DevBasicInfo = toolKit.Args.DevBasicInfo,
                        SavePath = toolKit.Args.SavePath + $"\\{toolKit.Args.ImageType.ToString().ToLower()}.img",
                        FilePathOnDevice = copyPath,
                    };
                    var pullResult = new FilePuller().Run(filePullerArgs);
                    _pullSuccessful = (pullResult.ExitCode == 0);
                    toolKit.Ae("rm -rf " + copyPath);
                    outBuilder.Append(pullResult.OutputData);
                }
            }
            return outBuilder.Result;
        }
        /// <summary>
        /// 处理结果
        /// </summary>
        /// <param name="result"></param>
        protected override void AnalyzeResult(FlowResult result)
        {
            base.AnalyzeResult(result);
            result.ResultType = _getPathSuccessful && _copySuccessful && _pullSuccessful ? ResultType.MaybeSuccessful : ResultType.MaybeUnsuccessful;
        }
    }
}

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
    public sealed class DeviceImageExtractorArgs : FlowArgs
    {
        public DeviceImage ImageType { get; set; } = DeviceImage.Recovery;
        public string SavePath { get; set; } = ".";
    }
    public sealed class DeviceImageExtractor : FunctionFlow<DeviceImageExtractorArgs>
    {
        private const string tempFileName = "iamwaitingforyou_caona.img";
        private bool _getPathSuccessful = false;
        private bool _copySuccessful = false;
        private bool _pullSuccessful = false;
        protected override Output MainMethod(ToolKit<DeviceImageExtractorArgs> toolKit)
        {
            var output = new Output();
            string path = DeviceImageFinder.PathOf(toolKit.Args.DevBasicInfo.Serial, toolKit.Args.ImageType);
            if (path == null) { return null; }
            _getPathSuccessful = true;
            using (AndroidShell shell = new AndroidShell(toolKit.Args.DevBasicInfo.Serial))
            {
                shell.Connect();
                shell.Switch2Su();
                Logger.D("?");
                //复制到程序根目录
                string copyPath = $"/sdcard/{tempFileName}";
                var copyResult = shell.SafetyInput($"cp {path} {copyPath}");
                output.Append(copyResult);
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
                    output.Append(pullResult.OutputData);
                }
            }
            return output;
        }
        protected override void AnalyzeResult(FlowResult result)
        {
            base.AnalyzeResult(result);
            result.ResultType = _getPathSuccessful && _copySuccessful && _pullSuccessful ? ResultType.MaybeSuccessful : ResultType.MaybeUnsuccessful;
        }
    }
}

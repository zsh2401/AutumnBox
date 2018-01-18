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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    public sealed class DeviceImageFlasherArgs : FlowArgs
    {
        public DeviceImage ImageType { get; set; } = DeviceImage.Recovery;
        public string SourceFile { get; set; }
    }
    public sealed class DeviceImageFlasher : FunctionFlow<DeviceImageFlasherArgs>
    {
        private const string _savePath = "/sdcard/autumnbox.img.tmp";
        private ShellOutput moveResult;
        protected override OutputData MainMethod(ToolKit<DeviceImageFlasherArgs> toolKit)
        {
            OutputData output = new OutputData();
            /*push image file to sdcard*/
            var pushArgs = new FilePusherArgs()
            {
                DevBasicInfo = toolKit.Args.DevBasicInfo,
                SavePath = _savePath,
                SourceFile = toolKit.Args.SourceFile,
            };
            var pushResult = new FilePusher().Run(pushArgs);
            output.Append(pushResult.OutputData);
            /*move file to img path*/
            string path = DeviceImageFinder.PathOf(toolKit.Args.DevBasicInfo.Serial, toolKit.Args.ImageType);
            using (AndroidShell shell = new AndroidShell(toolKit.Args.DevBasicInfo.Serial))
            {
                shell.Connect();
                shell.Switch2Su();
                moveResult = shell.SafetyInput($"mv {_savePath} {path}");
                output.Append((OutputData)moveResult);
            }
            return output;
        }
        protected override void AnalyzeResult(FlowResult result)
        {
            base.AnalyzeResult(result);
            result.ResultType = moveResult.IsSuccess ? ResultType.MaybeSuccessful : ResultType.MaybeUnsuccessful;
        }
    }
}

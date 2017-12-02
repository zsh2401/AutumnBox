/* =============================================================================*\
*
* Filename: ApkInstaller
* Description: 
*
* Version: 1.0
* Created: 2017/12/1 0:07:42 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.FlowFramework;
using System.Collections.Generic;
using System.IO;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Util;
using AutumnBox.Support.CstmDebug;

namespace AutumnBox.Basic.Flows
{
    public sealed class ApkInstallerArgs : FlowArgs
    {
        public List<FileInfo> Files { get; set; }
    }
    public sealed class AApkInstalltionCompleteArgs
    {
        public FileInfo ApkFileInfo { get; internal set; }
        public bool IsSuccess { get; internal set; } = true;
        public bool NeedContinue { get; set; } = true;
        public OutputData Output { get; internal set; }
    }
    public delegate void AApkInstallltionComplete(object sender, AApkInstalltionCompleteArgs e);
    public sealed class ApkInstaller : FunctionFlow<ApkInstallerArgs, FlowResult>
    {
        public event AApkInstallltionComplete AApkIstanlltionCompleted;
        protected override OutputData MainMethod(ToolKit<ApkInstallerArgs> toolKit)
        {
            Logger.D($"Start installing....have {toolKit.Args.Files.Count} Apks");
            OutputData result = new OutputData()
            {
                OutSender = toolKit.Executer
            };
            foreach (FileInfo apkFileInfo in toolKit.Args.Files)
            {
                Command command =
                    Command.MakeForCmd(
                        $"{ConstData.ADB_PATH} -s {toolKit.Args.DevBasicInfo.ToString()} install \"{apkFileInfo.FullName}\"");
                Logger.D("making command ->" + command.FileName + command.FullCommand);
                var r = toolKit.Executer.Execute(command);
                Logger.D(r.ToString());
                var args = new AApkInstalltionCompleteArgs()
                {
                    ApkFileInfo = apkFileInfo,
                    IsSuccess = r.Contains("success"),
                    Output = r,
                };
                AApkIstanlltionCompleted?.Invoke(this, args);
                if (!args.NeedContinue) break;
            }
            Logger.D(result.ToString());
            return result;
        }
        protected override void AnalyzeResult(FlowResult result)
        {
            base.AnalyzeResult(result);
            if (result.OutputData.Contains("failure")) { result.ResultType = ResultType.MaybeSuccessful; }
        }
    }
}

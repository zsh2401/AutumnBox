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
        public OutputData Output { get; internal set; }
    }
    public sealed class ApkInstallerResult : FlowResult
    {
        public int InstallFailedCount { get; internal set; }
    }
    public delegate bool AApkInstallltionComplete(object sender, AApkInstalltionCompleteArgs e);
    public sealed class ApkInstaller : FunctionFlow<ApkInstallerArgs, ApkInstallerResult>
    {
        public event AApkInstallltionComplete AApkIstanlltionCompleted;
        private int errorCount = 0;
        protected override OutputData MainMethod(ToolKit<ApkInstallerArgs> toolKit)
        {
            Logger.T($"Start installing....have {toolKit.Args.Files.Count} Apks");
            OutputData result = new OutputData();
            result.AddOutSender(toolKit.Executer);
            foreach (FileInfo apkFileInfo in toolKit.Args.Files)
            {
                Command command =
                    Command.MakeForCmd(
                        $"{ConstData.FullAdbFileName} {toolKit.Args.Serial.ToFullSerial()} install -r \"{apkFileInfo.FullName}\"");

                var installResult = toolKit.Executer.Execute(command);
                bool currentSuccessful = !installResult.Output.Contains("failure");
                if (!currentSuccessful)
                {
                    errorCount++;
                }
                var args = new AApkInstalltionCompleteArgs()
                {
                    ApkFileInfo = apkFileInfo,
                    IsSuccess = currentSuccessful,
                    Output = installResult.Output,
                };
                if (AApkIstanlltionCompleted?.Invoke(this, args) != true)
                {
                    break;
                }
            }
            Logger.D(result.ToString());
            return result;
        }
        protected override void AnalyzeResult(ApkInstallerResult result)
        {
            base.AnalyzeResult(result);
            result.InstallFailedCount = errorCount;
            if (errorCount > 0) { result.ResultType = ResultType.MaybeUnsuccessful; }
        }
    }
}

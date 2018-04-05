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
using AutumnBox.Support.Log;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// Apk安装器的参数
    /// </summary>
    public sealed class ApkInstallerArgs : FlowArgs
    {
        /// <summary>
        /// 要安装的所有APK路径
        /// </summary>
        public List<FileInfo> Files { get; set; }
    }
    /// <summary>
    /// 一个APK安装完成时的参数
    /// </summary>
    public sealed class AApkInstalltionCompleteArgs
    {
        /// <summary>
        /// 文件信息
        /// </summary>
        public FileInfo ApkFileInfo { get; internal set; }
        /// <summary>
        /// 是否安装成功
        /// </summary>
        public bool IsSuccess { get; internal set; } = true;
        /// <summary>
        /// 安装时的输出内容
        /// </summary>
        public Output Output { get; internal set; }
    }
    /// <summary>
    /// Apk安装结果
    /// </summary>
    public sealed class ApkInstallerResult : FlowResult
    {
        /// <summary>
        /// 安装失败的数量
        /// </summary>
        public int InstallFailedCount { get; internal set; }
    }
    /// <summary>
    /// 一个APK安装完成时的事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate bool AApkInstallltionComplete(object sender, AApkInstalltionCompleteArgs e);
    /// <summary>
    /// APK安装器
    /// </summary>
    public sealed class ApkInstaller : FunctionFlow<ApkInstallerArgs, ApkInstallerResult>
    {
#pragma warning disable CA1009
        public event AApkInstallltionComplete AApkIstanlltionCompleted;
#pragma warning disable CA1009
        private int errorCount = 0;
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
        protected override Output MainMethod(ToolKit<ApkInstallerArgs> toolKit)
        {
            Logger.Info(this,$"Install starts....have {toolKit.Args.Files.Count} Apks");
            OutputBuilder result = new OutputBuilder();
            result.Register(toolKit.Executer);
            foreach (FileInfo apkFileInfo in toolKit.Args.Files)
            {
                Command command =
                    Command.MakeForCmd(
                        $"{AdbConstants.FullAdbFileName} {toolKit.Args.Serial.ToFullSerial()} install -r \"{apkFileInfo.FullName}\"");

                var installResult = toolKit.Executer.Execute(command);
                bool currentSuccessful = !installResult.Contains("failure");
                if (!currentSuccessful)
                {
                    errorCount++;
                }
                var args = new AApkInstalltionCompleteArgs()
                {
                    ApkFileInfo = apkFileInfo,
                    IsSuccess = currentSuccessful,
                    Output = installResult,
                };
                if (AApkIstanlltionCompleted?.Invoke(this, args) != true)
                {
                    break;
                }
            }
            return result.ToOutput() ;
        }
        /// <summary>
        /// 处理结果
        /// </summary>
        /// <param name="result"></param>
        protected override void AnalyzeResult(ApkInstallerResult result)
        {
            base.AnalyzeResult(result);
            result.InstallFailedCount = errorCount;
            if (errorCount > 0) { result.ResultType = ResultType.MaybeUnsuccessful; }
        }
    }
}

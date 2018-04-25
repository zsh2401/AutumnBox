/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/11 18:03:15
** filename: MiFlasher.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows.Result;
using AutumnBox.Basic.Util;
using AutumnBox.Support.Log;
using System.Diagnostics;

namespace AutumnBox.Basic.Flows.MiFlash
{
    /// <summary>
    /// MiFlash 参数
    /// </summary>
    public class MiFlasherArgs : FlowArgs
    {
        /// <summary>
        /// 目标BAT路径
        /// </summary>
        public string BatFileName { get; set; } = "flash_all.bat";
    }
    /// <summary>
    /// AutumnBox miflash参数
    /// </summary>
    public sealed class MiFlasher : FunctionFlow<MiFlasherArgs, AdvanceResult>
    {
        int retCode;
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        protected override Output MainMethod(ToolKit<MiFlasherArgs> toolKit)
        {
            MiFlashBatExecuteProcess process = new MiFlashBatExecuteProcess(toolKit.Args.BatFileName, toolKit.Args.Serial);
            process.OutputReceived += (s, e) => { OnOutputReceived(e); };
            process.ProcessStarted += (s, e) => { OnProcessStarted(e); };
            retCode = process.ExecuteToEnd();
            return _outputBuilder.ToOutput();
        }
        protected override void AnalyzeResult(AdvanceResult result)
        {
            base.AnalyzeResult(result);
            result.ExitCode = retCode;
        }
        private OutputBuilder _outputBuilder = new OutputBuilder();
        protected override void OnOutputReceived(OutputReceivedEventArgs e)
        {
            base.OnOutputReceived(e);
            if (!e.IsError)
            {
                _outputBuilder.AppendOut(e.Text);
            }
            else
            {
                _outputBuilder.AppendError(e.Text);
            }
        }

        private sealed class MiFlashBatExecuteProcess : Process
        {
            public event OutputReceivedEventHandler OutputReceived;
            public event ProcessStartedEventHandler ProcessStarted;
            public MiFlashBatExecuteProcess(string batFileName, DeviceSerialNumber serial)
            {
                this.StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = $"/q /c \"{batFileName}\" \"{serial.ToFullSerial()}\"",
                    FileName = "cmd.exe",
                    WorkingDirectory = AdbConstants.toolsPath,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                };
                Logger.Warn(this,$"file path:{StartInfo.Arguments}");
                OutputDataReceived += (s, e) =>
                {
                    OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e, false));
                };
                ErrorDataReceived += (s, e) =>
                {
                    OutputReceived?.Invoke(this, new OutputReceivedEventArgs( e, true));
                };
            }
            public int ExecuteToEnd()
            {
                Start();
                ProcessStarted?.Invoke(this, new ProcessStartedEventArgs(Id));
                BeginOutputReadLine();
                BeginErrorReadLine();
                WaitForExit();
                int ret = ExitCode;
                CancelErrorRead();
                CancelOutputRead();
                return ret;
            }
        }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}

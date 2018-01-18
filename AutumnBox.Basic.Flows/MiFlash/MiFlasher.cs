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
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutumnBox.Basic.Flows.MiFlash
{
    public class MiFlasherArgs : FlowArgs
    {
        public string BatFileName { get; set; } = "flash_all.bat";
    }
    public sealed class MiFlasher : FunctionFlow<MiFlasherArgs, AdvanceResult>
    {
        int retCode;
        
        protected override OutputData MainMethod(ToolKit<MiFlasherArgs> toolKit)
        {
            MiFlashBatExecuteProcess process = new MiFlashBatExecuteProcess(toolKit.Args.BatFileName, toolKit.Args.Serial);
            process.OutputReceived += (s, e) => { OnOutputReceived(e); };
            process.ProcessStarted += (s, e) => { OnProcessStarted(e); };
            retCode = process.ExecuteToEnd();
            return _outputBuffer;
        }
        protected override void AnalyzeResult(AdvanceResult result)
        {
            base.AnalyzeResult(result);
            result.ExitCode = retCode;
        }
        private OutputData _outputBuffer = new OutputData();
        protected override void OnOutputReceived(OutputReceivedEventArgs e)
        {
            base.OnOutputReceived(e);
            if (!e.IsError)
            {
                _outputBuffer.OutAdd(e.Text);
            }
            else
            {
                _outputBuffer.ErrorAdd(e.Text);
            }
        }


        private sealed class MiFlashBatExecuteProcess : Process
        {
            public event OutputReceivedEventHandler OutputReceived;
            public event ProcessStartedEventHandler ProcessStarted;
            public MiFlashBatExecuteProcess(string batFileName, DeviceSerial serial)
            {
                this.StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = $"/q /c {batFileName} {serial.ToFullSerial()}",
                    FileName = "cmd.exe",
                    WorkingDirectory = ConstData.toolsPath,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                };
                OutputDataReceived += (s, e) =>
                {
                    OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e.Data, e, false));
                };
                ErrorDataReceived += (s, e) =>
                {
                    OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e.Data, e, true));
                };
            }
            public int ExecuteToEnd()
            {
                Start();
                ProcessStarted?.Invoke(this, new ProcessStartedEventArgs() { Pid = Id });
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
}

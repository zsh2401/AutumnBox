using System;
using System.Collections.Generic;
using System.Text;
using AutumnBox.Basic.Executer;
using System.Diagnostics;
using AutumnBox.Basic.Functions.FunctionArgs;
using AutumnBox.Basic.Devices;

namespace AutumnBox.Basic.Functions.FunctionModules
{
    /// <summary>
    /// 模拟的Miflash线刷功能模块,未完成,请勿使用
    /// </summary>
    public class MiFlash : FunctionModule
    {
        private OutputData temtOut = new OutputData();
        private ABProcess MainProcess = new ABProcess();
        public FunctionArgs.MiFlasherArgs Args;
        public MiFlash(MiFlasherArgs args)
        {
            this.Args = args;
            MainProcess.OutputDataReceived += (s, e) =>
            {
                if (e.Data != null)
                {
                    temtOut.OutAdd(e.Data);
                    LogT("Out : " + e.Data);
                    OnOutReceived(e);
                }
                
            };
            MainProcess.ErrorDataReceived += (s, e) =>
            {
                if (e.Data != null)
                {
                    temtOut.OutAdd(e.Data);
                    LogT("Error : " + e.Data);
                    OnErrorReceived(e);
                }
            };
        }
        protected override OutputData MainMethod()
        {
            //MainProcess.StartInfo.Arguments = $"-s {DeviceID}";
            //LogD(MainProcess.StartInfo.FileName + " " + MainProcess.StartInfo.Arguments);
            //MainProcess.Start();
            //MainProcess.BeginErrorReadLine();
            //MainProcess.BeginOutputReadLine();
            //MainProcess.WaitForExit();
            //MainProcess.CancelOutputRead();
            //MainProcess.CancelErrorRead();
            MainProcess.StartInfo.WorkingDirectory = @"adb\";
            temtOut.Append(MainProcess.RunToExited(Args.batFileName, $"-s {DeviceID}"));
            return temtOut;
        }
        protected override void HandingOutput(OutputData output, ref ExecuteResult executeResult)
        {
            if (MainProcess.ExitCode == 1) executeResult.IsSuccessful = false;
            executeResult.Message = output.LineAll[output.LineAll.Count - 1];
        }
    }
}

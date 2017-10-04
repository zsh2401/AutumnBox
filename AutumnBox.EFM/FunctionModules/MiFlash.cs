/*我想要传达给你的话语*/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Functions.FunctionArgs;

namespace AutumnBox.Basic.Functions.FunctionModules
{
    /// <summary>
    /// 模拟的Miflash线刷功能模块,未完成,请勿使用
    /// </summary>
    public sealed class MiFlash : FunctionModule
    {
        private OutputData temtOut = new OutputData();
        private ABProcess MainProcess = new ABProcess();
        public MiFlasherArgs Args;
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
                    temtOut.ErrorAdd(e.Data);
                    LogT("Error : " + e.Data);
                    OnErrorReceived(e);
                }
            };
        }
        protected override OutputData MainMethod()
        {
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

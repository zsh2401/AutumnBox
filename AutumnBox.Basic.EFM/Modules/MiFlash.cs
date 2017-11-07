/* =============================================================================*\
*
* Filename: MiFlash.cs
* Description: 
*
* Version: 1.0
* Created: 10/2/2017 19:54:11(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
/*我想要传达给你的话语*/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Support.CstmDebug;

namespace AutumnBox.Basic.Function.Modules
{

    /// <summary>
    /// 模拟的Miflash线刷功能模块,未完成,请勿使用
    /// </summary>
    public sealed class MiFlash : FunctionModule
    {
        public static readonly string flash_all_bat = "flash_all.bat";
        public static readonly string flash_all_except_storage_bat = "flash_all_except_storage.bat";
        public static readonly string flash_all_except_storage_and_data_bat = "flash_all_except_data_storage.bat";
        private OutputData temtOut = new OutputData();
        private ABProcess MainProcess = new ABProcess();
        public MiFlasherArgs _Args;
        public MiFlash()
        {
            MainProcess.OutputDataReceived += (s, e) =>
            {
                if (e.Data != null)
                {
                    temtOut.OutAdd(e.Data);
                    Logger.D("Out: " + e.Data);
                    OnOutReceived(e);
                }

            };
            MainProcess.ErrorDataReceived += (s, e) =>
            {
                if (e.Data != null)
                {
                    temtOut.ErrorAdd(e.Data);
                    Logger.T("Error : " + e.Data);
                    OnErrorReceived(e);
                }
            };
            MainProcess.ProcessStarted += (s, e) => { OnProcessStarted(e); };
        }
        protected override void AnalyzeArgs(ModuleArgs args)
        {
            base.AnalyzeArgs(args);
            this._Args = (MiFlasherArgs)args;
        }
        protected override OutputData MainMethod()
        {
            MainProcess.StartInfo.WorkingDirectory = @"adb\";
            temtOut.Append(MainProcess.RunToExited("cmd.exe", "/c " + _Args.FloderPath + "/" + GetBatFileNameBy(_Args.Type) + $" -s {DeviceID}"));
            return temtOut;
        }
        private static string GetBatFileNameBy(MiFlashType type)
        {
            switch (type)
            {
                case MiFlashType.FlashAll:
                    return flash_all_bat;
                case MiFlashType.FlashAllExceptStorage:
                    return flash_all_except_storage_bat;
                default:
                    return flash_all_except_storage_and_data_bat;
            }
        }
        protected override void AnalyzeOutput(ref ExecuteResult executeResult)
        {
            if (Status != ModuleStatus.ForceStoped)
            {
                if (MainProcess.ExitCode == 1) executeResult.Level = ResultLevel.Unsuccessful;
            }
            executeResult.Message = executeResult.OutputData.LineAll[executeResult.OutputData.LineAll.Count - 1];
        }
    }
}

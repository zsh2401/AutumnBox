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
using AutumnBox.Basic.Function.Bundles;
using AutumnBox.Support.CstmDebug;
using System.IO;

namespace AutumnBox.Basic.Function.Modules
{

    /// <summary>
    /// 模拟的Miflash线刷功能模块,未完成,请勿使用
    /// </summary>
    public sealed class MiFlash : FunctionModule
    {
        public static readonly string flash_all_lock_bat = "flash_all_lock.bat";
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
                    OnOutputReceived(new OutputReceivedEventArgs(e.Data, e, false));
                }

            };
            MainProcess.ErrorDataReceived += (s, e) =>
            {
                if (e.Data != null)
                {
                    temtOut.ErrorAdd(e.Data);
                    Logger.T("Error : " + e.Data);
                    OnOutputReceived(new OutputReceivedEventArgs(e.Data, e, true));
                }
            };
            MainProcess.ProcessStarted += (s, e) => { OnProcessStarted(e); };
        }
        protected override void Create(BundleForCreate bundle)
        {
            base.Create(bundle);
            this._Args = (MiFlasherArgs)bundle.Args;
        }
        protected override OutputData MainMethod(BundleForTools toolsBundle)
        {
            MainProcess.StartInfo.WorkingDirectory = @"adb\";
            temtOut.Append(MainProcess.RunToExited("cmd.exe", "/c " + _Args.FloderPath + "/" + GetBatFileNameBy(_Args.Type) + $"{toolsBundle.Serial.ToFullSerial()}"));
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
        protected override void AnalyzeOutput(BundleForAnalyzingResult bundle)
        {
            base.AnalyzeOutput(bundle);
            if (Status != ModuleStatus.ForceStoped)
            {
                if (MainProcess.ExitCode == 1) bundle.Result.Level = ResultLevel.Unsuccessful;
            }
            bundle.Result.Message = bundle.Result.OutputData.LineAll[bundle.Result.OutputData.LineAll.Count - 1];
        }
        public static bool HaveFlashAllAndLockBat(string path)
        {
            return File.Exists(path + "/" + flash_all_lock_bat);
        }
    }
}

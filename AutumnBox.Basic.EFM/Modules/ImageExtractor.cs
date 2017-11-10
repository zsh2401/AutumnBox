/* =============================================================================*\
*
* Filename: RecoveryImgExtractor
* Description: 
*
* Version: 1.0
* Created: 2017/11/10 17:11:13 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Text;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Util;
using System.IO;
using System.Threading;
using AutumnBox.Support.CstmDebug;
using AutumnBox.Basic.Function.Args;

namespace AutumnBox.Basic.Function.Modules
{
    public class ImageExtractor : FunctionModule
    {
        private ABProcess MainProcess = new ABProcess();
        private bool NotFoundSu = false;
        private ImgExtractArgs _Args;
        private StreamWriter CmdWriter;
        public ImageExtractor()
        {
            MainProcess.StartInfo.FileName = "cmd.exe";
            MainProcess.OutputReceived += (s, e) => { OnOutputReceived(e); };
        }
        protected override void AnalyzeArgs(ModuleArgs args)
        {
            base.AnalyzeArgs(args);
            _Args = (ImgExtractArgs)args;
        }
        protected override OutputData MainMethod()
        {
            OutputData result = new OutputData
            {
                OutSender = MainProcess
            };
            MainProcess.Start();
            CmdWriter = MainProcess.StandardInput;
            CmdWriter.AutoFlush = true;
            MainProcess.BeginRead();
            Thread.Sleep(1000);
            CmdWriter.WriteLine($"{ConstData.ADB_PATH.Replace('/', '\\')} -s {DeviceID} shell");
            CmdWriter.WriteLine($"su");
            Thread.Sleep(2000);
            if (result.All.ToString().Contains("not found"))
            {
                NotFoundSu = true;
                ExitProcess();
                return result;
            }
            Thread.Sleep(2000);
            if (_Args.ExtractImage == Image.Recovery)
                WL($"cp /dev/block/platform/*/*/by-name/recovery /sdcard/recovery.img");
            else
                WL($"cp /dev/block/platform/*/*/by-name/boot /sdcard/boot.img");
            ExitProcess();
            return result;
        }
        private void WL(string cmd)
        {
            CmdWriter.WriteLine(cmd);
        }
        private void ExitProcess()
        {
            while (!MainProcess.HasExited)
            {
                WL("exit");
            }
            CmdWriter.Close();
        }
        protected override void AnalyzeOutput(ref ExecuteResult executeResult)
        {
            base.AnalyzeOutput(ref executeResult);
            if (NotFoundSu) executeResult.Level = ResultLevel.Unsuccessful;
            Logger.D($"Analyzing -----------------\n{executeResult.OutputData.All} \n------------------");
        }
    }
}

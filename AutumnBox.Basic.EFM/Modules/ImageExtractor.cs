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
        private bool _suNotFound = false;
        private bool _imgNotFound = false;
        private AndroidShell _shell;
        private ImgExtractArgs _Args;
        protected override void AnalyzeArgs(ModuleArgs args)
        {
            base.AnalyzeArgs(args);
            _Args = (ImgExtractArgs)args;
        }
        protected override OutputData MainMethod()
        {
            _shell = new AndroidShell(DeviceID);
            _shell.OutputReceived += (s, e) => { OnOutputReceived(e); };
            OutputData result = new OutputData
            {
                OutSender = _shell
            };
            //检查root
            _shell.InputLine("su");
            if (_shell.LatestLineOutput.Contains("not found"))
            {
                _suNotFound = true;
                return result;
            }
            string fileName = _Args.ExtractImage == Image.Recovery ? "recovery" : "boot";
            //获取镜像路径
            _shell.InputLine($"find /dev -name {fileName}");
            if (_shell.LatestLineOutput == _shell.LastCommand)
            {
                _imgNotFound = true;
                return result;
            }
            string imgPath = _shell.LatestLineOutput;
            //复制到手机根目录
            _shell.InputLine($"cp {imgPath} /sdcard/{fileName}.img ; echo copyfinish");
            //无论复制是否成功,都会在结束后显示copyfinish,等待就行了
            while (_shell.LatestLineOutput != "copyfinish") ;
            //Ok!
            Logger.D("Extractor finished.....");
            _shell.Dispose();
            return result;
        }
        protected override void AnalyzeOutput(ref ExecuteResult executeResult)
        {
            base.AnalyzeOutput(ref executeResult);
            if (_suNotFound || _imgNotFound) executeResult.Level = ResultLevel.Unsuccessful;
            Logger.D($"Analyzing -----------------\n{executeResult.OutputData.All} \n------------------");
        }

    }
}

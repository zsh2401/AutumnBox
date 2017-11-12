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
        private bool _copyFailed = false;
        private bool _findNotFind = false;
        private AndroidShell _shell;
        private ImgExtractArgs _Args;
        private bool FindImgPath(out string pathResult, string fileName)
        {
            //获取镜像路径
            _shell.InputLine("find || echo fail");
            _findNotFind = _shell.LatestLineOutput == "fail";
            if (!_findNotFind)
            {
                _shell.InputLine($"find /dev -name {fileName}");
                if (_shell.LatestLineOutput == _shell.LastCommand)
                {
                    Logger.D("find img fail " + _shell.LatestLineOutput);
                    _imgNotFound = true;
                    pathResult = null;
                    return false;
                }
                pathResult = _shell.LatestLineOutput;
                return true;
            }
            else
            {
                pathResult = $"/dev/block/platform/*/by-name/{fileName}";
                return false;
            }
        }
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
            //尝试切换到root权限
            if (!_shell.Switch2Superuser())
            {
                _suNotFound = true;
                return result;
            }
            string fileName = _Args.ExtractImage == Image.Recovery ? "recovery" : "boot";
            if (FindImgPath(out string imgPath, fileName)) { return result; }
            //复制到手机根目录
            _shell.DrangerousInputLine(
                $"cp {imgPath} /sdcard/{fileName}.img && echo __copyfinish__ || echo __copyfail__ "
                );
            //等待复制完毕,复制成功会显示copyfinish,否则显示copyfail
            while (true)
            {
                if (_shell.LatestLineOutput == "__copyfinish__")
                {
                    break;
                }
                else if (_shell.LatestLineOutput == "__copyfail__")
                {
                    _copyFailed = true;
                    break;
                }
            }
            //Ok!
            Logger.D("Extract finished.....");
            _shell.ExitShell();
            var puller = FunctionModuleProxy.Create<FilePuller>(new FilePullArgs(DevSimpleInfo) { PhoneFilePath = $"/sdcard/{fileName}.img" });
            result.Append(puller.FastRun().OutputData);
            //_shell.Dispose();
            return result;
        }
        protected override void AnalyzeOutput(ref ExecuteResult executeResult)
        {
            base.AnalyzeOutput(ref executeResult);
            if (_suNotFound || _imgNotFound || _copyFailed) executeResult.Level = ResultLevel.Unsuccessful;
            Logger.D($"Analyzing -----------------\n{executeResult.OutputData.All} \n------------------");
        }
    }
}

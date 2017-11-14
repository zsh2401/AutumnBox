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
using System.Text.RegularExpressions;

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
            Logger.D("find start....");
            _shell.SafetyInput($"ls -l /dev/block/bootdevice/by-name/{_Args.ExtractImage.ToString().ToLower()}");
            string result = _shell.LatestLineOutput;
            Logger.D("finding...." + result);
            Regex regex = new Regex(@"(?i)recovery[\u0020|\t]+->[\u0020|\t]+(?<filepath>.+)$");
            var m = regex.Match(result);
            if (m.Success)
            {
                pathResult = m.Result("${filepath}");
                Logger.D("fined filepath  " + m.Result("${filepath}"));
                return true;
            }
            _shell.SafetyInput("mkdir /sdcard/.autumnboxtest/");
            _findNotFind = _shell.SafetyInput("find /sdcard/.autumnboxtest/");
            _shell.SafetyInput("rm -rf /sdcard/.autumnboxtest");
            if (!_findNotFind)
            {
                _shell.SafetyInput($"find /dev -name {fileName}");
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
                return true;
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
            _shell.ProcessStarted += (s, e) => { OnProcessStarted(e); };
            _shell.Connect();
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
            if (!FindImgPath(out string imgPath, fileName)) { return result; }
            Logger.D("Image real-path finded.....");
            //复制到手机根目录
            _copyFailed = !(_shell.SafetyInput($"cp {imgPath} /sdcard/{fileName}.img"));
            //Ok!
            Logger.D("Extract and Copy finished.....");
            new Thread(_shell.Disconnect).Start();
            Logger.D("shell disconnected....");
            var puller = FunctionModuleProxy.Create<FilePuller>(new FilePullArgs(DevSimpleInfo) { PhoneFilePath = $"/sdcard/{fileName}.img", LocalFilePath = _Args.SavePath });
            result.Append(puller.FastRun().OutputData);
            Logger.D("pull finished....");
            return result;
        }
        protected override void AnalyzeOutput(ref ExecuteResult executeResult)
        {
            base.AnalyzeOutput(ref executeResult);
            if (_suNotFound || _imgNotFound || _copyFailed) executeResult.Level = ResultLevel.Unsuccessful;
            Logger.D($"SU NOT FOUND {_suNotFound}");
            Logger.D($"IMG NOT FOUND {_imgNotFound}");
            Logger.D($"COPY FAIL {_copyFailed}");
            //Logger.D($"Analyzing -----------------\n{executeResult.OutputData.All} \n------------------");
        }
    }
}

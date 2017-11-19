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
using AutumnBox.Basic.Devices;

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
        protected override void Create(BundleForCreate bundle)
        {
            base.Create(bundle);
            _Args = (ImgExtractArgs)bundle.Args;
        }
        protected override OutputData MainMethod(ToolsBundle toolsBundle)
        {
            OutputData result = new OutputData();
            string imagePath;
            string fileName = _Args.ExtractImage.ToString().ToLower();
            using (AndroidShell _shell = new AndroidShell(toolsBundle.DeviceID))
            {
                _shell.OutputReceived += (s, e) => { OnOutputReceived(e); };
                _shell.ProcessStarted += (s, e) => { OnProcessStarted(e); };
                _shell.Connect();
                result.OutSender = _shell;
                //尝试切换到root权限
                if (!_shell.Switch2Su())
                {
                    _suNotFound = true;
                    return result;
                }
                Logger.T("Switch to superuser success");
                imagePath = DeviceImageHelper.Find(_shell, _Args.ExtractImage);
                if (imagePath == null)//获取失败
                {
                    return result;
                }
                Logger.T("Image real-path finded..... ->" + imagePath );
                //复制到手机根目录
                Logger.T($"copy command ->cp {imagePath} /sdcard/{fileName}.img");
                var copyOutput = _shell.SafetyInput($"cp {imagePath} /sdcard/{fileName}.img");
                if (!(copyOutput.IsSuccess))
                {
                    _copyFailed = true;
                    return result;
                }
                //Ok!
                Logger.T("Extract and Copy finished.....");
            }
            Logger.T("pull recovery to computer.....");
            var puller = FunctionModuleProxy.Create<FilePuller>(new FilePullArgs(toolsBundle.Args.DeviceBasicInfo) { PhoneFilePath = $"/sdcard/{fileName}.img", LocalFilePath = _Args.SavePath });
            result.Append(puller.SyncRun().OutputData);
            Logger.D("pull finished....");
            return result;
        }
        protected override void AnalyzeOutput(BundleForAnalyzeOutput bundleForAnalyzeOutput)
        {
            base.AnalyzeOutput(bundleForAnalyzeOutput);
            if (_suNotFound || _imgNotFound || _copyFailed) bundleForAnalyzeOutput.Result.Level = ResultLevel.Unsuccessful;
            Logger.D($"SU NOT FOUND {_suNotFound}");
            Logger.D($"IMG NOT FOUND {_imgNotFound}");
            Logger.D($"COPY FAIL {_copyFailed}");
        }
    }
}

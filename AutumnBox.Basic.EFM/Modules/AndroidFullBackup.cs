/* =============================================================================*\
*
* Filename: AndroidFullBackup
* Description: 
*
* Version: 1.0
* Created: 2017/11/6 22:19:20 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System.Diagnostics;
using AutumnBox.Basic.Executer;
using System.Threading;

namespace AutumnBox.Basic.Function.Modules
{
    public class AndroidFullBackup : FunctionModule
    {
        private static readonly int _WaitTime = 2000;
        protected override OutputData MainMethod(ToolsBundle bundle)
        {
            OutputData result = new OutputData
            {
                OutSender = bundle.Executer
            };
            bundle.Ae("backup -apk -shared -all -f/backup.ab");
            return result;
        }
        protected override void OnOutputReceived(OutputReceivedEventArgs e)
        {
            base.OnOutputReceived(e);
            try
            {
                if (e.Text.ToLower().Contains("now unlock your device"))
                {
                    Thread.Sleep(_WaitTime);
                    ForceStop();
                }
            }
            catch { }
        }
        protected override void AnalyzeOutput(BundleForAnalyzeOutput bundle)
        {
            base.AnalyzeOutput(bundle);
            if (bundle.Result.OutputData.All.ToString().ToLower().Contains("now unlock your device"))
            {
                bundle.Result.Level = ResultLevel.Successful;
                bundle.Result.WasForcblyStop = false;
            }
        }
    }
}

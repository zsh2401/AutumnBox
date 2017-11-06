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
using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.Function.Modules
{
    public class AndroidFullBackup : FunctionModule
    {
        protected override OutputData MainMethod()
        {
            OutputData result = new OutputData
            {
                OutSender = this.Executer
            };
            Ae("backup -apk -shared -all -f/backup.ab");
            return result;
        }
        protected override void AnalyzeOutput(ref ExecuteResult executeResult)
        {
            base.AnalyzeOutput(ref executeResult);
            if (executeResult.OutputData.All.ToString().ToLower().Contains("now unlock your device"))
            {
                executeResult.Level = ResultLevel.Successful;
                executeResult.WasForcblyStop = false;
            }
        }
    }
}

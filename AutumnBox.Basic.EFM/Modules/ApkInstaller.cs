/* =============================================================================*\
*
* Filename: ApkInstaller
* Description: 
*
* Version: 1.0
* Created: 2017/10/24 3:36:44 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Bundles;
using AutumnBox.Basic.Function.Event;

namespace AutumnBox.Basic.Function.Modules
{
    public sealed class ApkInstaller : FunctionModule
    {
        private InstallApkArgs _Args;
        protected override void Create(BundleForCreate bundle)
        {
            base.Create(bundle);
            _Args = (InstallApkArgs)bundle.Args;
        }
        protected override OutputData MainMethod(BundleForTools bundle)
        {
            OutputData o = new OutputData() { OutSender = bundle.Executer };
            bundle.Ae($"install {_Args.ApkPath}");
            return o;
        }
        protected override void AnalyzeOutput(BundleForAnalyzeOutput bundle)
        {
            base.AnalyzeOutput(bundle);
            if (bundle.OutputData.LineError.Count != 0)
            {
                bundle.Result.Level = ResultLevel.MaybeSuccessful;
            }
        }
    }
}

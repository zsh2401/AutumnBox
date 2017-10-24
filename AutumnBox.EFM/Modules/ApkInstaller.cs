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

namespace AutumnBox.Basic.Function.Modules
{
    public sealed class ApkInstaller : FunctionModule
    {
        private InstallApkArgs _Args;
        protected override void HandlingModuleArgs(ModuleArgs e)
        {
            base.HandlingModuleArgs(e);
            _Args = (InstallApkArgs)e;
        }
        protected override OutputData MainMethod()
        {
            OutputData o = new OutputData() { OutSender = this.Executer };
            Ae($"install {_Args.ApkPath}");
            return o;
        }
        protected override void HandingOutput(ref ExecuteResult executeResult)
        {
            base.HandingOutput(ref executeResult);
        }
    }
}

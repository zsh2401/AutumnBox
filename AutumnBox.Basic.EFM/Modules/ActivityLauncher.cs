/* =============================================================================*\
*
* Filename: ActivityLauncher.cs
* Description: 
*
* Version: 1.0
* Created: 9/22/2017 03:13:12(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Function.Modules
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Function.Args;
    using AutumnBox.Basic.Function.Bundles;
    using AutumnBox.Support.CstmDebug;

    /// <summary>
    /// 活动启动器
    /// </summary>
    public sealed class ActivityLauncher : FunctionModule
    {
        private ActivityLaunchArgs _Args;
        protected override void Create(BundleForCreate bundle)
        {
            base.Create(bundle);
            _Args = (ActivityLaunchArgs)bundle.Args;
        }
        protected override OutputData MainMethod(BundleForTools bundle)
        {
            Logger.D($"Try Launch {bundle.Serial} Activity : {_Args.ActivityName}");
            string command = $"shell am start -n {_Args.PackageName}/{ _Args.ActivityName}";
            Logger.D(command);
            var o = bundle.Ae(command);
            return o;
        }
    }
}

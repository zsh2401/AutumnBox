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
    using static AutumnBox.Basic.Debug;
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Function.Args;
    using AutumnBox.Basic.Util;
    /// <summary>
    /// 活动启动器
    /// </summary>
    public sealed class ActivityLauncher : FunctionModule
    {
        private ActivityLaunchArgs _Args;
        protected override void HandlingModuleArgs(ModuleArgs e)
        {
            base.HandlingModuleArgs(e);
            _Args = (ActivityLaunchArgs)e;
        }
        protected override OutputData MainMethod()
        {
            Logger.D(TAG, $"Try Launch {DeviceID} Activity : {_Args.ActivityName}");
            string command = $"shell am start -n {_Args.PackageName}/{_Args.PackageName + _Args.ActivityName}";
            var o = Ae(command);
            return o;
        }
    }
}

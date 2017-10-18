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
        private ActivityLaunchArgs Args;
        public ActivityLauncher(ActivityLaunchArgs args)
        {
            this.Args = args;
        }
        protected override OutputData MainMethod()
        {
            Logger.D(TAG, $"Try Launch {DeviceID} Activity : {Args.ActivityName}");
            string command = $"shell am start -n {Args.PackageName}/{Args.PackageName + Args.ActivityName}";
            var o = Ae(command);
            return o;
        }
    }
}

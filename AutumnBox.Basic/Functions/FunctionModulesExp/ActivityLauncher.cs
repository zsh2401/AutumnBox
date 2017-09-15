/*
 Activity启动器
 */
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Util;
namespace AutumnBox.Basic.Functions
{
    public sealed class  ActivityLauncher:FunctionModule
    {
        private ActivityLaunchArgs Args;
        public ActivityLauncher(ActivityLaunchArgs args)  {
            this.Args = args;
        }
        protected override OutErrorData MainMethod()
        {
            Logger.D(TAG,$"Try Launch {DeviceID} Activity : {Args.ActivityName}");
            string command = $"shell am start -n {Args.PackageName}/{Args.PackageName + Args.ActivityName}";
            var o = executer.Execute(this.DeviceID,command);
            return o;
        }
    }
}

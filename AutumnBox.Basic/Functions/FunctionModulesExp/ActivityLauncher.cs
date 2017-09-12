/*
 Activity启动器
 */
using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Functions.Event;
using AutumnBox.Basic.Util;
namespace AutumnBox.Basic.Functions
{
    public sealed class  ActivityLauncher:FunctionModule
    {
        private ActivityLaunchArgs Args;
        public ActivityLauncher(ActivityLaunchArgs args) : base(FunctionArgs.ExecuterInitType.Adb) {
            this.Args = args;
        }
        protected override OutputData MainMethod()
        {
            Logger.D(TAG,$"Try Launch {DeviceID} Activity : {Args.ActivityName}");
            string command = $"shell am start -n {Args.PackageName}/{Args.PackageName + Args.ActivityName}";
            var o = MainExecuter.Execute(this.DeviceID,command);
            return o;
        }
    }
}

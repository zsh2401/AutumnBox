/*
 Activity启动器
 */
using AutumnBox.Basic.Functions.Event;
using AutumnBox.Basic.Util;
namespace AutumnBox.Basic.Functions
{
    public sealed class  ActivityLauncher:FunctionModule
    {
        private ActivityLaunchArgs Args;
        public ActivityLauncher(ActivityLaunchArgs args) : base(needStatus: FunctionRequiredDeviceStatus.Running) {
            this.Args = args;
        }
        protected override void MainMethod()
        {
            Logger.D(TAG,$"Try Launch {DeviceID} Activity : {Args.ActivityName}");
            string command = $"shell am start -n {Args.PackageName}/{Args.PackageName + Args.ActivityName}";
            var o = adb.Execute(this.DeviceID,command);
            OnFinish(this,new FinishEventArgs() { OutputData =o});
        }
    }
}

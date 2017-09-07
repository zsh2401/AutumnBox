using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Functions
{
    public sealed class  ApplicationLauncher:FunctionModule
    {
        private ApplicationLaunchArgs Args;
        public ApplicationLauncher(ApplicationLaunchArgs args) : base(needStatus: FunctionRequiredDeviceStatus.Running) {
            this.Args = args;
        }
        protected override void MainMethod()
        {
            Logger.D(TAG,"Try Launch Activity");
            string command = $"shell am start -n {Args.PackageName}/{Args.PackageName + Args.ActivityName}";
            var o = adb.Execute(this.DeviceID,command);
            OnFinish(this,new FinishEventArgs() { OutputData =o});
        }
    }
}

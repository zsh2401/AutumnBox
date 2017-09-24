namespace AutumnBox.Basic.Functions
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Util;
    /// <summary>
    /// 活动启动器
    /// </summary>
    public sealed class  ActivityLauncher:FunctionModule
    {
        private ActivityLaunchArgs Args;
        public ActivityLauncher(ActivityLaunchArgs args)  {
            this.Args = args;
        }
        protected override OutputData MainMethod()
        {
            Logger.D(TAG,$"Try Launch {DeviceID} Activity : {Args.ActivityName}");
            string command = $"shell am start -n {Args.PackageName}/{Args.PackageName + Args.ActivityName}";
            var o = Ae(command);
            return o;
        }
    }
}

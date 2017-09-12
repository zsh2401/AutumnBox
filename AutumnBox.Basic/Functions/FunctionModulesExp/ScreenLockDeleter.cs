namespace AutumnBox.Basic.Functions.FunctionModulesExp
{
    public sealed class ScreenLockDeleter : FunctionModule
    {
        public static new FunctionRequiredDeviceStatus RequiredDeviceStatus = FunctionRequiredDeviceStatus.Recovery;
        public ScreenLockDeleter() : base(RequiredDeviceStatus) { }
        protected override void MainMethod()
        {
            adb.Execute(DeviceID,"root");
            adb.Execute(DeviceID, "shell rm /data/system/gesture.key");
            adb.Execute(DeviceID, "adb shell rm /data/system/password.key");
            OnFinish(this,new Event.FinishEventArgs());
        }
    }
}

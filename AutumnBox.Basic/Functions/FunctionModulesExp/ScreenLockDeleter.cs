namespace AutumnBox.Basic.Functions.FunctionModulesExp
{
    public sealed class ScreenLockDeleter : FunctionModule
    {
        public static new FunctionRequiredDeviceStatus RequiredDeviceStatus = FunctionRequiredDeviceStatus.Recovery;
        protected override void MainMethod()
        {
            MainExecuter.Execute(DeviceID,"root");
            MainExecuter.Execute(DeviceID, "shell rm /data/system/gesture.key");
            MainExecuter.Execute(DeviceID, "adb shell rm /data/system/password.key");
            OnFinish(this,new Event.FinishEventArgs());
        }
    }
}

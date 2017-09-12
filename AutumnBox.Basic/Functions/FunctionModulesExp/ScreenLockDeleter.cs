using AutumnBox.Basic.AdbEnc;

namespace AutumnBox.Basic.Functions.FunctionModulesExp
{
    public sealed class ScreenLockDeleter : FunctionModule
    {
        protected override OutputData MainMethod()
        {
            MainExecuter.Execute(DeviceID,"root");
            MainExecuter.Execute(DeviceID, "shell rm /data/system/gesture.key");
            MainExecuter.Execute(DeviceID, "adb shell rm /data/system/password.key");
            return new OutputData();
        }
    }
}

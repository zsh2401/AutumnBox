using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.Functions.FunctionModulesExp
{
    public sealed class ScreenLockDeleter : FunctionModule
    {
        protected override OutputData MainMethod()
        {
            ae(DeviceID,"root");
            ae(DeviceID, "shell rm /data/system/gesture.key");
            ae(DeviceID, "adb shell rm /data/system/password.key");
            return new OutputData();
        }
    }
}

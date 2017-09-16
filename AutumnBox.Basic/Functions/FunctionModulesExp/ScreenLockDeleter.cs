using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.Functions.FunctionModulesExp
{
    public sealed class ScreenLockDeleter : FunctionModule
    {
        protected override OutputData MainMethod()
        {
            executer.Execute(DeviceID,"root");
            executer.Execute(DeviceID, "shell rm /data/system/gesture.key");
            executer.Execute(DeviceID, "adb shell rm /data/system/password.key");
            return new OutputData();
        }
    }
}

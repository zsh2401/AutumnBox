using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.Functions.FunctionModulesExp
{
    public sealed class ScreenLockDeleter : FunctionModule
    {
        protected override OutputData MainMethod()
        {
            Ae("root");
            Ae("shell rm /data/system/gesture.key");
            Ae("adb shell rm /data/system/password.key");
            return new OutputData();
        }
    }
}

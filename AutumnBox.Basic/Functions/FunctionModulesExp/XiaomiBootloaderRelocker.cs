using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.Functions
{
    public class XiaomiBootloaderRelocker : FunctionModule
    {
        protected override OutputData MainMethod()
        {
            var o = executer.Execute(DeviceID, " oem lock");
            return o;
        }
    }
}

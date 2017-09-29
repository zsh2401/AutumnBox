namespace AutumnBox.Basic.Functions
{
    using AutumnBox.Basic.Executer;
    public class XiaomiBootloaderRelocker : FunctionModule
    {
        protected override OutputData MainMethod()
        {
            var o = Ae(" oem lock");
            return o;
        }
    }
}

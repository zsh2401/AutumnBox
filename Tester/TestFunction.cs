namespace Tester
{
    using AutumnBox.Basic.Functions;
    using AutumnBox.Basic.Executer;
    public class TestFunction : FunctionModule
    {
        protected override OutputData MainMethod()
        {
            return Executer.AdbExecute("help");
        }
    }
}

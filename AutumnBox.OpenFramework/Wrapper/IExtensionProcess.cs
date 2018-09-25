namespace AutumnBox.OpenFramework.Wrapper
{
    public interface IExtensionProcess
    {
        int ExitCode { get; }
        void Start();
        int WaitForExit();
        void Kill();
    }
}

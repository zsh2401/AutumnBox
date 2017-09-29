namespace AutumnBox.Basic.Functions.Interface
{
    using AutumnBox.Basic.Executer;
    using System.Diagnostics;
    public interface IFunctionModule
    {
        event DataReceivedEventHandler OutReceived;
        event DataReceivedEventHandler ErrorReceived;
        event ProcessStartedEventHandler ProcessStarted;
        event StartedEventHandler Started;
        event FinishedEventHandler Finished;
        void Run();
    }
}

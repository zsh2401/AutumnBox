/* =============================================================================*\
*
* Filename: IFunctionModule.cs
* Description: 
*
* Version: 1.0
* Created: 9/26/2017 18:17:19(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
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

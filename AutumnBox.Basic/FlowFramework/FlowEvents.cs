/* =============================================================================*\
*
* Filename: FlowEvents
* Description: 
*
* Version: 1.0
* Created: 2017/11/23 15:15:10 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/

namespace AutumnBox.Basic.FlowFramework
{
    public delegate void FinishedEventHandler<TResult>(object sender, FinishedEventArgs<TResult> e) where TResult : FlowResult;
    public sealed class FinishedEventArgs<TResult> where TResult : FlowResult
    {
        public TResult Result { get; private set; }
        public FinishedEventArgs(TResult result)
        {
            Result = result;

        }
    }
    public delegate void StartupEventHandler(object sender, StartupEventArgs e);
    public class StartupEventArgs { }
}

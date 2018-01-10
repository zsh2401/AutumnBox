/* =============================================================================*\
*
* Filename: FunctionFlowBase
* Description: 
*
* Version: 1.0
* Created: 2017/11/24 20:45:39 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/

namespace AutumnBox.Basic.FlowFramework
{
    /// <summary>
    /// all flow is this, this is all flow
    /// </summary>
    public abstract class FunctionFlowBase : object
    {
        public static event FinishedEventHandler<FlowResult> AnyFinished;
        protected static void OnAnyFinished(object sender, FinishedEventArgs<FlowResult> e)
        {
            AnyFinished?.Invoke(sender, e);
        }
        protected internal static bool AnyFinishedRegistered => (AnyFinished != null);
    }
}

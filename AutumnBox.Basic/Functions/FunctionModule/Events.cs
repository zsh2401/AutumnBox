/* =============================================================================*\
*
* Filename: Events.cs
* Description: 
*
* Version: 1.0
* Created: 9/26/2017 18:25:55(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Functions.Event;

namespace AutumnBox.Basic.Functions
{
    public delegate void StartedEventHandler(object sender, StartEventArgs e);
    public delegate void FinishedEventHandler(object sender, FinishEventArgs e);

}

/* =============================================================================*\
*
* Filename: RunningManagerStatus.cs
* Description: 
*
* Version: 1.0
* Created: 9/27/2017 02:54:40(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Functions.RunningManager
{
    /// <summary>
    /// Nothing....
    /// </summary>
    public enum RunningManagerStatus
    {
        Loading = -1,
        Loaded = 0,
        Running,
        Finished,
        Cancel,
    }
}

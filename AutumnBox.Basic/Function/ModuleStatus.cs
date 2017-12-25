/* =============================================================================*\
*
* Filename: ModuleStatus
* Description: 
*
* Version: 1.0
* Created: 2017/10/24 3:12:17 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Function
{
    public enum ModuleStatus
    {
        Loading = -3,
        Ready = -2,
        Running = -1,
        Finished = 0,
        ForceStoped = 1,
        UnableToRun = 2,
        CannontStart = 3,
    }
}

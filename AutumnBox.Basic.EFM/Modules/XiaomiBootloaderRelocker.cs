/* =============================================================================*\
*
* Filename: XiaomiBootloaderRelocker.cs
* Description: 
*
* Version: 1.0
* Created: 9/22/2017 03:13:12(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Function.Modules
{
    using AutumnBox.Basic.Executer;
    public class XiaomiBootloaderRelocker : FunctionModule
    {
        protected override OutputData MainMethod(ToolsBundle toolsBundle)
        {
            var o = toolsBundle.Ae(" oem lock");
            return o;
        }
    }
}

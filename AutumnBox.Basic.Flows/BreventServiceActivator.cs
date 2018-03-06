/* =============================================================================*\
*
* Filename: BreventServiceActivator
* Description: 
*
* Version: 1.0
* Created: 2017/11/24 21:15:11 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/

namespace AutumnBox.Basic.Flows
{
    public class BreventServiceActivator : ShScriptExecuter
    {
        public static readonly string _AppPackageName = "me.piebridge.brevent";
        protected override string ScriptPath => "/data/data/me.piebridge.brevent/brevent.sh";
        protected override string AppActivity => ".ui.BreventActivity";
        protected override string AppPackageName => _AppPackageName;
    }
}

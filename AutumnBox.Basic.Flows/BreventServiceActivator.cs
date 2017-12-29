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
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.Basic.Function.Args;
using System.Threading;
using AutumnBox.Basic.Flows.Result;

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

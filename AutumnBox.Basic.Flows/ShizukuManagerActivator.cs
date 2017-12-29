/* =============================================================================*\
*
* Filename: ShizukuManagerActivator
* Description: 
*
* Version: 1.0
* Created: 2017/11/25 0:49:03 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/

namespace AutumnBox.Basic.Flows
{
    public class ShizukuManagerActivator : ShScriptExecuter
    {
        public static readonly string _AppPackageName = "moe.shizuku.privileged.api";
        protected override string AppActivity => "moe.shizuku.manager.MainActivity";
        protected override string AppPackageName => _AppPackageName;
        protected override string ScriptPath => "/sdcard/Android/data/moe.shizuku.privileged.api/files/start.sh";
    }
}

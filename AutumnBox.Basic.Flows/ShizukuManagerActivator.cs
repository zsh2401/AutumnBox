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
    /// <summary>
    /// ShizukuManager脚本运行器
    /// </summary>
    public class ShizukuManagerActivator : ShScriptExecuter
    {
        /// <summary>
        /// ShizukuManager包名
        /// </summary>
        public const string _AppPackageName = "moe.shizuku.privileged.api";
        /// <summary>
        /// 主Acitivty路径
        /// </summary>
        protected override string AppActivity => "moe.shizuku.manager.MainActivity";
        /// <summary>
        /// 包名
        /// </summary>
        protected override string AppPackageName => _AppPackageName;
        /// <summary>
        /// 脚本路径
        /// </summary>
        protected override string ScriptPath => "/sdcard/Android/data/moe.shizuku.privileged.api/files/start.sh";
    }
}

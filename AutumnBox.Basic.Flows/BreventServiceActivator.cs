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
    /// <summary>
    /// 黑域服务启动器
    /// </summary>
    public sealed class BreventServiceActivator : ShScriptExecuter
    {
        /// <summary>
        /// 黑域包名
        /// </summary>
        public const string _AppPackageName = "me.piebridge.brevent";
        /// <summary>
        /// 脚本路径
        /// </summary>
        protected override string ScriptPath => "/data/data/me.piebridge.brevent/brevent.sh";
        /// <summary>
        /// 主Activity的路径
        /// </summary>
        protected override string AppActivity => "ui.BreventActivity";
        /// <summary>
        /// 包名
        /// </summary>
        protected override string AppPackageName => _AppPackageName;
    }
}

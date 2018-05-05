/*************************************************
** auth： zsh2401@163.com
** date:  2018/5/5 19:41:40 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Support.Log;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// AppOps激活器的包名
    /// </summary>
    public sealed class AppOpsXActivator : ShScriptExecuter
    {
        /// <summary>
        /// AppOpsX的包名
        /// </summary>
        public const string ApplicationPackageName = "com.zzzmode.appopsx";
        /// <summary>
        /// AppOpsX的主界面类名
        /// </summary>
        protected override string AppActivity => "ui.main.MainActivity";
        /// <summary>
        /// 包名
        /// </summary>
        protected override string AppPackageName => ApplicationPackageName;
        /// <summary>
        /// 脚本路径
        /// </summary>
        protected override string ScriptPath => "/sdcard/Android/data/com.zzzmode.appopsx/opsx.sh";
    }
}

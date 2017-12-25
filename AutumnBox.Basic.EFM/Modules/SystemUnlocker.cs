/* =============================================================================*\
*
* Filename: XiaomiSystemUnlocker.cs
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
/*
 小米系统解锁器
 @zsh2401
 2017/9/8
 */
namespace AutumnBox.Basic.Function.Modules
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Function.Bundles;
    using AutumnBox.Basic.Function.Event;
    using System.Threading;
    /// <summary>
    /// 小米系统解锁器(非BL锁解锁器),操作完成后可以获得完整root权限,前提是,必须是开发版并且已经开启开发版的root权限
    /// </summary>
    public sealed class SystemUnlocker : FunctionModule
    {
        private BundleForTools _toolsBundle;
        protected override OutputData MainMethod(BundleForTools toolsBundle)
        {
            this._toolsBundle = toolsBundle;
            toolsBundle.Ae("root");
            Thread.Sleep(300);
            OutputData o = toolsBundle.Ae("disable-verity");
            return o;
        }
        protected override void OnFinished(FinishEventArgs e)
        {
            base.OnFinished(e);
            _toolsBundle.Ae("reboot");
        }
    }
}

/*
 小米系统解锁器
 @zsh2401
 2017/9/8
 */
namespace AutumnBox.Basic.Functions
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Functions.Event;
    using System.Threading;
    /// <summary>
    /// 小米系统解锁器(非BL锁解锁器),操作完成后可以获得完整root权限,前提是,必须是开发版并且已经开启开发版的root权限
    /// </summary>
    public sealed class XiaomiSystemUnlocker : FunctionModule
    {
        protected override OutputData MainMethod()
        {
            Ae("root");
            Thread.Sleep(300);
            OutputData o = Ae("disable-verity");
            return o;
        }
        protected override void OnFinish(FinishEventArgs a)
        {
            base.OnFinish(a);
            Ae("reboot");
        }
    }
}

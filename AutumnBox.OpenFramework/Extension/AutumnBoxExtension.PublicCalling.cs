/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/26 16:18:01 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Wrapper;
using AutumnBox.OpenFramework.Exceptions;
using AutumnBox.OpenFramework.Open;
namespace AutumnBox.OpenFramework.Extension
{
    /*此处是AutumnBoxExtension的一些实现部分,请勿随意调用*/
    public partial class AutumnBoxExtension
    {
        /// <summary>
        /// 检查Context的权限
        /// </summary>
        /// <param name="ctx"></param>
        private void PermissionCheck(Context ctx)
        {
            bool isWrapper = ctx is IExtensionWrapper;
            bool ctxPermissionIsEnough = ctx.Permission >= CtxPer.High;
            bool isProcess = ctx is IExtensionProcess;
            if (!(isWrapper || ctxPermissionIsEnough || isProcess))
            {
                throw new AccessDeniedException();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="args"></param>
        public void Init(Context caller, ExtensionArgs args)
        {
            PermissionCheck(caller);
            OnCreate(args);
        }
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="caller"></param>
        /// <returns></returns>
        public int Run(Context caller)
        {
            PermissionCheck(caller);
            return Main();
        }
        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="args"></param>
        public void Finish(Context caller, ExtensionFinishedArgs args)
        {
            PermissionCheck(caller);
            Canceled = args.IsForceStopped;
            Logger.CDebug("Finish()");
            Logger.CDebug("is fs:" + args.IsForceStopped);
            Logger.CDebug("exit code:" + args.ExitCode);
            OnFinish(args);
        }
        /// <summary>
        /// 尝试停止
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool TryStop(Context caller, ExtensionStopArgs args)
        {
            PermissionCheck(caller);
            return OnStopCommand();
        }
        /// <summary>
        /// 摧毁
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="args"></param>
        public void Destory(Context caller, ExtensionDestoryArgs args)
        {
            PermissionCheck(caller);
            OnDestory(args);
        }
    }
}

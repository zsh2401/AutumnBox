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
    public partial class AutumnBoxExtension
    {
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
        public void Init(Context caller, ExtensionArgs args)
        {
            PermissionCheck(caller);
            OnCreate(args);
        }
        public int Run(Context caller)
        {
            PermissionCheck(caller);
            return Main();
        }
        public void Finish(Context caller, ExtensionFinishedArgs args)
        {
            PermissionCheck(caller);
            Canceled = args.IsForceStopped;
            Logger.CDebug("Finish()");
            Logger.CDebug("is fs:" + args.IsForceStopped);
            Logger.CDebug("exit code:" + args.ExitCode);
            OnFinish(args);
        }
        public bool TryStop(Context caller, ExtensionStopArgs args)
        {
            PermissionCheck(caller);
            return OnStopCommand();
        }
        public void Destory(Context caller, ExtensionDestoryArgs args)
        {
            PermissionCheck(caller);
            OnDestory(args);
        }
    }
}

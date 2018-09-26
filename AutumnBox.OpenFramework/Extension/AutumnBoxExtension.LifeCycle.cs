/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 0:40:51 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Exceptions;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Wrapper;
using System;

namespace AutumnBox.OpenFramework.Extension
{

    /// <summary>
    /// 标准的秋之盒拓展基类
    /// </summary>
    [ExtName("无名拓展")]
    [ExtName("Unknown extension", Lang = "en-us")]
    [ExtAuth("佚名")]
    [ExtAuth("Anonymous", Lang = "en-us")]
    [ExtDesc(null)]
    [ExtVersion()]
    [ExtRequiredDeviceStates(NoMatter)]
    [ExtMinApi(8)]
    [ExtTargetApi()]
    [ExtRunAsAdmin(false)]
    [ExtRequireRoot(false)]
    [ExtOfficial(false)]
    [ExtRegion(null)]
    //[ExtAppProperty("com.miui.fm")]
    //[ExtMinAndroidVersion(7,0,0)]
    public abstract partial class AutumnBoxExtension : Context, IClassExtension
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
        protected ExtensionArgs Args { get; private set; }
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

        /// <summary>
        /// 当拓展被创建后调用
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnCreate(ExtensionArgs args)
        {
            Args = args;
        }

        /// <summary>
        /// 主函数
        /// </summary>
        public abstract int Main();

        /// <summary>
        /// 当拓展模块执行完成时调用,这通常发生在Main()函数之后
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnFinish(ExtensionFinishedArgs args) { }

        /// <summary>
        /// 当模块被要求终止时调用,如果做不到,请返回false或抛出异常
        /// </summary>
        /// <returns></returns>
        protected virtual bool OnStopCommand()
        {
            return false;
        }

        /// <summary>
        /// 无论如何,在模块即将被析构时,都将调用此函数
        /// </summary>
        protected virtual void OnDestory(ExtensionDestoryArgs args)
        {
        }
    }
}

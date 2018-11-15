/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/10 19:13:50 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Exceptions;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Wrapper;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 基础的IClassExtension的基础实现
    /// </summary>
    [ExtName("无名拓展", "en-us:Unknown extension")]
    [ExtAuth("佚名", "en-us:Anonymous")]
    [ExtDesc(null)]
    [ExtVersion()]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    [ExtMinApi(8)]
    [ExtTargetApi()]
    [ExtRunAsAdmin(false)]
    [ExtRequireRoot(false)]
    [ExtOfficial(false)]
    [ExtRegions(null)]
    //[ExtAppProperty("com.miui.fm")]
    //[ExtMinAndroidVersion(7,0,0)]
    public abstract class ClassExtensionBase : Context, IClassExtension
    {
        /// <summary>
        /// 当拓展被创建后调用
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnCreate(ExtensionArgs args)
        {
        }
        /// <summary>
        /// 主函数
        /// </summary>
        /// <returns></returns>
        protected abstract int Main();
        /// <summary>
        /// 当拓展模块执行完成时调用,这通常发生在Main()函数之后
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnFinish(ExtensionFinishedArgs args) { }
        /// <summary>
        /// 无论如何,在模块即将被析构时,都将调用此函数
        /// </summary>
        protected virtual void OnDestory(ExtensionDestoryArgs args) { }
        /// <summary>
        /// 当模块被要求终止时调用,如果做不到,请返回false或抛出异常
        /// </summary>
        /// <returns></returns>
        protected virtual bool OnStopCommand(ExtensionStopArgs args)
        {
            return false;
        }
        /// <summary>
        /// 摧毁
        /// </summary>
        /// <param name="methodCaller"></param>
        /// <param name="args"></param>
        public void Destory(Context methodCaller, ExtensionDestoryArgs args)
        {
            PermissionCheck(methodCaller);
            OnDestory(args);
        }
        /// <summary>
        /// 当完成时才应该调用此函数
        /// </summary>
        /// <param name="methodCaller"></param>
        /// <param name="args"></param>
        public void Finish(Context methodCaller, ExtensionFinishedArgs args)
        {
            PermissionCheck(methodCaller);
            OnFinish(args);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="methodCaller"></param>
        /// <param name="args"></param>
        public void Init(Context methodCaller, ExtensionArgs args)
        {
            PermissionCheck(methodCaller);
            OnCreate(args);
        }
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="methodCaller"></param>
        /// <returns></returns>
        public int Run(Context methodCaller)
        {
            PermissionCheck(methodCaller);
            return Main();
        }
        /// <summary>
        /// 尝试停止
        /// </summary>
        /// <param name="methodCaller"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool TryStop(Context methodCaller, ExtensionStopArgs args)
        {
            PermissionCheck(methodCaller);
            return OnStopCommand(args);
        }
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
    }
}

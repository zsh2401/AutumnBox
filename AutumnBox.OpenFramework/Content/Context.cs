/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/6 16:48:15 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Service;
using System;

namespace AutumnBox.OpenFramework.Content
{
    /// <summary>
    /// AutumnBox开放框架上下文
    /// </summary>
    [ContextPermission(CtxPer.Normal)]
    public abstract class Context : Object
    {
        private ContextApiProvider apiWrapper;
        /// <summary>
        /// 权限
        /// </summary>
        internal CtxPer Permission
        {
            get
            {
                if (permission == CtxPer.None)
                {
                    var attr = Attribute
                    .GetCustomAttribute(GetType(),
                    typeof(ContextPermissionAttribute), true);
                    permission = (attr as ContextPermissionAttribute)?.Value ?? CtxPer.Normal;
                }
                return permission;
            }
        }
        private CtxPer permission = CtxPer.None;
        /// <summary>
        /// 日志标签
        /// </summary>
        public virtual string LoggingTag
        {
            get
            {
                return GetType().Name;
            }
        }
        /// <summary>
        /// 声音播放器
        /// </summary>
        public ISoundPlayer SoundPlayer => apiWrapper.Factory.GetSoundPlayer(this);
        /// <summary>
        /// Ux api
        /// </summary>
        public IUx Ux => apiWrapper.Ux;
        /// <summary>
        /// 日志API
        /// </summary>
        public ILogger Logger => apiWrapper.Logger;
        /// <summary>
        /// 秋之盒整体程序相关API
        /// </summary>
        public IAppManager App => apiWrapper.App;
        /// <summary>
        /// 操作系统API
        /// </summary>
        public IOSApi OperatingSystem => apiWrapper.OS;
        /// <summary>
        /// 临时文件管理器
        /// </summary>
        public ITemporaryFloder Tmp => apiWrapper.Tmp;
        /// <summary>
        /// 兼容性相关API
        /// </summary>
        public ICompApi Comp => apiWrapper.Comp;
        /// <summary>
        /// 嵌入资源提取器
        /// </summary>
        public IEmbeddedFileManager EmbFileManager => apiWrapper.Emb;
        /// <summary>
        /// 构建
        /// </summary>
        public Context()
        {
            apiWrapper = new ContextApiProvider(this);
        }
        /// <summary>
        /// 获取全局服务
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AtmbService GetService(string name)
        {
            return Manager.ServicesManager.GetServiceByName(this, name);
        }
        /// <summary>
        /// 获取并根据传入泛型拆箱
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public TReturn GetService<TReturn>(string name) where TReturn : class
        {
            return Manager.ServicesManager.GetServiceByName(this, name) as TReturn;
        }
        /// <summary>
        /// 在UI线程运行代码
        /// </summary>
        /// <param name="act"></param>
        public void RunOnUIThread(Action act)
        {
            App.RunOnUIThread(act);
        }
    }
}
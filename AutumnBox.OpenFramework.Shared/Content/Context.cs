/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/6 16:48:15 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.Impl;
using AutumnBox.OpenFramework.Running;
using AutumnBox.OpenFramework.Service;
using AutumnBox.OpenFramework.Service.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Content
{
    /// <summary>
    /// AutumnBox开放框架上下文
    /// </summary>
    [ContextPermission(CtxPer.Normal)]
    public abstract class Context : object
    {
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
        /// Ux api
        /// </summary>
        public IUx Ux => _lazyUX.Value;
        private Lazy<IUx> _lazyUX;

        /// <summary>
        /// 日志API
        /// </summary>
        public ILogger Logger => _lazyLogger.Value;
        private Lazy<ILogger> _lazyLogger;

        /// <summary>
        /// 秋之盒整体程序相关API
        /// </summary>
        public IAppManager App => _lazyApp.Value;
        private Lazy<IAppManager> _lazyApp;

        /// <summary>
        /// 临时文件管理器
        /// </summary>
        public ITemporaryFloder Tmp => _lazyTmp.Value;
        private Lazy<ITemporaryFloder> _lazyTmp;

        /// <summary>
        /// 兼容性相关API
        /// </summary>
        public ICompApi Comp => _comp;
        private readonly static ICompApi _comp = new CompImpl();


        /// <summary>
        /// 嵌入资源提取器
        /// </summary>
        public IEmbeddedFileManager EmbeddedManager => _lazyEmb.Value;

        /// <summary>
        /// 嵌入资源提取器
        /// </summary>
        public IEmbeddedFileManager EmbFileManager => _lazyEmb.Value;
        private Lazy<IEmbeddedFileManager> _lazyEmb;

        [ContextPermission(CtxPer.High)]
        private class UniversalHighPermissionContext : Context
        {
            public UniversalHighPermissionContext(Context sourceContext)
            {
                SourceContext = sourceContext;
            }
            public Context SourceContext { get; }
        }
        internal Context HContext
        {
            get
            {
                if (hContext == null)
                {
                    hContext = new UniversalHighPermissionContext(this);
                }
                return hContext;
            }
        }
        private UniversalHighPermissionContext hContext;

        internal IBaseApi BaseApi
        {
            get
            {
                var ctx = new UniversalHighPermissionContext(this);
                string servName = SBaseApiContainer.NAME;
                return GetService<SBaseApiContainer>(servName).GetApi(ctx);
            }
        }

        /// <summary>
        /// 服务管理器
        /// </summary>
        public IServicesManager ServicesManager
        {
            get
            {
                return Manager.ServicesManager;
            }
        }
        /// <summary>
        /// 获取拓展模块文件夹
        /// </summary>
        public System.IO.DirectoryInfo ExtensionDir => new System.IO.DirectoryInfo(Manager.InternalManager.ExtensionPath);
        static Context()
        {
        }
        /// <summary>
        /// 构建
        /// </summary>
        public Context()
        {
            InitFactory();
        }
        /// <summary>
        /// 获取一个新的拓展模块线程
        /// </summary>
        /// <param name="extensionType"></param>
        /// <returns></returns>
        public IExtensionThread NewExtensionThread(Type extensionType)
        {
            var wrappers = from wrapper in Manager.InternalManager.GetLoadedWrappers()
                           where extensionType == wrapper.Info.ExtType
                           select wrapper;
            if (wrappers.Count() == 0)
            {
                throw new Exception("Extension not found");
            }
            return wrappers.First().GetThread();
        }
        /// <summary>
        /// 获取一个新的拓展模块线程
        /// </summary>
        /// <returns></returns>
        public IExtensionThread NewExtensionThread(string className)
        {
            var wrappers = from wrapper in Manager.InternalManager.GetLoadedWrappers()
                           where className == wrapper.Info.ExtType.Name
                           select wrapper;
            if (!wrappers.Any())
            {
                throw new Exception("Extension not found");
            }
            return wrappers.First().GetThread();
        }
        /// <summary>
        /// 启动一个另一个模块
        /// </summary>
        /// <param name="t"></param>
        /// <param name="callback"></param>
        public void StartExtension(Type t, Action<int> callback = null)
        {
            var thread = NewExtensionThread(t);
            thread.Finished += (s, e) =>
            {
                callback?.Invoke(e.Thread.ExitCode);
            };
            thread.Start();
        }
        /// <summary>
        /// 获取全局服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public AtmbService GetService(string serviceName)
        {
            return Manager.ServicesManager.GetServiceByName(this, serviceName);
        }
        /// <summary>
        /// 获取并根据传入泛型拆箱
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public TReturn GetService<TReturn>(string serviceName) where TReturn : class
        {
            return Manager.ServicesManager.GetServiceByName(this, serviceName) as TReturn;
        }
        /// <summary>
        /// 在UI线程运行代码
        /// </summary>
        /// <param name="act"></param>
        public void RunOnUIThread(Action act)
        {
            App.RunOnUIThread(act);
        }
        /// <summary>
        /// 初始化各种懒加载工厂方法
        /// </summary>
        private void InitFactory()
        {
            _lazyApp = new Lazy<IAppManager>(() =>
            {
                return new AppManagerImpl(this, BaseApi);
            });
            _lazyUX = new Lazy<IUx>(() =>
            {
                return new UxImpl(this, BaseApi);
            });
            _lazyLogger = new Lazy<ILogger>(() =>
            {
                return new LoggerImpl(this);
            });
            _lazyTmp = new Lazy<ITemporaryFloder>(() =>
            {
                return new TemporaryFloderImpl(this);
            });
            _lazyEmb = new Lazy<IEmbeddedFileManager>(() =>
            {
                return new EmbeddedFileManagerImpl(this);
            });
        }
    }
}
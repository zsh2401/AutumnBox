/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/4 1:02:15 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Open.ServiceImpl;
using AutumnBox.OpenFramework.Running;
using AutumnBox.OpenFramework.Service;

namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// 各种管理器
    /// </summary>

    public static class OpenFx
    {
        /// <summary>
        /// 内部API
        /// </summary>
        internal static IBaseApi BaseApi { get; set; }

        /// <summary>
        /// 内部管理器
        /// </summary>
        public static ILibsManager LibsManager { get; internal set; }

        /// <summary>
        /// 服务管理器
        /// </summary>
        public static IServicesManager ServicesManager { get; internal set; }

        /// <summary>
        /// 初始化环境
        /// </summary>
        /// <param name="baseApi"></param>
#if !SDK
        public static void InitEnv(IBaseApi baseApi)
        {
            //加载API
            BaseApi = baseApi ?? throw new System.ArgumentNullException(nameof(baseApi));

            //初始化服务
            ServicesManager = new ServicesManagerImpl();
            LibsManager = new TheWanderingEarthManager(BuildInfo.DEFAULT_EXTENSION_PATH);

            //加载基础服务
            ServicesManager.StartService<SMd5>();
            ServicesManager.StartService<SSoundManager>();
            ServicesManager.StartService<SResourcesManager>();
            ServicesManager.StartService<SDeviceSelector>();
            ServicesManager.StartService<SExtensionThreadManager>();
            ServicesManager.StartService<SOSApi>();
        }

        /// <summary>
        /// 加载拓展模块
        /// </summary>
        public static void LoadExtensions()
        {
            OpenFx.LibsManager.Load();
        }

        /// <summary>
        /// 卸载
        /// </summary>
        public static void Unload()
        {
            foreach (var lib in OpenFx.LibsManager.Librarians)
            {
                try { lib.Destory(); } catch { }
            }
            ServicesManagerImpl serviceManager = (ServicesManagerImpl)OpenFx.ServicesManager;
            foreach (var serv in serviceManager._serviceCollection)
            {
                try { serv.Stop(); } catch { }
                try { serv.Destory(); } catch { }
            }
        }
#endif
    }
}

/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/21 1:28:25 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Open.ServiceImpl;
using AutumnBox.OpenFramework.Running;
using AutumnBox.OpenFramework.Service;

namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// 框架加载器
    /// </summary>
#if SDK
    internal
#else
    public
#endif
        static class FxLoader
    {
        /// <summary>
        /// 初始化环境
        /// </summary>
        /// <param name="baseApi"></param>
        public static void InitEnv(IBaseApi baseApi)
        {
            //加载API
            CallingBus.BaseApi = baseApi ?? throw new System.ArgumentNullException(nameof(baseApi));

            //初始化服务
            Manager.ServicesManager = new ServicesManagerImpl();
            Manager.InternalManager = new InternalManagerImpl();

            //加载基础服务
            Manager.ServicesManager.StartService<SMd5>();
            Manager.ServicesManager.StartService<SSoundManager>();
            Manager.ServicesManager.StartService<SResourcesManager>();
            Manager.ServicesManager.StartService<SDeviceSelector>();
            Manager.ServicesManager.StartService<SExtensionThreadManager>();
            Manager.ServicesManager.StartService<SOSApi>();
        }
        /// <summary>
        /// 加载拓展模块
        /// </summary>
        public static void LoadExtensions()
        {
            Manager.InternalManager.Reload();
        }

        /// <summary>
        /// 卸载拓展模块
        /// </summary>
        public static void UnloadExtensions()
        {
            foreach (var lib in Manager.InternalManager.Librarians)
            {
                try { lib.Destory(); } catch { }
            }
        }
        /// <summary>
        /// 停止并摧毁服务
        /// </summary>
        public static void UnloadServices()
        {
            ServicesManagerImpl serviceManager = (ServicesManagerImpl)Manager.ServicesManager;
            foreach (var serv in serviceManager._serviceCollection)
            {
                try { serv.Stop(); } catch { }
                try { serv.Destory(); } catch { }
            }
        }
    }
}

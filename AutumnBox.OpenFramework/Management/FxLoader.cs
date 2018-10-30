/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/21 1:28:25 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Service;
using AutumnBox.OpenFramework.Service.Default;
using System;
using System.Collections.Generic;
using System.Text;

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
        /// FxLoader总管理器
        /// </summary>
        /// <param name="guiApiImpl"></param>
        public static void LoadApi(IAutumnBox_GUI guiApiImpl)
        {
            CallingBus.LoadApi(guiApiImpl);
        }
        /// <summary>
        /// 加载基础服务
        /// </summary>
        public static void LoadServices()
        {
            IServicesManager serviceManager = Manager.ServicesManager;
            serviceManager.StartService<SMd5>();
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

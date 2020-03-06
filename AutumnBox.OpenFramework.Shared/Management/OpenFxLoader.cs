/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/4 1:02:15 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.OpenFramework.Management.ExtLibrary;

namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// 各种管理器
    /// </summary>
#if !SDK
    public
#else
     internal
#endif
        static class OpenFxLoader
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
        /// 初始化环境
        /// </summary>
        /// <param name="baseApi"></param>

        public static void InitEnv(IBaseApi baseApi)
        {
            //加载API
            BaseApi = baseApi ?? throw new System.ArgumentNullException(nameof(baseApi));

            //初始化服务
            LibsManager = new DreamLibManager();
        }

        /// <summary>
        /// 加载拓展模块
        /// </summary>
        public static void LoadExtensions()
        {
            OpenFxLoader.LibsManager.Load();
        }

        /// <summary>
        /// 卸载
        /// </summary>
        public static void Unload()
        {
            foreach (var lib in OpenFxLoader.LibsManager.Librarians)
            {
                try { lib.Destory(); } catch { }
            }
        }

    }
}

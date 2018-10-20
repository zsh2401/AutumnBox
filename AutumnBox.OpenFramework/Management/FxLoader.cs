/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/21 1:28:25 (UTC +8:00)
** desc： ...
*************************************************/
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
        /// 加载拓展模块
        /// </summary>
        public static void LoadExtensions()
        {
            Manager.InternalManager.Reload();
        }
    }
}

/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/26 19:35:44 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
namespace AutumnBox.OpenFramework.Internal
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public static class ExtensionManager
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    {
        private const string TAG = "ExtensionManager";
        private static ExtensionManagerCore inner;
        /// <summary>
        /// 拓展文件夹绝对路径
        /// </summary>s
        [Obsolete("This property will be removed in the future,please use    ExtsManager.ExtensionsPath    to instead",true)]
        public static string ExtensionsPath => ExtensionsPath_Internal;
        internal static string ExtensionsPath_Internal
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\AutumnBox\Extensions";
            }
        }
        static ExtensionManager()
        {
            inner = new ExtensionManagerCore();
        }

        /// <summary>
        /// 加载所有模块
        /// </summary>
        internal static void LoadAllExtension(Context context)
        {
            context.PermissionCheck(ContextPermissionLevel.Mid);
            inner.Load();
        }

        /// <summary>
        /// 摧毁所有模块
        /// </summary>
        internal static void DestoryAllExtension(Context context)
        {
            context.PermissionCheck(ContextPermissionLevel.Mid);
            inner.Extensions.ForEach((extRuntime) =>
            {
                extRuntime.Destory(new DestoryArgs());
            });
        }

        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <param name="context"></param>
        /// <returns>所有拓展模块</returns>
        internal static IAutumnBoxExtension[] GetExtensions(Context context)
        {
            context.PermissionCheck(ContextPermissionLevel.Mid);
            return inner.Extensions.ToArray();
        }

    }
}

#nullable enable
using AutumnBox.OpenFramework.Management.ExtInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// IExtension接口的实用拓展方法
    /// </summary>
    public static class IExtensionUsefulTools
    {
        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="classExtension"></param>
        /// <returns></returns>
        public static string GetName(this IClassExtension classExtension)
        {
            var extInf = ClassExtensionInfoCache.Acquire(classExtension.GetType());
            return extInf.Name();
        }

        /// <summary>
        /// 获取图标
        /// </summary>
        /// <param name="classExtension"></param>
        /// <returns></returns>
        public static byte[] GetIcon(this IClassExtension classExtension)
        {
            var extInf = ClassExtensionInfoCache.Acquire(classExtension.GetType());
            return extInf.Icon();
        }
    }
}

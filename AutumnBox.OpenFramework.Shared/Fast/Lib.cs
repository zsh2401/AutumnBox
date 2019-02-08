using AutumnBox.OpenFramework.ExtLibrary;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Fast
{
    /// <summary>
    /// 关于ILibsManager的拓展函数
    /// </summary>
    public static class Lib
    {
        /// <summary>
        /// 获取加载的Wrappers
        /// </summary>
        /// <param name="libsManager"></param>
        /// <returns></returns>
        public static IEnumerable<IExtensionWrapper> Wrappers(this ILibsManager libsManager)
        {
            List<IExtensionWrapper> result = new List<IExtensionWrapper>();
            IEnumerable<ILibrarian> libs = libsManager.Librarians;
            foreach (var lib in libs)
            {
                try
                {
                    result.AddRange(lib.GetWrappers());
                }
                catch (Exception ex)
                {
                    OpenFx.BaseApi.Log("LibExtension", "Warn", $"获取拓展模块封装类失败({lib.Name}){ex}");
                }
            }
            return result;
        }
    }
}

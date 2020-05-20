#nullable enable
using AutumnBox.OpenFramework.Management.ExtInfo;
using System.Collections.Generic;
using System.Linq;

namespace AutumnBox.OpenFramework.Management.ExtLibrary
{
    /// <summary>
    /// 关于ILibsManager的拓展函数
    /// </summary>
    public static class LibrarianExtension
    {
        /// <summary>
        /// 获取所有拓展模块
        /// </summary>
        /// <param name="libsManager"></param>
        /// <returns></returns>
        public static IEnumerable<IExtensionInfo> GetAllExtensions(this ILibsManager libsManager)
        {
            var result = new List<IExtensionInfo>();
            libsManager.Librarians.All((lib) =>
            {
                result.AddRange(lib.Extensions);
                return true;
            });
            return result;
        }
    }
}

using AutumnBox.OpenFramework.ExtLibrary;
using AutumnBox.OpenFramework.Wrapper;
using System.Collections.Generic;
namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// 总的拓展模块管理器
    /// </summary>
    public interface ILibsManager 
    {
        /// <summary>
        /// 重载
        /// </summary>
        void Load();
        /// <summary>
        /// 已加载的库管理器
        /// </summary>
        IEnumerable<ILibrarian> Librarians { get; }
    }
}

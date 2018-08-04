using AutumnBox.OpenFramework.Warpper;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.ExtLibrary
{
    /// <summary>
    /// 代表拓展模块程序的核心接口
    /// </summary>
    public interface ILibrarian
    {
        /// <summary>
        /// 代表该程序集的入口类的名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 检查
        /// </summary>
        /// <returns></returns>
        bool Check();
        /// <summary>
        /// 重载
        /// </summary>
        void Reload();
        /// <summary>
        /// 获取该入口类所代表的程序集中的拓展的包装类
        /// </summary>
        /// <returns></returns>
        IEnumerable<IExtensionWarpper> GetWarppers();
        /// <summary>
        /// 当入口所代表的程序集被卸载时调用
        /// </summary>
        void Destory();
    }
}

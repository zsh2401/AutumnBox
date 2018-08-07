using AutumnBox.OpenFramework.Warpper;

namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// 运行中的拓展模块管理器
    /// </summary>
    public interface IRunningManager
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="warpper"></param>
        void Add(IExtensionWarpper warpper);
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="warpper"></param>
        void Remove(IExtensionWarpper warpper);
    }
}

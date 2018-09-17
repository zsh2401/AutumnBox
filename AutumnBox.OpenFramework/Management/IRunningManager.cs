using AutumnBox.OpenFramework.Wrapper;

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
        /// <param name="wrapper"></param>
        void Add(IExtensionWrapper wrapper);
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="wrapper"></param>
        void Remove(IExtensionWrapper wrapper);
    }
}

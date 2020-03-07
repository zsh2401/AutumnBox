using AutumnBox.OpenFramework.Management.ExtensionThreading;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 运行管理器
    /// </summary>
    public interface IThreadManager
    {
        /// <summary>
        /// 获取所有运行中的拓展模块线程
        /// </summary>
        /// <returns></returns>
        IExtensionThread[] RunningThreads();
        /// <summary>
        /// 获取一个新的,未开始的拓展模块线程
        /// </summary>
        /// <param name="extensionClassName"></param>
        /// <returns></returns>
        IExtensionThread GetNewThread(string extensionClassName);
    }
}

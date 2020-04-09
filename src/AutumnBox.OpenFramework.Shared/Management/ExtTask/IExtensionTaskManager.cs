using System;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Management.ExtTask
{

    /// <summary>
    /// 线程管理器
    /// </summary>
    public interface IExtensionTaskManager
    {
        /// <summary>
        /// 分配
        /// </summary>
        /// <returns></returns>
        IExtensionTask Allocate(Type extType);
        /// <summary>
        /// 获取运行中的
        /// </summary>
        /// <returns></returns>
        IEnumerable<IExtensionTask> RunningTasks { get; }
        /// <summary>
        /// 根据ID查找正在运行的线程
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception">Extension not found</exception>
        /// <returns></returns>
        IExtensionTask FindTaskById(int id);
    }
}

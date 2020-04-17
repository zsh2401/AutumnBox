#nullable enable
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management.ExtInfo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Management.ExtTask
{

    /// <summary>
    /// 线程管理器
    /// </summary>
    public interface IExtensionTaskManager
    {
        /// <summary>
        /// 获取正在运行的任务
        /// </summary>
        IEnumerable<Task<object?>> RunningTasks { get; }

        /// <summary>
        /// 获取该任务所运行的拓展模块
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        IExtensionInfo GetExtensionByTask(Task<object?> task);

        /// <summary>
        /// 启动一个任务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="args"></param>
        /// <param name="extralArgs"></param>
        /// <returns></returns>
        Task<object?> Start(string id, Dictionary<string, object>? args = null);

        /// <summary>
        /// 启动一个任务
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        Task<object?> Start(IExtensionInfo extension, Dictionary<string, object>? args = null);

        /// <summary>
        /// 结束一个任务
        /// </summary>
        /// <param name="task"></param>
        void Terminate(Task<object?> task);
    }
}

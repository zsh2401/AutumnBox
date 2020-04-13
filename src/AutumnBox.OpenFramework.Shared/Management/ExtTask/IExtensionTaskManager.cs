using AutumnBox.OpenFramework.Extension;
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
        IEnumerable<Task<object>> RunningTasks { get; }

        /// <summary>
        /// 获取该任务所运行的拓展模块
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        Type ExtensionOfTask(Task<object> task);

        /// <summary>
        /// 启动一个任务
        /// </summary>
        /// <param name="extensionClassName"></param>
        /// <param name="extralArgs"></param>
        /// <returns></returns>
        Task<object> Start(string extensionClassName, Dictionary<string, object> extralArgs = null);

        /// <summary>
        /// 启动一个任务
        /// </summary>
        /// <param name="t"></param>
        /// <param name="extralArgs"></param>
        /// <returns></returns>
        Task<object> Start(Type t, Dictionary<string, object> extralArgs = null);

        /// <summary>
        /// 启动一个任务
        /// </summary>
        /// <typeparam name="TClassExtension"></typeparam>
        /// <param name="extralArgs"></param>
        /// <returns></returns>
        Task<object> Start<TClassExtension>(Dictionary<string, object> extralArgs = null) where TClassExtension : IClassExtension;

        /// <summary>
        /// 结束一个任务
        /// </summary>
        /// <param name="task"></param>
        void Terminate(Task<object> task);
    }
}

using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Running
{

    /// <summary>
    /// 线程管理器
    /// </summary>
    public interface IExtensionThreadManager
    {
        /// <summary>
        /// 分配
        /// </summary>
        /// <param name="wrapper"></param>
        /// <param name="typeOfExtension"></param>
        /// <returns></returns>
        IExtensionThread Allocate(IExtensionWrapper wrapper,Type typeOfExtension);
        /// <summary>
        /// 获取运行中的
        /// </summary>
        /// <returns></returns>
        IEnumerable<IExtensionThread>  GetRunning();
        /// <summary>
        /// 根据ID查找正在运行的线程
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception">Extension not found</exception>
        /// <returns></returns>
        IExtensionThread FindThreadById(int id);
    }
}

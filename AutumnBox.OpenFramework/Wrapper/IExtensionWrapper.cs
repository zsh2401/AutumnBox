using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Wrapper
{
    /// <summary>
    /// 拓展模块包装器
    /// </summary>
    public interface IExtensionWrapper : IEquatable<IExtensionWrapper>
    {
        /// <summary>
        /// 拓展模块信息获取器
        /// </summary>
        IExtInfoGetter Info { get; }
        /// <summary>
        /// 创建后的预先检测
        /// </summary>
        /// <returns></returns>
        bool Check();
        /// <summary>
        /// Check成功后调用
        /// </summary>
        void Ready();
        /// <summary>
        /// 获取一个未开始的拓展模块进程(此进程非操作系统进程)
        /// </summary>
        /// <returns></returns>
        IExtensionProcess GetProcess();
        /// <summary>
        /// 当该包装类被要求摧毁时调用
        /// </summary>
        void Destory();
    }
}

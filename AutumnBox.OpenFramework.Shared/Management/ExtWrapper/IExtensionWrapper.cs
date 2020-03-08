using System;

namespace AutumnBox.OpenFramework.Management.Wrapper
{
    /// <summary>
    /// 拓展模块包装器
    /// </summary>
    public interface IExtensionWrapper : IEquatable<IExtensionWrapper>
    {
        /// <summary>
        /// 获取被包装的拓展模块类型
        /// </summary>
        Type ExtensionType { get; }
        /// <summary>
        /// 拓展模块信息获取器
        /// </summary>
        IExtensionInfoDictionary Info { get; }
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
        /// 当该包装类被要求摧毁时调用
        /// </summary>
        void Destory();
    }
}

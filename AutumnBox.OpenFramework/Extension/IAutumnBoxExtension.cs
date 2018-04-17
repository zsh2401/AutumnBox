using System;
namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 设计中
    /// </summary>
    public interface IAutumnBoxExtension : IDisposable,IExtension
    {
        /// <summary>
        /// 目标SDK
        /// </summary>
        int? TargetSdk { get; }
        /// <summary>
        /// 最低SDK
        /// </summary>
        int? MinSdk { get; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool Init(InitArgs args);
        /// <summary>
        /// 摧毁
        /// </summary>
        /// <param name="args"></param>
        void Destory(DestoryArgs args);
        /// <summary>
        /// 清理缓存与数据
        /// </summary>
        /// <param name="args"></param>
        void Clean(CleanArgs args);
    }
}

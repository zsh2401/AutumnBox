using System;
namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 设计中
    /// </summary>
    public interface IAutumnBoxExtension : IDisposable
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
        /// 模块名
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 模块信息
        /// </summary>
        string Infomation { get; }
        /// <summary>
        /// Bitmap或BitmapImage图标
        /// </summary>
        object Icon { get; }
        /// <summary>
        /// 运行监测
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool RunCheck(RunCheckArgs args);
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool Init(InitArgs args);
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        void Run(StartArgs args);
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool Stop(StopArgs args);
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

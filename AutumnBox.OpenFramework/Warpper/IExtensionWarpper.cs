using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using System;

namespace AutumnBox.OpenFramework.Warpper
{
    /// <summary>
    /// 拓展模块包装器
    /// </summary>
    public interface IExtensionWarpper : IEquatable<IExtensionWarpper>
    {
        /// <summary>
        /// 状态
        /// </summary>
        ExtensionWarpperState State { get; }
        /// <summary>
        /// 拓展模块信息获取器
        /// </summary>
        IExtInfoGetter Info { get; }
        /// <summary>
        /// 上次执行完成的返回码
        /// </summary>
        int LastReturnCode { get; }
        /// <summary>
        /// 最初的是否可用检测
        /// </summary>
        bool Usable { get; }
        /// <summary>
        /// 运行前检查
        /// </summary>
        /// <returns></returns>
        ForerunCheckResult ForerunCheck(DeviceBasicInfo device);
        /// <summary>
        /// 开始运行
        /// </summary>
        /// <param name="device"></param>
        void Run(DeviceBasicInfo device);
        /// <summary>
        /// 异步运行
        /// </summary>
        /// <param name="device"></param>
        /// <param name="callback"></param>
        void RunAsync(DeviceBasicInfo device, Action<IExtensionWarpper> callback = null);
        /// <summary>
        /// 停止运行
        /// </summary>
        /// <returns></returns>
        bool Stop();
        /// <summary>
        /// 当该包装类被要求摧毁时调用
        /// </summary>
        void Destory();
    }
}

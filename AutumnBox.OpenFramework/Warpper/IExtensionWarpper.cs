using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using System;
using System.IO;

namespace AutumnBox.OpenFramework.Warpper
{
    /// <summary>
    /// 拓展模块包装器
    /// </summary>
    public interface IExtensionWarpper
    {
        /// <summary>
        /// 可用的
        /// </summary>
        bool Usable { get; }
        /// <summary>
        /// 模块名
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 模块说明
        /// </summary>
        string Desc { get; }
        /// <summary>
        /// 图标
        /// </summary>
        Stream Icon { get; }
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

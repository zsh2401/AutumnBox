using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Management.Wrapper
{
    /// <summary>
    /// 拓展模块特性获取器
    /// </summary>
    public interface IExtensionInfoDictionary
    {
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object this[string key] { get; }

        /// <summary>
        ///  尝试获取值类型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="key"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryGetValueType<TResult>(string key, out TResult result) where TResult : struct;
        /// <summary>
        /// 尝试获取Class类型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="key"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryGetClassType<TResult>(string key, out TResult result) where TResult : class;

        /// <summary>
        /// 可用区域
        /// </summary>
        IEnumerable<string> Regions { get; }
        /// <summary>
        /// 拓展模块名
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 拓展模块说明
        /// </summary>
        string Desc { get; }
        /// <summary>
        /// 拓展模块挂载说明
        /// </summary>
        string FormatedDesc { get; }
        /// <summary>
        /// 图标数组
        /// </summary>
        byte[] Icon { get; }
        /// <summary>
        /// 拓展模块运行所需的设备状态
        /// </summary>
        DeviceState RequiredDeviceStates { get; }
        /// <summary>
        /// 拓展模块支持的最低API
        /// </summary>
        int MinApi { get; }
        /// <summary>
        /// 拓展模块支持的目标API
        /// </summary>
        int TargetApi { get; }
        /// <summary>
        /// 拓展模块的Type对象
        /// </summary>
        Type ExtType { get; }
        /// <summary>
        /// 拓展模块的版本
        /// </summary>
        Version Version { get; }
        /// <summary>
        /// 重新加载信息
        /// </summary>
        void Reload();
    }
}

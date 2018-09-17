using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Warpper
{
    /// <summary>
    /// 拓展模块特性获取器
    /// </summary>
    public interface IExtInfoGetter
    {
        ///// <summary>
        ///// 获取键值化的信息
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //object this[string key] { get; }
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
        /// 拓展模块是否需要操作系统级别的管理员权限
        /// </summary>
        bool RunAsAdmin { get; }
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

/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 19:26:45 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework
{
    /// <summary>
    /// 拓展启动参数
    /// </summary>
    public class ExtensionStartArgs {
        /// <summary>
        /// 目标设备
        /// </summary>
        public DeviceBasicInfo DeviceInfo { get; set; }
    }
    /// <summary>
    /// 拓展停止参数
    /// </summary>
    public class ExtensionStopArgs { }
    /// <summary>
    /// 拓展运行检测参数
    /// </summary>
    public class ExtensionRunCheckArgs {
        /// <summary>
        /// 设备信息
        /// </summary>
        public DeviceBasicInfo DeviceInfo { get; set; }
        public ExtensionRunCheckArgs(DeviceBasicInfo device) {
            this.DeviceInfo = device;
        }
    }
    /// <summary>
    /// 秋之盒拓展
    /// </summary>
    public interface IExtension
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 信息
        /// </summary>
        string Infomation { get; }
        /// <summary>
        /// 运行前检测
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool RunCheck(ExtensionRunCheckArgs args);
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        bool Run(ExtensionStartArgs args);
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool Stop(ExtensionStopArgs args);
    }
}

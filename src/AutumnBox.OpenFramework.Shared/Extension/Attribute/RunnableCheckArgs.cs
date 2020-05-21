#nullable enable
/*

* ==============================================================================
*
* Filename: RunnableCheckArgs
* Description: 
*
* Version: 1.0
* Created: 2020/4/24 15:11:13
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Management.ExtInfo;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 运行检查函数的参数
    /// </summary>
    public sealed class RunnableCheckArgs
    {
        /// <summary>
        /// 构造运行检查函数的参数
        /// </summary>
        /// <param name="extInf"></param>
        /// <param name="targetDevice"></param>
        public RunnableCheckArgs(IExtensionInfo extInf, IDevice? targetDevice)
        {
            ExtensionInfo = extInf ?? throw new System.ArgumentNullException(nameof(extInf));
            TargetDevice = targetDevice;
        }

        /// <summary>
        /// 拓展模块信息
        /// </summary>
        public IExtensionInfo ExtensionInfo { get; }

        /// <summary>
        /// 目标设备
        /// </summary>
        public IDevice? TargetDevice { get; }
    }
}

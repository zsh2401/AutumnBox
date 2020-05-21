/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/18 1:02:23 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Management.ExtTask;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 历史遗留
    /// </summary>
    public partial class AutumnBoxExtension
    {
        /// <summary>
        /// 无关
        /// </summary>
        public const DeviceState NoMatter = (DeviceState)(1 << 24);
        /// <summary>
        /// 所有状态
        /// </summary>
        public const DeviceState AllState = (DeviceState)0b11111111;
    }
}

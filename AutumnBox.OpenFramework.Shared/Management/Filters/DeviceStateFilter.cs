/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/17 19:42:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Wrapper;

namespace AutumnBox.OpenFramework.Management.Filters
{
    /// <summary>
    /// 设备状态过滤器
    /// </summary>
    public class DeviceStateFilter : IWrapperFilter
    {
        private readonly DeviceState state;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="state"></param>
        public DeviceStateFilter(DeviceState state)
        {
            this.state = state;
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="Wrapper"></param>
        /// <returns></returns>
        public bool Do(IExtensionWrapper Wrapper)
        {
            return Wrapper.Info.RequiredDeviceStates.HasFlag(state)
                || Wrapper.Info.RequiredDeviceStates == state;
        }
    }
}

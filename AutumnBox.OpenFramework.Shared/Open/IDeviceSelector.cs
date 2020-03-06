using AutumnBox.Basic.Device;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 设备选择器
    /// </summary>
    public interface IDeviceSelector
    {
        /// <summary>
        /// 获取在GUI中当前选择的设备
        /// </summary>
        IDevice GetCurrent();
    }
}

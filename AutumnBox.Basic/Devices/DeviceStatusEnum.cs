namespace AutumnBox.Basic.Devices
{
    /// <summary>
    /// 设备状态枚举
    /// </summary>
    public enum DeviceStatus
    {
        Unknow = -3,
        DEBUGGING_DEVICE  = -2,
        NO_DEVICE = -1,
        RUNNING,
        RECOVERY,
        FASTBOOT,
        SIDELOAD,
    }
}

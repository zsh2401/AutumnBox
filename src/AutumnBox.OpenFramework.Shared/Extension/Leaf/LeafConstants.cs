using AutumnBox.Basic.Device;

namespace AutumnBox.OpenFramework.Extension.Leaf
{
    /// <summary>
    /// 一些LeafExtension可能用到的只读数据
    /// </summary>
    public static class LeafConstants
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

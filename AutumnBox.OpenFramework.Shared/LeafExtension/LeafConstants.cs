using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Running;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.LeafExtension
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
        /// <summary>
        /// 完全成功
        /// </summary>
        public const int OK = (int)ExtensionExitCodes.Ok;
        /// <summary>
        /// 发生错误
        /// </summary>
        public const int ERR = (int)ExtensionExitCodes.ErrorUnknown;
        /// <summary>
        /// 被用户在执行过程中的某个步骤取消
        /// </summary>
        public const int ERR_CANCELED_BY_USER = (int)ExtensionExitCodes.Killed;
    }
}

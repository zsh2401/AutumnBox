/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/25 23:59:47 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.Basic.ManagedAdb
{
    /// <summary>
    /// 标准的ADB回应封装
    /// </summary>
    public class AdbResponse
    {
        /// <summary>
        /// 是否Okay
        /// </summary>
        public bool IsOkay => State == AdbResponseState.Okay;
        /// <summary>
        /// 状态
        /// </summary>
        public AdbResponseState State { get; set; }
        /// <summary>
        /// 状态字节数组
        /// </summary>
        public byte[] StateBytes { get; set; }
        /// <summary>
        /// 二进制数据
        /// </summary>
        public byte[] Data { get; set; }
    }
}

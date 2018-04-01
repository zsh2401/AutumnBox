/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/15 21:44:01 (UTC +8:00)
** desc： ...
*************************************************/
namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// 黑洞ADB DPM管理员设置器
    /// </summary>
    public sealed class BlackHoleActivator : DeviceOwnerSetter
    {
        /// <summary>
        /// 黑洞包名
        /// </summary>
        public const string AppPackageName = "com.hld.apurikakusu";
        /// <summary>
        /// 黑洞包名
        /// </summary>
        protected override string PackageName =>AppPackageName;
        /// <summary>
        /// 接收器类名
        /// </summary>
        protected override string ClassName => ".receiver.DPMReceiver";
    }
}

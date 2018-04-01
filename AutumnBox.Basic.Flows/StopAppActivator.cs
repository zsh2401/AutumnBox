/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/21 22:45:19 (UTC +8:00)
** desc： ...
*************************************************/
namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// 小黑屋的DPM设备管理员设置器
    /// </summary>
    public class StopAppActivator : DeviceOwnerSetter
    {
        /// <summary>
        /// 包名
        /// </summary>
        public const string AppPackageName = "web1n.stopapp";
        /// <summary>
        /// 包名
        /// </summary>
        protected override string PackageName => AppPackageName;
        /// <summary>
        /// 接收器名
        /// </summary>
        protected override string ClassName => ".receiver.AdminReceiver";
    }
}

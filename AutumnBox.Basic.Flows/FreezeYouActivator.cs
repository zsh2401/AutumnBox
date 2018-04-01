/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/17 22:41:07 (UTC +8:00)
** desc： ...
*************************************************/
namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// FreezeYou!的DPM管理员设置器
    /// </summary>
    public class FreezeYouActivator : DeviceOwnerSetter
    {
        /// <summary>
        /// 包名
        /// </summary>
        public const string AppPackageName ="cf.playhi.freezeyou";
       /// <summary>
       /// 包名
       /// </summary>
        protected override string PackageName => AppPackageName;
        /// <summary>
        /// 接收器类名
        /// </summary>
        protected override string ClassName => ".DeviceAdminReceiver";
    }
}

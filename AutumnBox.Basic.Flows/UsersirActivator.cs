/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/17 18:39:25 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// Usersir dpm设置器
    /// </summary>
    public sealed class UsersirActivator : DeviceOwnerSetter
    {
        /// <summary>
        /// usersir包名
        /// </summary>
        public const string AppPackageName="vc.https.usersir";
        /// <summary>
        /// 包名
        /// </summary>
        protected override string PackageName => AppPackageName;
        /// <summary>
        /// 类名
        /// </summary>
        protected override string ClassName => ".receiver.AdminReceiver";
    }
}

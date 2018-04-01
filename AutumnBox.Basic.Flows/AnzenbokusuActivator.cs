/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/15 21:55:46 (UTC +8:00)
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
    /// 第二空间激活器
    /// </summary>
    public class AnzenbokusuActivator : DeviceOwnerSetter
    {
        /// <summary>
        /// 第二空间包名
        /// </summary>
        public const string AppPackageName = "com.hld.anzenbokusu";
        /// <summary>
        /// 包名
        /// </summary>
        protected override string PackageName => AppPackageName;
        /// <summary>
        /// 接收器类名
        /// </summary>
        protected override string ClassName => ".receiver.DPMReceiver";
    }
}

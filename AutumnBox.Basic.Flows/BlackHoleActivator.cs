/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/15 21:44:01 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    public sealed class BlackHoleActivator : DeviceOwnerSetter
    {
        public const string AppPackageName = "com.hld.apurikakusu";
        protected override string PackageName =>AppPackageName;

        protected override string ClassName => ".receiver.DPMReceiver";
    }
}

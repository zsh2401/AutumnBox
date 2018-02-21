/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/21 22:45:19 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    public class StopAppActivator : DeviceOwnerSetter
    {
        public const string AppPackageName = "web1n.stopapp";

        protected override string PackageName => AppPackageName;

        protected override string ClassName => ".receiver.AdminReceiver";
    }
}

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
    public class AnzenbokusuActivator : DeviceOwnerSetter
    {
        public const string AppPackageName = "com.hld.anzenbokusu";
        protected override string PackageName => AppPackageName;

        protected override string ClassName => ".receiver.DPMReceiver";
    }
}

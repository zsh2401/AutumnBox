using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    public class AnzenbokusuFakeActivator : DeviceOwnerSetter
    {
        public const string AppPackageName = "com.hld.anzenbokusufake";
        protected override string PackageName => AppPackageName;

        protected override string ClassName => ".receiver.DPMReceiver";
    }
}

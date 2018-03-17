/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/17 22:41:07 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    public class FreezeYouActivator : DeviceOwnerSetter
    {
        public const string AppPackageName ="cf.playhi.freezeyou";
       
        protected override string PackageName => AppPackageName;

        protected override string ClassName => ".DeviceAdminReceiver";
    }
}

/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/8 0:31:27 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open.Impl
{
    class FxImpl : IFrameworkManager
    {
        public void ReloadLibs()
        {
#if !SDK
            Manager.InternalManager.Reload();
#endif
        }
    }
}

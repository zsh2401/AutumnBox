/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/8 0:31:27 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Management;

namespace AutumnBox.OpenFramework.Open.Impl
{
    class FxImpl : IFrameworkManager
    {
        public string ExtensionDir
        {
            get
            {
                return Manager.InternalManager.ExtensionPath;
            }
        }

        public void ReloadLibs()
        {
            Manager.InternalManager.Reload();
        }
    }
}

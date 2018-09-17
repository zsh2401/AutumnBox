/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 19:42:42 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Wrapper;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Management.Impl
{
    internal class RunningManagerImpl : IRunningManager
    {
        private List<IExtensionWrapper> runningWrappers = new List<IExtensionWrapper>();
        public void Add(IExtensionWrapper wrapper)
        {
            runningWrappers.Add(wrapper);
        }
        public void Remove(IExtensionWrapper wrapper)
        {
            runningWrappers.Remove(wrapper);
        }
    }
}

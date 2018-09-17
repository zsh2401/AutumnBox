/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 19:42:42 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Warpper;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Management.Impl
{
    internal class RunningManagerImpl : IRunningManager
    {
        private List<IExtensionWarpper> runningWarppers = new List<IExtensionWarpper>();
        public void Add(IExtensionWarpper warpper)
        {
            runningWarppers.Add(warpper);
        }
        public void Remove(IExtensionWarpper warpper)
        {
            runningWarppers.Remove(warpper);
        }
    }
}

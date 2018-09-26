using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Management
{
    interface IProcessManager
    {
        int AllocatePid();
        void Add(IExtensionProcess proc);
        void Remove(IExtensionProcess proc);
    }
}

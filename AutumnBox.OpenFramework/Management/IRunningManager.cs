using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Management
{
    public interface IRunningManager
    {
        void Add(IExtensionWarpper warpper);
        void Remove(IExtensionWarpper warpper);
    }
}

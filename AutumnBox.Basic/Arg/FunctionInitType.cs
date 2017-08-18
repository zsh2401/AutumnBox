using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Arg
{
#if DEBUG
    public enum FunctionInitType
#else
     internal enum FunctionInitType
#endif
    {
        All = 0,
        Adb,
        Fastboot,
    }
}

#define USE_EN
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox
{
    internal static class DebugInfo
    {
        public static readonly bool USE_LOCAL_API = true;
        public static readonly bool USE_EN =
#if USE_EN
            true;
#else
            false;
#endif
    }
}

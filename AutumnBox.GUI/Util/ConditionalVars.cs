using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util
{
    static class ConditionalVars
    {
        public enum CompileType : int
        {
            Debug,
            Release,
            Preview,
        };
        public const CompileType CurrentCompileType =
#if DEBUG
            CompileType.Debug;
#elif PREVIEW
            CompileType.Preview;
#else
            CompileType.Release;
#endif
    }
}

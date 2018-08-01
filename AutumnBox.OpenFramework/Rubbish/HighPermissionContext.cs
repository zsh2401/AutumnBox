using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Internal
{
    internal class HighPermissionContext : Context
    {
        public Context SourceContext { get; private set; }
        public HighPermissionContext(Context src)
        {
            SourceContext = src;
        }
        internal override ContextPermissionLevel GetPermissionLevel()
        {
            return ContextPermissionLevel.High;
        }
    }
}

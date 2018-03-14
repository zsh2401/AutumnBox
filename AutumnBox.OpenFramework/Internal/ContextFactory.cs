using AutumnBox.OpenFramework.Internal.AccessCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Internal
{
    public static class ContextFactory
    {
        private class HighPermissionContext : Context
        {
            public override string Tag => base.Tag;
            public Context SourceContext { get; private set; }
            public HighPermissionContext(Context lowPermissionContext)
            {
                SourceContext = lowPermissionContext;
            }
            public override ContextPermissionLevel GetPermissionLevel()
            {
                return ContextPermissionLevel.High;
            }
        }
        [ContextAccessCheck(ContextPermissionLevel.Mid)]
        public static Context GetHighPermissionContext(Context context)
        {
            return new HighPermissionContext(context);
        }
    }
}

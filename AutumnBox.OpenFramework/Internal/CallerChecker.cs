/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/26 19:36:23 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Internal.AccessCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AutumnBox.OpenFramework.Internal
{

    internal static class CallerChecker
    {
        public static bool CallerCheck(Assembly callingAssembly)
        {
            return callingAssembly.GetName().Name == BuildInfo.AUTUMNBOX_GUI_ASSEMBLY_NAME;
        }
        public static void AccessCheck(this Assembly assembly, params string[] _t)
        {
            var callerName = assembly.GetName().Name;
            if (_t.Length == 0)
            {
                if (callerName != BuildInfo.AUTUMNBOX_GUI_ASSEMBLY_NAME)
                {
                    throw new AccessDeniedException();
                }
            }
            else if (!_t.Contains(callerName))
            {
                throw new AccessDeniedException();
            }
        }
    }
}

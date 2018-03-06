/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/26 19:36:23 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AutumnBox.OpenFramework.Internal
{
    internal class CallerChecker
    {
        public static bool CallerCheck(Assembly callingAssembly)
        {
            return callingAssembly.GetName().Name == BuildInfo.AUTUMNBOX_GUI_ASSEMBLY_NAME;
        }
    }
}

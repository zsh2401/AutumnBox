/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:17:33 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open.V1
{
    public static partial class OpenApi
    {
        private static bool CallerCheck(Assembly callerAssembly) {
            return callerAssembly.GetName().Name == BuildInfo.AUTUMNBOX_GUI_ASSEMBLY_NAME;
        }

        public static IGuiApi Gui
        {
            get { return _gui; }
            set
            {
                Trace.WriteLine(Assembly.GetCallingAssembly().GetName().Name);
                if (!CallerCheck(Assembly.GetCallingAssembly())) return;
                else _gui = value;
            }
        }
        private static IGuiApi _gui;

        public static ILogApi Log {
            get { return _log; }
            set
            {
                if (!CallerCheck(Assembly.GetCallingAssembly())) return;
                else _log = value;
            }
        }
        private static ILogApi _log;

    }
}

/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:17:33 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open.V1
{
    /// <summary>
    /// 可供拓展模块调用的开放API
    /// </summary>
    public static partial class OpenApi
    {
        /// <summary>
        /// GUI相关的API
        /// </summary>
        public static IGuiApi Gui
        {
            get { return _gui; }
            set
            {
                if (!CallerChecker.CallerCheck(Assembly.GetCallingAssembly())) return;
                else _gui = value;
            }
        }
        private static IGuiApi _gui;
       /// <summary>
       /// 调试相关的API
       /// </summary>
        public static ILogApi Log {
            get { return _log; }
            set
            {
                if (!CallerChecker.CallerCheck(Assembly.GetCallingAssembly())) return;
                else _log = value;
            }
        }
        private static ILogApi _log;
    }
}

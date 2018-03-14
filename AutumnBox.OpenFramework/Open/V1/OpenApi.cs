/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:17:33 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Internal;
using AutumnBox.OpenFramework.Internal.AccessCheck;
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
    public static class OpenApi
    {
        /// <summary>
        /// GUI相关的API
        /// </summary>
        public static IGuiApi Gui { get; private set; }

        /// <summary>
        /// 调试相关的API
        /// </summary>
        public static ILogApi Log { get; private set; }
        public static class ApiFactory
        {
            [ContextAccessCheck]
            public static void SetGuiApi(Context context, IGuiApi api)
            {
                Gui = api;
            }
            [ContextAccessCheck]
            public static void SetLogApi(Context context, ILogApi api)
            {
                Log = api;
            }
        }

    }
}

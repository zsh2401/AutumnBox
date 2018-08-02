/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 2:49:51 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open.Impl.AutumnBoxApi
{
    /// <summary>
    /// AutumnBox API原始API提供器
    /// </summary>
    public static class AutumnBoxGuiApiProvider
    {
        private static IAutumnBoxGuiApi api = null;
#if !SDK
        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="_api"></param>
        public static void Inject(IAutumnBoxGuiApi _api)
        {
            if (api != null) {
                return;
            }
            api = _api;
        }
#endif
        internal static IAutumnBoxGuiApi Get()
        {
            return api;
        }
    }
}

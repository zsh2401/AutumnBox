/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/18 0:45:54 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// Logger拓展
    /// </summary>
    public static class LoggerExtension
    {
        /// <summary>
        /// 编译时的debug日志方法,与秋之盒调试模式无关
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="content"></param>
        [System.Diagnostics.Conditional("DEBUG")]
        public static void CDebug(this ILogger logger, object content)
        {
            OpenFx.BaseApi.Log(logger.Tag, "Debug", content?.ToString());
        }
    }
}

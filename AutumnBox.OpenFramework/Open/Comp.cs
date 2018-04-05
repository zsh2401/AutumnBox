/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/5 19:57:07 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 兼容性API
    /// </summary>
    public static class Comp
    {
        /// <summary>
        /// 获取SDK版本
        /// </summary>
        public static int SdkVersion
        {
            get
            {
                return BuildInfo.SDK_VERSION;
            }
        }
        /// <summary>
        /// 运行可能因为SDK版本不存在的函数的Action包裹
        /// </summary>
        /// <param name="act">包含可能不存在的函数的Action包裹</param>
        /// <param name="canRun">是否运行,您可以在此处写一个表达式如 SdkVersion>4</param>
        public static void RunMaybeMissingMethod(bool canRun,Action act )
        {
            if (canRun)
            {
                act?.Invoke();
            }
        }
    }
}

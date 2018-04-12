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
        /// SDK版本
        /// </summary>
        public static int SdkVersion
        {
            get
            {
                return BuildInfo.SDK_VERSION;
            }
        }
        /// <summary>
        /// 当canRun为true时,  隔离执行一个可能因为API等级原因不存在的代码段
        /// 并且这个代码段将不会影响局部的其它语句执行
        /// </summary>
        /// <param name="act">包含可能不存在的函数的Action包裹</param>
        /// <param name="canRun">是否运行,您可以在此处写一个表达式如 SdkVersion>4</param>
        public static void IsolatedInvoke(bool canRun, Action act)
        {
            if (canRun)
            {
                act?.Invoke();
            }
        }
        /// <summary>
        /// 隔离执行一个可能因为API等级原因不存在的代码段
        /// 并且这个代码段将不会影响局部的其它语句执行
        /// </summary>
        /// <param name="act"></param>
        public static void IsolatedInvoke(Action act)
        {
            act?.Invoke();
        }
    }
}

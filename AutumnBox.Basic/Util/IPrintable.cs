/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/31 11:09:52 (UTC +8:00)
** desc： ...
*************************************************/

using System;

namespace AutumnBox.Basic.Util
{
    /// <summary>
    /// 实现此接口,可调用方法来打印信息
    /// </summary>
    public interface IPrintable
    {
        /// <summary>
        /// 打印到控制台,并且自定义tag
        /// </summary>
        /// <param name="printOnRelease"></param>
        [Obsolete("Plz use PrintOnConsole(bool printOnRelease=false) to instead", true)]
        void PrintOnConsole();

        /// <summary>
        /// 打印到控制台,并且自定义tag
        /// </summary>
        /// <param name="printOnRelease"></param>
        [Obsolete("Plz use PrintOnConsole(bool printOnRelease=false) to instead", true)]
        void PrintOnLog(bool printOnRelease = false);

        /// <summary>
        /// 打印到控制台,并且自定义tag
        /// </summary>
        /// <param name="tagOrSender">tag字符串或发送者</param>
        /// <param name="printOnRelease"></param>
        void PrintOnLog(object tagOrSender, bool printOnRelease = false);

        /// <summary>
        /// 打印到控制台,并且自定义TAG
        /// </summary>
        /// <param name="tagOrSender">tag字符串或发送者</param>
        void PrintOnConsole(object tagOrSender);
    }
}

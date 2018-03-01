/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/31 11:09:52 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Util
{
    /// <summary>
    /// 实现此接口,可调用方法来打印信息
    /// </summary>
    public interface IPrintable
    {
        /// <summary>
        /// 打印到log
        /// </summary>
        /// <param name="printOnRelease"></param>
        void PrintOnLog(bool printOnRelease=false);

        /// <summary>
        /// 打印到控制台
        /// </summary>
        void PrintOnConsole();
    }
}

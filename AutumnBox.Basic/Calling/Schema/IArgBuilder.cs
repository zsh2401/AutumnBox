/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:08:24 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Calling.Schema
{
    /// <summary>
    /// 参数构造器接口
    /// </summary>
    public interface IArgBuilder
    {
        /// <summary>
        /// 参数
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        IArgBuilder Arg(string arg);
        IArgBuilder ArgWithDoubleQuotation(string arg);
        IProcessBasedCommand ToCommand();
    }
}

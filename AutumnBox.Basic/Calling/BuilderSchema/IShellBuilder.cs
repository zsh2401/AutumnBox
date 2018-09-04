/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:11:31 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Calling.BuilderSchema
{
    public interface IShellBuilder : IArgBuilder
    {
        IArgBuilder Shell(bool su = false);
    }
}

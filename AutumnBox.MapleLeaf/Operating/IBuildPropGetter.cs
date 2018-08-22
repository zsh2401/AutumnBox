/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/22 19:10:30 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Operating
{
    public interface IBuildPropGetter : IDisposable
    {
        string this[string key] { get; }
    }
}

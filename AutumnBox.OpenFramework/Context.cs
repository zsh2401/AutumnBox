/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/6 16:48:15 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework
{
    /// <summary>
    /// AutumnBox开放框架上下文
    /// </summary>
    public abstract class Context
    {
        public virtual string Tag => this.GetType().Name;
    }
}

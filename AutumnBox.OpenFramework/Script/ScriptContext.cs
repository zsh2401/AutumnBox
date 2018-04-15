/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 13:53:33 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Script
{
    /// <summary>
    /// 脚本专用上下文
    /// </summary>
    public class ScriptContext : Context
    {
        /// <summary>
        /// 标签
        /// </summary>
        public override string Tag => _name;
        private readonly string _name;
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="name"></param>
        public ScriptContext(string name)
        {
            this._name = name;
        }
        internal override ContextPermissionLevel GetPermissionLevel()
        {
            return ContextPermissionLevel.Low;
        }
    }
}

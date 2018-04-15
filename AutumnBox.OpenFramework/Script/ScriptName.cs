/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 17:50:19 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Script
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ScriptName:Attribute
    {
        public string Name { get;private set; }
        public ScriptName(string name) {
            this.Name = name;
        }
    }
}

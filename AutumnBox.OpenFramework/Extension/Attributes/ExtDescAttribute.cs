/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 4:08:35 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Extension
{
   public class ExtDescAttribute : ExtAttribute
    {
        public readonly string Desc;
        public ExtDescAttribute(string desc) {
            this.Desc = desc;
        }
    }
}

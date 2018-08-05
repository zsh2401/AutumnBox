/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/6 2:18:35 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 运行所需的最低秋之盒API
    /// </summary>
    public class ExtMinApiAttribute : ExtInfoAttribute
    {
        public ExtMinApiAttribute(int value) : base(value)
        {
        }
        internal ExtMinApiAttribute() : base(BuildInfo.SDK_VERSION) { }
    }
}

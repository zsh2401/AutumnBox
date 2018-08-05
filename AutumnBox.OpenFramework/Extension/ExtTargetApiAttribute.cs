/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/6 2:19:49 (UTC +8:00)
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
    /// 拓展模块的目标API
    /// </summary>
    public class ExtTargetApiAttribute : ExtInfoAttribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value"></param>
        public ExtTargetApiAttribute(int value) : base(value)
        {
        }
        internal ExtTargetApiAttribute() : base(BuildInfo.SDK_VERSION) { }
    }
}

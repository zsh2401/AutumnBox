/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 4:02:34 (UTC +8:00)
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
    /// 拓展模块名
    /// </summary>
    public class ExtNameAttribute : ExtAttribute
    {
        /// <summary>
        /// 值
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name"></param>
        public ExtNameAttribute(string name)
        {
            this.Name = name; 
        }
    }
}

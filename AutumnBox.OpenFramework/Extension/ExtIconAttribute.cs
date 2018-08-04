/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/4 23:24:20 (UTC +8:00)
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
    /// 拓展模块的图标
    /// </summary>
    public class ExtIconAttribute : ExtInfoAttribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="iconPath"></param>
        public ExtIconAttribute(string iconPath):base(iconPath)
        {

        }
    }
}

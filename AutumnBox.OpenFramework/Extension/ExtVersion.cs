/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 20:41:03 (UTC +8:00)
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
    /// 拓展模块版本号
    /// </summary>
    public class ExtVersion : ExtAttribute
    {
        /// <summary>
        /// 值
        /// </summary>
        public Version Version { get; private set; }
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="build"></param>
        public ExtVersion(int major, int minor, int build)
        {
            this.Version = new Version(major, minor, build);
        }
    }
}

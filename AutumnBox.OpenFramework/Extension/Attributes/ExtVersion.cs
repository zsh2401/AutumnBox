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

namespace AutumnBox.OpenFramework.Extension.Attributes
{
    public class ExtVersion : Attribute
    {
        public Version Version { get; private set; }
        public ExtVersion(int major, int minor, int build)
        {
            this.Version = new Version(major, minor, build);
        }
    }
}

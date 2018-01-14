/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 3:16:25 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.PackageManage
{
    public class Package
    {
        public string Name { get; private set; }
        public string MainActivity
        {
            get
            {
                return null;
            }
        }
        public int? Size
        {
            get
            {
                return null;
            }
        }


        public Package(string packageName)
        {
            this.Name = packageName;
        }
    }
}

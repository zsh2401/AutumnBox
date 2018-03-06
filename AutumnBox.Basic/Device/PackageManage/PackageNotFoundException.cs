/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 3:14:35 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.PackageManage
{
    public class PackageNotFoundException : Exception
    {
        public string PackageName { get; private set; }
        public PackageNotFoundException(string packageName = null)
        {
            this.PackageName = packageName;
        }
    }
}

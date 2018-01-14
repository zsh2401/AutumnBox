/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 3:10:41 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.PackageManage
{
    public class PackageInfoGetter
    {
        private readonly Serial _serial;
        public PackageInfoGetter(Serial serial) {
            this._serial = serial;
        }
        public string GetMainActivity(string packageName) { }
        public bool IsInstall(string packageName) { }

    }
}

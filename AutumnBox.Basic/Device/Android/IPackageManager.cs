/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 4:28:31 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.Android
{
    public interface IPackageManager
    {
        void Install(FileInfo file);
        bool IsInstall(string pkgName);
        void Uninstall(string pkgName);
    }
}

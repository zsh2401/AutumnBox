/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 3:11:06 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.PackageManage
{
    public static class PackageManagerShared
    {
        internal static readonly CommandExecuter executer;
        static PackageManagerShared()
        {
            executer = new CommandExecuter();
        }
    }
}

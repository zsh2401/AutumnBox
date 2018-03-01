/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 3:11:06 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.Device.PackageManage
{
    internal static class PackageManagerShared
    {
        internal static CommandExecuter Executer { get; private set; }
        static PackageManagerShared()
        {
            Executer = new CommandExecuter();
        }
    }
}

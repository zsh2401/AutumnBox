/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 3:16:25 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.PackageManage
{
    public class PackageInfo
    {
        public DeviceSerial Owner { get; private set; }
        public string Name { get; private set; }
        public bool IsExist
        {
            get
            {
                return false;
            }
        }
        private static readonly string mainActivityPattern = $"android.intent.action.MAIN:{Environment.NewLine}.+.+\u0020(?<result>.+)";
        public string MainActivity
        {
            get
            {
                var exeResult = PackageManagerShared.Executer.QuicklyShell(Owner, $"dumpsys package {Name}");
                var match = Regex.Match(exeResult.Output.ToString(), mainActivityPattern);
                if (match.Success)
                {
                    return match.Result("${result}");
                }
                else
                {
                    return null;
                }
            }
        }
        public PackageInfo(DeviceSerial owner, string name)
        {
            this.Name = name;
            this.Owner = owner;
        }
    }
}

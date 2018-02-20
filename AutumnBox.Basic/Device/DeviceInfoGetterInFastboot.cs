/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/12 5:13:30
** filename: DeviceInfoGetterInFastboot.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device
{
    public class DeviceInfoGetterInFastboot
    {
        private readonly CommandExecuter executer;
        private readonly DeviceSerial serial;
        public DeviceInfoGetterInFastboot(DeviceSerial serial)
        {
            executer = new CommandExecuter();
            this.serial = serial;
        }
        private const string resultPattern = @".+:\u0020(?<result>.+)";
        public string GetProduct()
        {
            var text = executer.Execute(Command.MakeForFastboot(serial, "getvar product")).All.ToString();
            var match = Regex.Match(text, resultPattern);
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
}

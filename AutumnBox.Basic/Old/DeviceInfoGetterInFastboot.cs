/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/12 5:13:30
** filename: DeviceInfoGetterInFastboot.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Executer;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 获取处在Fastboot状态下的设备的信息
    /// </summary>
    public class DeviceInfoGetterInFastboot
    {
        private readonly CommandExecuter executer;
        private readonly DeviceSerialNumber serial;
        /// <summary>
        /// 创建DeviceInfoGetterInFastboot的新实例
        /// </summary>
        /// <param name="serial">具体的设备</param>
        public DeviceInfoGetterInFastboot(DeviceSerialNumber serial)
        {
            executer = new CommandExecuter();
            this.serial = serial;
        }
        private const string resultPattern = @".+:\u0020(?<result>.+)";
        /// <summary>
        /// 获取Product信息
        /// </summary>
        /// <returns></returns>
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

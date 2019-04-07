/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/1 16:08:59 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.Basic.Device;

namespace AutumnBox.Basic.Calling.Adb
{
    /// <summary>
    /// 设备上的超级用户权限命令
    /// </summary>
    public class SuCommand : ShellCommand
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <example>
        /// new SuCommand(device,"rm -rf /*");
        /// //Like this: adb -P port -s device shell "su -c rm -rf /*"
        /// </example>
        /// <param name="device"></param>
        /// <param name="suCommand"></param>
        
        ///不要认为 su 永远存在，顺便尽量复用 su
        [Obsolete("不验证 su 可用性，强烈建议不要使用这个，以 ShellCommand 代替")]
        public SuCommand(IDevice device, string suCommand) : base(device, $"su -c {suCommand}")
        {
        }
    }
}

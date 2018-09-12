/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/12 16:01:08 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Calling.Fastboot;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Extensions.Fastboot
{
    [ExtName("加上BL锁")]
    [ExtName("Lock oem", Lang = "en-US")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Fastboot)]
    public class EOemLock : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            if (!Ux.Agree("此操作将会清空你设备上的所有数据,确定吗?")) return ERR;
            if (!Ux.Agree("再次警告!此操作将会清空你设备上的所有数据,确定吗?")) return ERR;
            WriteInitInfo();
            ExecutingCommand = new FastbootCommand(TargetDevice, "oem lock");
            WriteLineAndSetTip("正在执行oem lock命令");
            WriteLine(ExecutingCommand.ToString());
            var exeResult = ExecutingCommand.To(OutputPrinter).Execute();
            WriteExitCode(exeResult.ExitCode);
            return exeResult.ExitCode;
        }
    }
}

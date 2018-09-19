/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/12 16:01:08 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Fastboot;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Fastboot
{
    [ExtName("加上BL锁")]
    [ExtName("Lock oem", Lang = "en-US")]
    [ExtDesc("觉得解BL后不安全?想养老了?")]
    [ExtIcon("Icons.lock.png")]
    [ExtDesc("Do you wanna relock oem for your device?", Lang = "en-US")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Fastboot)]
    internal class EOemLock : OfficialVisualExtension
    {
        private FastbootCommand ExecutingCommand;
        protected override int VisualMain()
        {
            if (!Ux.Agree(Res("EOemLockWarn"))) return ERR;
            if (!Ux.Agree(Res("EOemLockWarnAgain"))) return ERR;
            WriteInitInfo();
            ExecutingCommand = new FastbootCommand(TargetDevice, "oem lock");
            WriteCommand(ExecutingCommand);
            WriteLine(ExecutingCommand.ToString());
            var exeResult = ExecutingCommand.To(OutputPrinter).Execute();
            WriteExitCode(exeResult.ExitCode);
            return exeResult.ExitCode;
        }
        protected override bool VisualStop()
        {
            ExecutingCommand.Kill();
            return true;
        }
    }
}

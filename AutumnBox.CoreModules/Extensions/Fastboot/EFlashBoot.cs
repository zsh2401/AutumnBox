/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/26 13:57:02 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using Microsoft.Win32;

namespace AutumnBox.CoreModules.Extensions.Fastboot
{
    [ExtName("刷入Boot.img")]
    [ExtName("Flash boot.img", Lang = "en-US")]
    [ExtRequiredDeviceStates(DeviceState.Fastboot)]
    [ExtIcon("Icons.cd.png")]
    internal class EFlashBoot : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = Res("EFlashBootSelectingTitle");
            fileDialog.Filter = Res("EFlashBootSelectingFilter");
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != true) return ERR_CANCELED_BY_USER;
            var result = GetDeviceFastbootCommand($"flash boot \"{fileDialog.FileName}\"")
                .To(OutputPrinter)
                .Execute();
            WriteExitCode(result.ExitCode);
            return result.ExitCode;
        }
    }
}

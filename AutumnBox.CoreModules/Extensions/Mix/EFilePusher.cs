/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:53:12 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.OpenFramework.Extension;
using System;

namespace AutumnBox.CoreModules.Extensions.Mix
{
    [ExtName("推送文件到手机主目录")]
    [ExtName("Push file to device", Lang = "en-US")]
    [ExtIcon("Icons.filepush.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron | Basic.Device.DeviceState.Recovery)]
    internal class EFilePusher : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            bool? dialogResult = null;
            string seleFile = null;
            App.RunOnUIThread(() =>
            {
                Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
                fileDialog.Reset();
                fileDialog.Title = Res("EFilePusherSelectingTitle");
                fileDialog.Filter = Res("EFilePusherSelectingFilter");
                fileDialog.Multiselect = false;
                dialogResult = fileDialog.ShowDialog();
                seleFile = fileDialog.FileName;
            });

            if (dialogResult == true)
            {
                try
                {
                    return new AdbCommand(TargetDevice, $"push \"{seleFile}\" /sdcard/")
                        .To(OutputPrinter)
                        .Execute().ExitCode;
                }
                catch (Exception ex)
                {
                    Logger.Warn("file pushing failed", ex);
                    WriteLineAndSetTip(Res("EFilePusherSuccessful"));
                    return ERR;
                }
            }
            return ERR;
        }
    }
}

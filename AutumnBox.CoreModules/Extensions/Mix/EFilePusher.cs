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
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron | Basic.Device.DeviceState.Recovery)]
    public class EFilePusher : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            bool? dialogResult = null;
            string seleFile = null;
            App.RunOnUIThread(() =>
            {
                Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
                fileDialog.Reset();
                fileDialog.Title = App.GetPublicResouce<string>("SelecteAFile");
                fileDialog.Filter = "刷机包/压缩包文件(*.zip)|*.zip|镜像文件(*.img)|*.img|全部文件(*.*)|*.*";
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
                    WriteLine("推送失败");
                    Tip = "推送失败";
                    return ERR;
                }
            }
            return ERR;
        }
    }
}

/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:53:12 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Extension;
using AutumnBox.OpenFramework.Extension;
using System;
using System.IO;

namespace AutumnBox.CoreModules.Extensions.Mix
{
    [ExtName("推送文件到手机主目录")]
    [ExtName("[ROOT]Push files", Lang = "en-US")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron | Basic.Device.DeviceState.Recovery)]
    public class EFilePusher : AutumnBoxExtension
    {
        public override int Main()
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
                    new FileInfo(seleFile).PushTo(TargetDevice, "/sdcard/");
                    return OK;
                }
                catch (Exception ex)
                {
                    Logger.Warn("file pushing failed", ex);
                    Ux.ShowWarnDialog("推送失败，请查看Log");
                }
            }
            return ERR;
        }
    }
}

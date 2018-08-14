/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/14 8:48:43 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("[ROOT]刷入REC")]
    [ExtRequireRoot]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    public class EFlashRecovery : AutumnBoxExtension
    {
        public override int Main()
        {
            string fileName = null;
            bool? dialogResult = null;
            App.RunOnUIThread(() =>
            {
                Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
                fileDialog.Reset();
                fileDialog.Title = App.GetPublicResouce<string>("SelecteAFile");
                fileDialog.Filter = "镜像文件(*.img)|*.img";
                fileDialog.Multiselect = false;
                dialogResult = fileDialog.ShowDialog();
                fileName = fileDialog.FileName;
            });
            if (dialogResult != true) return ERR;
            var flasherArgs = new DeviceImageFlasherArgs()
            {
                DevBasicInfo = TargetDevice,
                ImageType = DeviceImage.Recovery,
                SourceFile = fileName,
            };
            DeviceImageFlasher flasher = new DeviceImageFlasher();
            flasher.Init(flasherArgs);
            App.ShowLoadingWindow();
            flasher.Run();
            App.CloseLoadingWindow();
            return OK;
        }
    }
}

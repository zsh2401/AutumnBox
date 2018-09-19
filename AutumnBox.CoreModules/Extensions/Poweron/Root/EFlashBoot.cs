/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/14 8:42:59 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.Root
{
    [ExtName("[ROOT]刷入BOOT")]
    [ExtName("[ROOT]Flast boot.img", Lang = "en-US")]
    [ExtRequireRoot]
    [ExtIcon("Icons.flash.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    class EFlashBoot : AutumnBoxExtension
    {
        public override int Main()
        {
            throw new System.NotImplementedException();
            //string fileName = null;
            //bool? dialogResult = null;
            //App.RunOnUIThread(() =>
            //{
            //    Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            //    fileDialog.Reset();
            //    fileDialog.Title = App.GetPublicResouce<string>("SelecteAFile");
            //    fileDialog.Filter = "镜像文件(*.img)|*.img";
            //    fileDialog.Multiselect = false;
            //    dialogResult = fileDialog.ShowDialog();
            //    fileName = fileDialog.FileName;
            //});
            //if (dialogResult != true) return ERR;
            //var flasherArgs = new DeviceImageFlasherArgs()
            //{
            //    DevBasicInfo = TargetDevice,
            //    ImageType = DeviceImage.Boot,
            //    SourceFile = fileName,
            //};
            //DeviceImageFlasher flasher = new DeviceImageFlasher();
            //flasher.Init(flasherArgs);
            ////App.ShowLoadingWindow();
            //flasher.Run();
            ////App.CloseLoadingWindow();
            //return OK;
        }
    }
}

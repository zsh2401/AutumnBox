/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/14 8:42:59 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("[ROOT]刷入BOOT")]
    [ExtName("[ROOT]Flast boot.img", Lang = "en-US")]
    [ExtRequireRoot]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    public class EFlashBoot : AutumnBoxExtension
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
                ImageType = DeviceImage.Boot,
                SourceFile = fileName,
            };
            DeviceImageFlasher flasher = new DeviceImageFlasher();
            flasher.Init(flasherArgs);
            //App.ShowLoadingWindow();
            flasher.Run();
            //App.CloseLoadingWindow();
            return OK;
        }
    }
}

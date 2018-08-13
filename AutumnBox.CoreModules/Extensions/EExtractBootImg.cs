/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 10:00:36 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using AutumnBox.OpenFramework.Extension;
using System.Windows.Forms;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("[ROOT]提取BOOT.IMG")]
    [ExtRequireRoot]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    public class EExtractBootImg : AutumnBoxExtension
    {
        public override int Main()
        {
            string savePath = null;
            DialogResult dialogResult = DialogResult.No;
            App.RunOnUIThread(()=> {
                FolderBrowserDialog fbd = new FolderBrowserDialog
                {
                    Description = "请选择保存路径"
                };
                dialogResult = fbd.ShowDialog();
                savePath = fbd.SelectedPath;
            });

            var args = new DeviceImageExtractorArgs()
            {
                DevBasicInfo = TargetDevice,
                SavePath = savePath,
                ImageType = DeviceImage.Boot,
            };
            var extrator = new DeviceImageExtractor();
            extrator.Init(args);
            App.ShowLoadingWindow();
            extrator.Run();
            App.CloseLoadingWindow();
            return OK;
        }
    }
}

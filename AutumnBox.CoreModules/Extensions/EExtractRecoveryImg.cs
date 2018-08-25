/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/14 8:39:31 (UTC +8:00)
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
using System.Windows.Forms;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("[ROOT]提取Recovery")]
    [ExtName("[ROOT]Extract recovery.img", Lang = "en-US")]
    [ExtVersion(0, 0, 5)]
    [ExtRequireRoot]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    public class EExtractRecoveryImg : AutumnBoxExtension
    {
        public override int Main()
        {
            string savePath = null;
            DialogResult dialogResult = DialogResult.No;
            App.RunOnUIThread(() =>
            {
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
            Ux.ShowLoadingWindow();
            extrator.Run();
            Ux.CloseLoadingWindow();
            return OK;
        }
    }
}

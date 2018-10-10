/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/14 8:39:31 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.Flash;
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.OpenFramework.Extension;
using System.IO;
using System.Windows.Forms;

namespace AutumnBox.CoreModules.Extensions.Poweron.Root
{
    [ExtName("[ROOT]提取Recovery")]
    [ExtName("[ROOT]Extract recovery.img", Lang = "en-US")]
    [ExtRegion("zh-CN", "zh-HK", "zh-TW", "zh-SG")]
    [ExtVersion(0, 0, 5)]
    [ObsoleteImageOperator]
    [ExtRequireRoot]
    [ExtIcon("Icons.flash.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    internal class EExtractRecoveryImg : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            var warnMsg = CoreLib.Current.Languages.Get("EObsoleteAndTryImageHelper");
            Ux.Warn(warnMsg);
            return ERR;
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

            var finder = new DeviceImageFinder(TargetDevice);
            WriteLineAndSetTip("寻找Boot文件中");
            var path = finder.PathOf(DeviceImage.Recovery);
            if (path == null)
            {
                WriteLineAndSetTip("寻找路径失败");
                return ERR;
            }
            else
            {
                WriteLine("寻找成功:" + path);
            }
            WriteLineAndSetTip("正在复制到临时目录");
            var tmpPath = $"{Adb.AdbTmpPathOnDevice}/tmp.img";
            var cpResult = new SuCommand(TargetDevice, $"cp {path} {tmpPath}")
                 .To(OutputPrinter)
                 .Execute();
            var pullResult = new AdbCommand(TargetDevice, $"pull {tmpPath} \"{Path.Combine(savePath, "recovery.img")}\"")
                 .To(OutputPrinter)
                 .Execute();
            return OK;
        }
    }
}

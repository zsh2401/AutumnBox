/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:37:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.OpenFramework.Extension;
using System.Windows.Forms;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot
{
    [ExtName("截图并保存到电脑")]
    [ExtName("Screenshot and save to pc", Lang = "en-US")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    public class EScreenShoter : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            DialogResult dialogResult = DialogResult.No;
            string path = null;
            App.RunOnUIThread(() =>
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                dialogResult = fbd.ShowDialog();
                path = fbd.SelectedPath;
            });

            if (dialogResult == DialogResult.OK)
            {
                string tmpFile = $"{Adb.AdbTmpPathOnDevice}/dream.png";
                TargetDevice.Shell($"/system/bin/screencap -p {tmpFile}");
                var result = new AdbCommand(TargetDevice, $"pull {tmpFile} {path}")
                    .To((e) =>
                    {
                        WriteLine(e.Text);
                    }).Execute();
                return result.ExitCode;
            }
            return ERR;
        }
        protected override bool VisualStop()
        {
            return false;
        }
    }
}

/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:37:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using System;
using System.IO;
using System.Windows.Forms;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("Capture the screen and save it to pc", "zh-cn:截图并保存到电脑")]
    [ExtIcon("Icons.screenshot.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    internal class EScreenShoter : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(IDevice device, ILeafUI ui)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Icon = this.GetIconBytes();
                ui.Show();
                var executor = new CommandExecutor();
                executor.OutputReceived += (s, e) => ui.WriteOutput(e.Text);
                var tmpPath = GenerateTmpPath();
                var capResult = executor.AdbShell(device, $"screencap -p {tmpPath}");
                if (capResult.ExitCode == 0)
                {
                    executor.Adb(device, $"pull {tmpPath} \"{GetSaveTarget(ui)}\"");
                    executor.AdbShell(device, $"rm -f {tmpPath}");
                }
                ui.Finish(capResult.ExitCode);
            }
        }
        private FileInfo GetSaveTarget(ILeafUI ui)
        {
            DialogResult dialogResult = DialogResult.No;
            FileInfo path = null;
            string saveDir = null;
            ui.RunOnUIThread(() =>
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                dialogResult = fbd.ShowDialog();
                saveDir = fbd.SelectedPath;
            });
            if (dialogResult == DialogResult.OK)
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                path = new FileInfo(Path.Combine(saveDir, fileName + ".png"));
                return path;
            }
            else
            {
                return null;
            }

        }
        private string GenerateTmpPath()
        {
            return $"/sdcard/atmb_screenshot_{Guid.NewGuid().ToString()}.png";
        }
    }
}

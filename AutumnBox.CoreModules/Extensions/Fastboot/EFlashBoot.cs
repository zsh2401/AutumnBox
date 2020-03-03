/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/26 13:57:02 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.LKit;
using Microsoft.Win32;

namespace AutumnBox.CoreModules.Extensions.Fastboot
{
    [ExtName("刷入Boot.img", "en-us:Flash boot.img")]
    [ExtRequiredDeviceStates(DeviceState.Fastboot)]
    [ExtIcon("Icons.cd.png")]
    [ExtText("EFlashBootSelectingTitle", "Select a image file", "zh-cn:选择一个文件")]
    [ExtText("EFlashBootSelectingFilter", "Image file(*.img)|*.img|Any file(*.*)|*.*", "zh-cn:镜像文件(*.img)|*.img|全部文件(*.*)|*.*")]
    internal class EFlashBoot : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI ui, IClassTextManager textManager,IDevice device)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Icon = this.GetIconBytes();
                ui.Show();
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Reset();
                fileDialog.Title = textManager["EFlashBootSelectingTitle"];
                fileDialog.Filter = textManager["EFlashBootSelectingFilter"];

                fileDialog.Multiselect = false;
                if (fileDialog.ShowDialog() != true) ui.EFinish();

                CommandExecutor executor = new CommandExecutor();
                executor.OutputReceived += (s, e) => ui.WriteOutput(e.Text);
                var result = executor.Fastboot(device,$"flash boot \"{fileDialog.FileName}\"");
                ui.Finish(result.ExitCode);
            }
        }
    }
}

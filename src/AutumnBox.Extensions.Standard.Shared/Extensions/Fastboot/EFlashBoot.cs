/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/26 13:57:02 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;
using Microsoft.Win32;

namespace AutumnBox.CoreModules.Extensions.Fastboot
{
    [ExtName("Flash Boot Partion", "zh-cn:刷入Boot分区")]
    [ExtDesc("Support A/B Slot now!", "zh-cn:现已支持A/B槽位切换!")]
    [ExtRequiredDeviceStates(DeviceState.Fastboot)]
    [ExtIcon("Icons.cd.png")]
    [ExtVersion(2, 0, 0)]
    [ExtAuth("zsh2401")]
    [ClassText("EFlashBootSelectingTitle", "Select a image file", "zh-cn:选择一个文件")]
    [ClassText("EFlashBootSelectingFilter", "Image file(*.img)|*.img|Any file(*.*)|*.*", "zh-cn:镜像文件(*.img)|*.img|全部文件(*.*)|*.*")]
    internal class EFlashBoot : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI _ui, ICommandExecutor _executor, IDevice device)
        {
            using var ui = _ui;
            using var executor = _executor;
            var text = ClassTextReaderCache.Acquire<EFlashBoot>();

            ui.Show();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = text["EFlashBootSelectingTitle"];
            fileDialog.Filter = text["EFlashBootSelectingFilter"];

            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                bool? slot = device.GetSlot();
                if (slot.HasValue)
                {
                    //Support A/B slot
                    executor.OutputReceived += (s, e) => ui.WriteLineToDetails(e.Text);
                    var result = executor.Fastboot(device, $"flash boot_{(slot.Value ? "a" : "b")} \"{fileDialog.FileName}\"");
                    ui.Finish(result.ExitCode == 0 ? StatusMessages.Success : StatusMessages.Failed);
                }
                else
                {
                    executor.OutputReceived += (s, e) => ui.WriteLineToDetails(e.Text);
                    var result = executor.Fastboot(device, $"flash boot \"{fileDialog.FileName}\"");
                    ui.Finish(result.ExitCode == 0 ? StatusMessages.Success : StatusMessages.Failed);
                }
            }
            else
            {
                ui.Shutdown();
            }
        }
    }
}

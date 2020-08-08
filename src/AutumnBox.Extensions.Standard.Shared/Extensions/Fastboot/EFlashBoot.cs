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
    [ExtName("刷入Boot.img", "en-us:Flash boot.img")]
    [ExtRequiredDeviceStates(DeviceState.Fastboot)]
    [ExtIcon("Icons.cd.png")]
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
                executor.OutputReceived += (s, e) => ui.WriteLineToDetails(e.Text);
                var result = executor.Fastboot(device, $"flash boot \"{fileDialog.FileName}\"");
                ui.Finish(result.ExitCode == 0 ? StatusMessages.Success : StatusMessages.Failed);
            }
            else
            {
                ui.Shutdown();
            }
        }
    }
}

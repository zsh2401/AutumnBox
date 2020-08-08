/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 1:45:39 (UTC +8:00)
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
    [ExtName("刷入REC", "en-us:Flash recovery.img")]
    [ExtRequiredDeviceStates(DeviceState.Fastboot)]
    [ExtIcon("Icons.cd.png")]
    [ClassText("Title", "Select a image file", "zh-cn:选择一个文件")]
    [ClassText("Filter", "Image file(*.img)|*.img|Any file(*.*)|*.*", "zh-cn:镜像文件(*.img)|*.img|全部文件(*.*)|*.*")]
    internal class EFlashRecovery : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI ui, IDevice device, ICommandExecutor executor)
        {
            using (ui)
            {
                using (executor)
                {
                    var text = ClassTextReaderCache.Acquire<EFlashRecovery>();
                    ui.Show();
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Reset();
                    fileDialog.Title = text["Title"];
                    fileDialog.Filter = text["Filter"];
                    fileDialog.Multiselect = false;
                    if (fileDialog.ShowDialog() == true)
                    {
                        executor.OutputReceived += (s, e) => ui.WriteLineToDetails(e.Text);
                        executor.Fastboot(device, $"flash recovery \"{fileDialog.FileName}\"");
                        var result = executor.Fastboot(device, $"boot \"{fileDialog.FileName}\"");
                        ui.Finish(result.ExitCode == 0 ? StatusMessages.Success : StatusMessages.Failed);
                    }
                    else
                    {
                        ui.Shutdown();
                    }
                }
            }
        }
    }
}

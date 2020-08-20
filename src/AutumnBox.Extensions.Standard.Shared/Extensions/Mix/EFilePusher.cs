/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:53:12 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;
using System.IO;

namespace AutumnBox.CoreModules.Extensions.Mix
{
    [ExtName("推送文件到手机主目录", "en-us:Push file to device")]
    [ExtIcon("Icons.filepush.png")]
    [ExtAuth("zsh2401")]
    [ExtRequiredDeviceStates(DeviceState.Poweron | DeviceState.Recovery)]
    [ClassText("title", "Select a file", "zh-cn:选择一个文件")]
    [ClassText("filter", "Any file", "zh-cn:任意文件(*.*)|*.*")]
    internal class EFilePusher : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI _ui, IDevice device, ICommandExecutor _executor)
        {
            using var ui = _ui;
            using var executor = _executor;
            var text = ClassTextReaderCache.Acquire<EFilePusher>();

            ui.Show();
            bool? dialogResult = null;
            string selectedFile = null;
            ui.RunOnUIThread(() =>
            {
                Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
                fileDialog.Reset();
                fileDialog.Title = text["title"];
                fileDialog.Filter = text["filter"];
                fileDialog.Multiselect = false;
                dialogResult = fileDialog.ShowDialog();
                selectedFile = fileDialog.FileName;
            });

            if (dialogResult != true) ui.EShutdown();
            FileInfo fileInfo = new FileInfo(selectedFile);

            executor.OutputReceived += (s, e) => ui.WriteLineToDetails(e.Text);
            var result = executor.Adb(device, $"push \"{fileInfo.FullName}\" \"/sdcard/{fileInfo.Name}\"");

            ui.Finish(result.ExitCode == 0 ? StatusMessages.Success : StatusMessages.Failed);
        }
    }
}

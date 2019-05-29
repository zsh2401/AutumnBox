/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/26 13:57:02 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;
using Microsoft.Win32;

namespace AutumnBox.CoreModules.Extensions.Fastboot
{
    //[ExtName("刷入Boot.img", "en-us:Flash boot.img")]
    //[ExtRequiredDeviceStates(DeviceState.Fastboot)]
    //[ExtIcon("Icons.cd.png")]
    //internal class EFlashBoot : LeafExtensionBase
    //{
    //    [LMain]
    //    public void EntryPoint(ILeafUI ui, IClassTextManager textManager)
    //    {
    //        using (ui)
    //        {
    //            OpenFileDialog fileDialog = new OpenFileDialog();
    //            fileDialog.Reset();
    //            fileDialog.Title = textManager["EFlashBootSelectingTitle"];
    //            fileDialog.Filter = textManager["EFlashBootSelectingFilter"];
    //        }
            
            
    //        fileDialog.Multiselect = false;
    //        if (fileDialog.ShowDialog() != true) return 1;
    //        var result = GetDeviceFastbootCommand($"flash boot \"{fileDialog.FileName}\"")
    //            .To(OutputPrinter)
    //            .Execute();
    //        WriteExitCode(result.ExitCode);
    //        return result.ExitCode;
    //    }
    //}
}

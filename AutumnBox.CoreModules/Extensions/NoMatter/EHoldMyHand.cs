/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 19:19:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.CoreModules.Extensions.Poweron.Dpm;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.Impl;
using AutumnBox.OpenFramework.Open.Management;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AutumnBox.CoreModules.Extensions.Hidden
{
    [ExtName("Example extension")]
    [ExtIcon("Icons.flash.png")]
    [ExtRequiredDeviceStates(LeafConstants.NoMatter)]
    [ExtDeveloperMode]
    [ExtText("fuck", "Hello", "zh-cn:你好!")]
    //[ExtMinAndroidVersion(9, 0, 0)]
    internal class EHoldMyHand : LeafExtensionBase
    {
        [LMain]
        public void Main(IDevice device, ILeafUI ui, IClassTextManager text, IEmbeddedFileManager emb, ITemporaryFloder tmp)
        {
            using (ui)
            {
                ui.Title = "TEST";
                ui.Show();
                ////storageManager = OpenApiFactory.Get<IStorageManager>(this, "123");
                //ui.WriteLine(text["fuck"]);
                //ui.WriteLine(this.GetType().Namespace);
                //ui.ShowMessage("wow");
                //ui.WriteOutput(ui.DoYN("wwww", "yes", "no"));
                //ui.WriteOutput(ui.DoChoice("www"));
                //ui.WriteOutput(ui.SelectFrom(null, "a", "b", "c", "d"));
                var dpmpro = new DpmPro(new CommandExecutor(), emb, tmp, new UsbDevice("aaabbb", DeviceState.Poweron));
                dpmpro.Extract();
                ui.Finish();
            }
        }
    }
}

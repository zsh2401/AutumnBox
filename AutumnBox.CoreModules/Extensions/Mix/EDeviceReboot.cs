using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Extensions.Mix
{
    [ExtName("Reboot", "zh-cn:重启")]
    [ExtAuth("zsh2401")]
    [ExtVersion(1,0,0)]
    [ExtIcon("Icons.restartdevice.png")]
    [ExtPriority(ExtPriority.HIGH + 2)]
    [ExtRequiredDeviceStates(DeviceState.Poweron | DeviceState.Recovery | DeviceState.Sideload | DeviceState.Fastboot)]
    [ExtText("modehint", "Reboot to?", "zh-cn:要重启到哪个模式?")]
    [ExtText("sys", "System", "zh-cn:系统")]
    [ExtText("rec", "Recovery", "zh-cn:恢复模式(Recovery)")]
    [ExtText("fb", "Fastboot", "zh-cn:Fastboot模式")]
    [ExtText("9008", "9008", "zh-cn:9008(不可靠)")]
    class EDeviceReboot : LeafExtensionBase
    {
        public void Main(IDevice device, ILeafUI ui, IClassTextManager text)
        {
            using (ui)
            {
                ui.Icon = this.GetIconBytes();
                ui.Title = this.GetName();
                //ui.Closing += (s, e) =>
                //{
                //    executor.Dispose();
                //    return true;
                //};
                //executor.OutputReceived += (s, e) =>
                //{
                //    ui.WriteLine(e.Text);
                //};
                ui.Show();
                string[] options = new string[] {
                    text["sys"],
                     text["rec"],
                      text["fb"],
                         text["9008"],
                };
                var result = ui.SelectFrom(text["modehint"], options);
                if (result == null) ui.EShutdown();
                if (result.Equals(options[0]))
                    device.Reboot2System();
                else if (result.Equals(options[1]))
                    device.Reboot2Recovery();
                else if (result.Equals(options[2]))
                    device.Reboot2Fastboot();
                else if (result.Equals(options[3]))
                    device.Reboot29008();
                else
                    device.Reboot2System();
                ui.Shutdown();
            }
        }
    }
}

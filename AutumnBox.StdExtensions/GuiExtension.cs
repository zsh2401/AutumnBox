/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/3 0:35:28 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.StdExtensions
{
    public class GuiExtension : AutumnBoxExtension
    {
        public override string Name => "GUI测试拓展";
        public override DeviceState RequiredDeviceState => DeviceState.None;
        public override void OnStartCommand(StartArgs args)
        {
            OpenApi.Gui.Dispatcher.Invoke(()=> {
                new Window1().ShowDialog();
            });
        }
    }
}

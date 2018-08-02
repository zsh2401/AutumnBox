/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/6 17:52:46 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.ExampleExtensions.Windows;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Attributes;
using AutumnBox.OpenFramework.Open;
using System;
using System.IO;
using System.Net.Mail;
using System.Windows;

namespace AutumnBox.ExampleExtensions
{
    [ExtRequiredDeviceStates(DeviceState.None)]
    [ExtName("ClearLove")]
    public class GuiExampleExtensions : AutumnBoxExtension
    {
        public override int Main()
        {
            App.RunOnUIThread(() =>
            {
                new ExampleWindow().Show();
            });
            return 0;
        }
    }
}

/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/6 17:52:46 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.ExampleExtensions.Windows;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Internal;
using AutumnBox.OpenFramework.Open.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.ExampleExtensions
{
    public class GuiExampleExtensions : AutumnBoxExtension
    {
        public override string Name => "带界面的拓展示例";
        public override string Auth => "zsh2401";
        public override string Description => "AutumnBox拓展允许使用自定义窗口";
        public override MailAddress ContactMail => new MailAddress("zsh2401@163.com");
        public override DeviceState RequiredDeviceState => DeviceState.None;
        public override bool InitAndCheck(InitArgs args)
        {
            Log("Init!");
            return base.InitAndCheck(args);
        }
        public override void OnStartCommand(StartArgs args)
        {
            OpenApi.Gui.Dispatcher.Invoke(() =>
            {
                new ExampleWindow()
                {
                    Owner = OpenApi.Gui.MainWindow,
                    WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
                }.ShowDialog();
            });
        }
    }
}

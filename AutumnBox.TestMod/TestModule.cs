/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/23 21:38:12 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Net.Mail;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.V1;

namespace AutumnBox.TestMod
{
    public class TestModule : AutumnBoxExtendModule
    {
        public override string Name => "测试模块";
        public override string Auth => "zsh2401";
        public override MailAddress ContactMail => new MailAddress("zsh2401@163.com");
        public override string Desc => "这是一个示例模块,没啥能做的.\n你可以在AutumnBox开源项目的AutumnBox.TestMod中查看本插件代码";
        public override Version Version => new Version("0.0.1");
        public override DeviceState RequiredDeviceState => DeviceState.Poweron | DeviceState.None;
        protected override void OnInit(InitArgs args)
        {
            base.OnInit(args);
            Log("Init!!");
        }
        protected override void OnStartCommand(RunArgs args)
        {
            var result = OpenApi.Gui.ShowChoiceBox(Name, "Hello AutumnBox!");
            OpenApi.Gui.ShowMessageBox(Name, $"choice result: {result}");
        }
    }
}

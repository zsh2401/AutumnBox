/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/23 21:38:12 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.V1;
using AutumnBox.OpenFramework.V1_5;

namespace AutumnBox.TestMod
{
    public class TestModule : AutumnBoxExtendModule
    {
        public override string Name => "测试模块";
        public override DeviceState RequiredDeviceState => DeviceState.Poweron | DeviceState.None;
        protected override void OnInit(InitArgs args)
        {
            base.OnInit(args);
            Log("Init!!");
        }
        protected override void OnStartCommand(RunArgs args)
        {
            var result = OpenFramework.V1.OpenApi.Gui.ShowChoiceBox(Name, "Hello AutumnBox!");
            OpenFramework.V1.OpenApi.Gui.ShowMessageBox(Name, $"choice result: {result}");
            OpenFramework.V1_5.OpenApi.Test();
        }
    }
}

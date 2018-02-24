/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/23 21:38:12 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework;

namespace AutumnBox.TestMod
{
    public class FuckMod : AutumnBoxExtendModule
    {
        public override string Name => "测试模块";
        public override DeviceState RequiredDeviceState => DeviceState.Poweron | DeviceState.None;
        public override void Run(RunArgs args)
        {
           var result =  OpenApi.Gui.ShowChoiceBox(Name,"Hello AutumnBox!");
            OpenApi.Gui.ShowMessageBox(Name,$"choice result: {result}");
        }
    }
}

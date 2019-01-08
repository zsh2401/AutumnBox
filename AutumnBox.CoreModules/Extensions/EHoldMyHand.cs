/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 19:19:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.CoreModules.Aspect;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("Example extension")]
    //[ExtDeveloperMode(true)]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    [UserAgree("Please be true")]
    internal class EHoldMyHand : LeafExtensionBase
    {

        //protected override void Processing(Dictionary<string, object> data)
        //{
        //    throw new Exception();
        //    Ux.ShowLoadingWindow();
        //    Ux.Message(Executor.Cmd("ping www.baidu.com").Output.ToString());
        //    Ux.CloseLoadingWindow();
        //    throw new Exception();
        //}
    }
}

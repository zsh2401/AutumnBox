/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 19:19:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.CoreModules.Aspect;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Running;
using System.Diagnostics;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("Example extension")]
    //[ExtDeveloperMode(true)]
    [ExtIcon("Icons.flash.png")]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    //[UserAgree("Please be true")]
    internal class EHoldMyHand : LeafExtensionBase
    {
        [LSignalReceive]
        private void OnCreate()
        {
            Trace.WriteLine("6666");
        }
        //[LMain]
        public void Main(IUx ux)
        {
            ux.Message("Leaf extension!");
        }

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

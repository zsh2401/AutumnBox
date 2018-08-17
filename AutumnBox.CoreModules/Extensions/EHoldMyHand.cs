/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 19:19:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using System.Threading;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("Running Window Test")]
    [ExtRequiredDeviceStates((DeviceState)255)]
    public class EHoldMyHand : AutumnBoxExtension
    {
        bool stoppable = false;
        public override int Main()
        {
            Ux.WriteLine("hey!");
            Ux.WriteLine("in other words, please be true!");
            Thread.Sleep(4000);
            stoppable = true;
            Ux.WriteLine("in other words, please be true!");
            Thread.Sleep(4000);
            return 0;
        }
        public override bool OnStopCommand()
        {
            return stoppable;
        }
    }
}

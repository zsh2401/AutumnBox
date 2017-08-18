using AutumnBox.Basic.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.DebugTools
{
    public class Tester
    {
        public static void Reboot(string id,Arg.RebootOptions option) {
            RebootOperator ro = new RebootOperator();
            ro.RebootFinish += new EventsHandlers.FinishEventHandler((o) => { Console.WriteLine("Reboot Finish"); });
            ro.Run(new Arg.RebootArgs { deviceID = id,rebootOption = option,nowStatus = DevicesTools.GetDeviceStatus(id)});
        }
    }
}
